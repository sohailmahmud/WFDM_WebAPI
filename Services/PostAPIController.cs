using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WFDM.IServices;

namespace WFDM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostAPIController : ControllerBase
{

    private readonly IPostAPIServices _postUpdateDeServices;

    public PostAPIController(IPostAPIServices postUpdateDeServices)
    {
        _postUpdateDeServices = postUpdateDeServices;
    }

    [HttpPost]
    public IActionResult InsertAll(string query)
    {
        var users = _postUpdateDeServices.QueryExecute(query);

        return Ok(users);
    }

    [HttpGet("getQuery")]
    public IActionResult GetAll(string getQuery)
    {
        var result = _postUpdateDeServices.GetAll(getQuery);

        return Ok(result);
    }

    [HttpGet("ShowData")]
    public IActionResult GetQueryAll(string query)
    {
        var result = _postUpdateDeServices.GetQueryAll(query);

        string json = JsonConvert.SerializeObject(result, Formatting.Indented);
        return Ok(json);
    }
}