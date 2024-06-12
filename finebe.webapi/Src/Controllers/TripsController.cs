using AutoMapper;
using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Trips;
using finebe.webapi.Src.Persistence;
using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finebe.webapi.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;

        public TripsController(ApplicationDbContext context, IAuthenticatedUserService authenticatedUserService, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _context.Trips.ToListAsync();
            var tripResponseDtos = _mapper.Map<List<TripResponseDto>>(trips);
            return Ok(tripResponseDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            var tripResponseDto = _mapper.Map<TripResponseDto>(trip);
            return Ok(tripResponseDto);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TripRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trip = _mapper.Map<Trip>(model);
                    _context.Trips.Add(trip);
                    await _context.SaveChangesAsync();

                    return Ok("Created");
                }

                return BadRequest(ModelState);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
