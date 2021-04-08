using BAS.AppServices.DTOs;
using BAS.AppServices.Services.Interfaces;
using BAS.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAS.Projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly IPersonnelService personnelService;

        public PersonnelController(IPersonnelService personnelService)
        {
            this.personnelService = personnelService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonnel([FromRoute] long id)
        {
            var personnel = await personnelService.GetPersonnel(id);
            return Ok(personnel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonnelList([FromBody] PersonnelFilters personnelFilters)
        {
            var personnelList = await personnelService.GetPersonnelWtihFilter(personnelFilters);
            return Ok(personnelList);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPersonnel([FromBody] Personnel personnel)
        {
            var result = await personnelService.InsertPersonnel(personnel);
            return result ? Ok() : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePersonnel([FromBody] PersonnelDTO personnelDTO)
        {

            var result = await personnelService.UpdatePersonnel(personnelDTO);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonnel([FromRoute] long id)
        {
            var result = await personnelService.DeletePersonnel(id);
            return result ? Ok() : NotFound();
        }
    }
}
