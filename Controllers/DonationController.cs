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
public class DonationController(IUnitOfWorks unitOfWorks) : ControllerBase
{
    private readonly IUnitOfWorks _unitOfWorks = unitOfWorks;

    [HttpGet]
    public IEnumerable<Donation> Gets()
    {
        return _unitOfWorks.Donation.Gets();
    }

    [HttpGet("{id}")]
    public IEnumerable<Donation> Get(int id)
    {
        return _unitOfWorks.Donation.Get(id);
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Donation entity)
    {
        await _unitOfWorks.Donation.Create(entity);
        return Ok(entity);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Donation>> Update(int id, Donation entity)
    {
        await _unitOfWorks.Donation.Update(id, entity);
        return Ok(entity);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWorks.Donation.Delete(id);
        return Ok();
    }
}
