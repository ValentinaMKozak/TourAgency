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
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountriesController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CountryViewModel> GetAllCountries()
        {
            var countryDTO = _countryService.GetAllCountries();
            var mapperList = new MapperConfiguration(cfg => cfg.CreateMap<CountryDTO, CountryViewModel>()).CreateMapper();
            var countries = mapperList.Map<IEnumerable<CountryDTO>, List<CountryViewModel>>(countryDTO);
            return countries;
        }

        [HttpGet("{id}")]
        public IActionResult GetCountry(int? id)
        {
            var countryDTO = _countryService.GetCountry(id);
            if (countryDTO == null)
            {
                return NotFound();
            }
            var country = _mapper.Map<CountryViewModel>(countryDTO);
            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryViewModel model)
        {
            var countryDTO = _mapper.Map<CountryDTO>(model);
            var result = _countryService.CreateCountry(countryDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest("New country wasn't created");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int? id, [FromBody] CountryViewModel model)
        {
            var countryDTO = _mapper.Map<CountryDTO>(model);
            var result = _countryService.UpdateCountry(id, countryDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest("changes not saved");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int? id)
        {
            var result = _countryService.DeleteCountry(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("country wasn't deleted");
        }
    }
}