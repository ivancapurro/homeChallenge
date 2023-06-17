using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Service.Implementation;
using WebAPI.Service.Interface;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MasterDataController: Controller
{

    private readonly IMasterDataService _masterDataService;

    public MasterDataController(IMasterDataService masterDataService)
    {
        _masterDataService = masterDataService;
    }

    [HttpGet("getRandomMasterData")]
    public IActionResult GetRandomMasterData()
    {
        var masterData = _masterDataService.GetRandomMasterData();
        return Ok(masterData);
    }
}