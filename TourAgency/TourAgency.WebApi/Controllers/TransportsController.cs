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
    [Route("api/transports")]
    [ApiController]
    public class TransportsController : ControllerBase
    {
        private readonly ITransportService _transportService;
        private readonly IMapper _mapper;

        public TransportsController(ITransportService transportService, IMapper mapper)
        {
            _transportService = transportService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<TransportViewModel> GetAllTransports()
        {
            var transportDTO = _transportService.GetAllTransports();
            var mapperList = new MapperConfiguration(cfg => cfg.CreateMap<TransportDTO, TransportViewModel>()).CreateMapper();
            var transports = mapperList.Map<IEnumerable<TransportDTO>, List<TransportViewModel>>(transportDTO);
            return transports;
        }

        [HttpGet("{id}")]
        public IActionResult GetTransport(int? id)
        {
            var transportDTO = _transportService.GetTransport(id);
            if (transportDTO == null)
            {
                return NotFound();
            }
            var transport = _mapper.Map<TransportViewModel>(transportDTO);
            return Ok(transport);
        }

        [HttpPost]
        public IActionResult CreateTransport([FromBody] TransportViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transportDTO = _mapper.Map<TransportDTO>(model);
            var result = _transportService.CreateTransport(transportDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest("New transport wasn't created");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTransport(int? id, [FromBody] TransportViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transportDTO = _mapper.Map<TransportDTO>(model);
            var result = _transportService.UpdateTransport(id, transportDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest("changes not saved");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransport(int? id)
        {
            var result = _transportService.DeleteTransport(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("transport wasn't deleted");
        }
    }
}