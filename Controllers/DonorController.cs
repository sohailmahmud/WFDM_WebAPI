using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WFDM.IServices;
using WFDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController(IUnitOfWorks unitOfWorks) : ControllerBase
    {
        private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;

        [HttpGet]
        public IEnumerable<Donor> Gets()
        {
            return _unitOfWorks.Donor.Gets();
        }

        [HttpGet("{id}")]
        public IEnumerable<Donor> Get(int id)
        {
            return _unitOfWorks.Donor.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Donor entity)
        {
            await _unitOfWorks.Donor.Create(entity);
            return Ok(entity);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Donor>> Update(int id, Donor entity)
        {
            await _unitOfWorks.Donor.Update(id, entity);
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWorks.Donor.Delete(id);
            return Ok();
        }

    }
}
