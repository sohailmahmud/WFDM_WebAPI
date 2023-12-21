using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WFDM.IServices;
using WFDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WFDM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController(IUnitOfWorks unitOfWorks) : ControllerBase
{

    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;

    [HttpGet]
    public IEnumerable<Volunteer> Gets()
    {
        return _unitOfWorks.Volunteer.Gets();
    }

    [HttpGet("{id}")]
    public IEnumerable<Volunteer> Get(int id)
    {
        return _unitOfWorks.Volunteer.Get(id);
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Volunteer entity)
    {
        await _unitOfWorks.Volunteer.Create(entity);
        return Ok(entity);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Volunteer>> Update(int id, Volunteer entity)
    {
        await _unitOfWorks.Volunteer.Update(id, entity);
        return Ok(entity);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWorks.Volunteer.Delete(id);
        return Ok();
    }
}
