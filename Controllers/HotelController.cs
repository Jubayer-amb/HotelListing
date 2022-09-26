using AutoMapper;
using HotelListing.Data.Models;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelController> _logger;

    public HotelController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<HotelController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHotels()
    {
        try
        {
            var Hotels = await _unitOfWork.Hotels.GetAll();
            var results = _mapper.Map<IList<HotelDTO>>(Hotels);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(HotelController)}");
            return BadRequest("Failed to get Hotels");
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHotel(Guid id)
    {
        try
        {
            var Hotel = await _unitOfWork.Hotels.Get(c => c.Id == id, new List<string> { "Country" });
            var result = _mapper.Map<HotelDTO>(Hotel);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(HotelController)}");
            return BadRequest("Failed to get Hotel");
        }
    }
}