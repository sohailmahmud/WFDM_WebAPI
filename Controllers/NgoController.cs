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
public class NgoController(IUnitOfWorks unitOfWorks) : ControllerBase
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;

    [HttpGet]
    public IEnumerable<Ngo> Gets()
    {
        return _unitOfWorks.Ngo.Gets();
    }

    [HttpGet("{id}")]
    public IEnumerable<Ngo> Get(int id)
    {
        return _unitOfWorks.Ngo.Get(id);
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Ngo entity)
    {
        await _unitOfWorks.Ngo.Create(entity);
        return Ok(entity);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Ngo>> Update(int id, Ngo entity)
    {
        await _unitOfWorks.Ngo.Update(id, entity);
        return Ok(entity);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWorks.Ngo.Delete(id);
        return Ok();
    }
}
