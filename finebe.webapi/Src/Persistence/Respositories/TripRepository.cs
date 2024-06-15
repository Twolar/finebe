using Microsoft.EntityFrameworkCore;
using finebe.webapi.Src.Persistence;
using finebe.webapi.Src.Persistence.DomainModel;
using finebe.webapi.Src.Interfaces;

namespace finebe.webapi.Src.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _context;

        public TripRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Trip>> GetAllTripsAsync()
        {
            return await _context.Trips.ToListAsync();
        }

        public async Task<Trip> GetTripByIdAsync(Guid id)
        {
            return await _context.Trips.FindAsync(id);
        }

        public async Task<Trip> CreateTripAsync(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException(nameof(trip));
            }

            await _context.Trips.AddAsync(trip);
            await _context.SaveChangesAsync();

            return trip;
        }

        public async Task<Trip> UpdateTripAsync(Trip trip)
        {
            if (trip == null)
            {
                throw new ArgumentNullException(nameof(trip));
            }

            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();

            return trip;
        }

        public async Task<Trip> DeleteTripAsync(Guid id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                throw new KeyNotFoundException($"Trip with id {id} not found.");
            }

            trip.IsDeleted = true;

            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();

            return trip;
        }
    }
}
