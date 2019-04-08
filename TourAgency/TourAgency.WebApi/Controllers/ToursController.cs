using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourAgency.BLL.DTOs;
using TourAgency.BLL.Interfaces;
using TourAgency.WebApi.ViewModel;

namespace TourAgency.WebApi.Controllers
{

    [Authorize]
    [Route("api/tours")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;
        private readonly IMapper _mapper;

        public ToursController(ITourService tourService, IMapper mapper)
        {
            _tourService = tourService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<TourForListViewModel> GetAllTours()
        {
            var toursDTO = _tourService.GetAllTours();
            var mapperList = new MapperConfiguration(cfg => cfg.CreateMap<TourForListDTO, TourForListViewModel>()).CreateMapper();
            var tours = mapperList.Map<IEnumerable<TourForListDTO>, List<TourForListViewModel>>(toursDTO);
            return tours;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetTour(int? id)
        {
            var tourDTO = _tourService.GetTour(id);
            if (tourDTO == null)
            {
                return NotFound();
            }
            var tour = _mapper.Map<TourForDetailedViewModel>(tourDTO);
            return Ok(tour);
        }

        [HttpPost]
        public IActionResult CreateTour([FromBody] TourViewModel model)
        {
            //
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tourDTO = _mapper.Map<TourDTO>(model);
            var result = _tourService.CreateTour(tourDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest("New tour wasn't created");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTour(int? id, [FromBody] TourViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tourDTO = _mapper.Map<TourDTO>(model);
            var result = _tourService.UpdateTour(id, tourDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest("changes not saved");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTour(int? id)
        {
            var result = _tourService.DeleteTour(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("tour wasn't deleted");
        }
    }
}