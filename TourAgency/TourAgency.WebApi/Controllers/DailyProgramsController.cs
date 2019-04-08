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
    [Route("api/tours/{tourId}/dailyprograms")]
    [ApiController]
    public class DailyProgramsController : ControllerBase
    {
        private readonly IDailyProgramService _dailyProgramService;
        private readonly IMapper _mapper;

        public DailyProgramsController(IDailyProgramService dailyProgramService, IMapper mapper)
        {
            _dailyProgramService = dailyProgramService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<DailyProgramViewModel> GetAllDailyProgramByTourId(int tourId)
        {
            var dailyProgramDTO = _dailyProgramService.GetAllDailyPrograms(tourId);
            var mapperList = new MapperConfiguration(cfg => cfg.CreateMap<DailyProgramDTO, DailyProgramViewModel>()).CreateMapper();
            return mapperList.Map<IEnumerable<DailyProgramDTO>, List<DailyProgramViewModel>>(dailyProgramDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetDailyProgram(int tourId, int id)
        {
            var dailyProgramDTO = _dailyProgramService.GetDailyProgram(id);
            var dailyProgram = _mapper.Map<DailyProgramViewModel>(dailyProgramDTO);
            return Ok(dailyProgram);
        }

        [HttpPost]
        public IActionResult CreateDailyProgram(int tourId, [FromBody] DailyProgramViewModel model)
        {
            var dailyProgramDTO = _mapper.Map<DailyProgramDTO>(model);
            bool isSaved = _dailyProgramService.CreateDailyProgram(dailyProgramDTO);
            if (isSaved)
            {
                return Ok();
            }
            return BadRequest("Could not add the dailyProgram");
        }

        [HttpPut]
        public IActionResult UpdateDailyProgram(int tourId, [FromBody] DailyProgramViewModel model)
        {
            var dailyProgramDTO = _mapper.Map<DailyProgramDTO>(model);
            bool isSaved = _dailyProgramService.UpdateDailyProgram(dailyProgramDTO.DailyProgramId, dailyProgramDTO);
            if (isSaved)
            {
                return Ok();
            }
            return BadRequest("Could not add the dailyProgram");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDailyProgram(int tourId, int? id)
        {
            var result = _dailyProgramService.DeleteDailyProgram(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("dailyProgram wasn't deleted");
        }
    }
}
