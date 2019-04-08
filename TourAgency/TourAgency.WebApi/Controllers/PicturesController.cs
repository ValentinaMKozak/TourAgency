using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourAgency.BLL.Interfaces;
using TourAgency.WebApi.ViewModel;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using TourAgency.WebApi.Helpers;
using TourAgency.BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TourAgency.WebApi.Data;
using System.Security.Claims;

namespace TourAgency.WebApi.Controllers
{
    [Authorize]
    [Route("api/tours/{tourId}/pictures")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPictureService _pictureService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITourService _tourService;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PicturesController(IPictureService pictureService,
                                    UserManager<ApplicationUser> userManager,
                                  ITourService tourService,
                                  IMapper mapper,
                                  IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _pictureService = pictureService;
            _userManager = userManager;
            _tourService = tourService;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet]
        public IEnumerable<PictureViewModel> GetPicturesByTourId(int? tourId)
        {
            var pictureDTO = _pictureService.GetAllPictures(tourId);
            var pictureForReturn = _mapper.Map<IEnumerable<PictureDTO>, List<PictureViewModel>>(pictureDTO);
            return pictureForReturn;
        }

        [HttpGet("{id}")]
        public IActionResult GetPicture(int? tourId, int? id)
        {
            var pictureDTO = _pictureService.GetPicture(id);
            var pictureViewModel = _mapper.Map<PictureDTO>(pictureDTO);

            return Ok(pictureViewModel);
        }

        [HttpPost("upload/{userId}")]
        public async Task<IActionResult> AddPictureForTour(int? tourId, string userId, IFormFile file) 
        {
            PictureForCreationViewModel pictureModel = new PictureForCreationViewModel
            {
                File = file
            };

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Could not find user");

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (currentUserId != userId)
                return Unauthorized();
                
            var tourDTO = _tourService.GetTour(tourId); 

            if (tourDTO == null)
                return BadRequest("you cannot add picture to not existed tour");

            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            pictureModel.URL = uploadResult.Uri.ToString();
            pictureModel.PublicId = uploadResult.PublicId;


            var pictureDTO = _mapper.Map<PictureForCreationDTO>(pictureModel);

            pictureDTO.TourDTO = tourDTO;
            pictureDTO.TourId = tourDTO.TourId;

            if (!tourDTO.Pictures.Any(m => m.IsMain))
                pictureDTO.IsMain = true;

   
            var isSaved = _pictureService.CreatePicture(pictureDTO);
            if (isSaved)
            {
                var savedPictureDto = _pictureService.GetPictureByCloudId(pictureModel.PublicId);
                tourDTO.Pictures.Add(savedPictureDto);
                return Ok();
            }
 
            return BadRequest("Cound not add picture");
        }

        [HttpPost("{id}/setMain/{userId}")]
        public IActionResult SetMainPicture(int? tourId, int? id, string userId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            var pictureDTO = _pictureService.GetPicture(id);
            if (pictureDTO == null)
                return NotFound();

            if (pictureDTO.IsMain)
                return BadRequest("This is already the main picture");

            var currentMainPicture = _pictureService.GetMainPicture(tourId);
            if (currentMainPicture != null)
                currentMainPicture.IsMain = false;


            pictureDTO.IsMain = true;
            if (_pictureService.UpdatePicture(pictureDTO.PictureId, pictureDTO))
            {
                _pictureService.UpdatePicture(currentMainPicture.PictureId, currentMainPicture);
                return NoContent();
            }
            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("{id}/{userId}")]
        public IActionResult DeletePicture(int? tourId, int? id, string userId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            var pictureDTO = _pictureService.GetPicture(id);

            if (pictureDTO == null)
                return NotFound();

            if (pictureDTO.IsMain)
                return BadRequest("You can not delete the main photo");

            if (pictureDTO.PictureId != null)
            {
                var deleteParams = new DeletionParams(pictureDTO.PublicId);

                var result = _cloudinary.Destroy(deleteParams);
                if (result.Result == "ok")
                    _pictureService.DeletePicture(pictureDTO.PictureId);
                return Ok();
            }
            if (pictureDTO.PublicId == null)
            {
                _pictureService.DeletePicture(pictureDTO.PictureId);
                return Ok();
            }
            return BadRequest("Failed to delete the photo");
        }
    }
}



