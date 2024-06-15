using AutoMapper;
using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Trips;
using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace finebe.webapi.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;

        public TripsController(ITripService tripService, IMapper mapper)
        {
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<IActionResult> GetTrips()
        {
            try
            {
                var trips = await _tripService.GetAllTripsAsync();
                var tripResponseDtos = _mapper.Map<List<TripResponseDto>>(trips);
                return Ok(tripResponseDtos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var trip = await _tripService.GetTripByIdAsync(id);
                if (trip == null)
                {
                    return NotFound();
                }

                var tripResponseDto = _mapper.Map<TripResponseDto>(trip);
                return Ok(tripResponseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TripRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trip = _mapper.Map<Trip>(model);
                    var createdTrip = await _tripService.CreateTripAsync(trip);

                    var tripResponseDto = _mapper.Map<TripResponseDto>(createdTrip);
                    return Ok(tripResponseDto);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TripRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trip = await _tripService.GetTripByIdAsync(id);
                    if (trip == null)
                    {
                        return NotFound();
                    }

                    _mapper.Map(model, trip);

                    var updatedTrip = await _tripService.UpdateTripAsync(trip);
                    var tripResponseDto = _mapper.Map<TripResponseDto>(updatedTrip);
                    return Ok(tripResponseDto);
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var trip = await _tripService.GetTripByIdAsync(id);
                if (trip == null)
                {
                    return NotFound();
                }

                var deletedTrip = await _tripService.DeleteTripAsync(id);
                var tripResponseDto = _mapper.Map<TripResponseDto>(deletedTrip);
                return Ok(tripResponseDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
