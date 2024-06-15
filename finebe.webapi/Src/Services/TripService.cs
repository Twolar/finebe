using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Trips;
using finebe.webapi.Src.Persistence.DomainModel;
using finebe.webapi.Src.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace finebe.webapi.Src.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
        }

        public async Task<List<Trip>> GetAllTripsAsync()
        {
            return await _tripRepository.GetAllTripsAsync();
        }

        public async Task<Trip> GetTripByIdAsync(Guid id)
        {
            return await _tripRepository.GetTripByIdAsync(id);
        }

        public async Task<Trip> CreateTripAsync(Trip trip)
        {
            return await _tripRepository.CreateTripAsync(trip);
        }

        public async Task<Trip> UpdateTripAsync(Trip trip)
        {
            return await _tripRepository.UpdateTripAsync(trip);
        }

        public async Task<Trip> DeleteTripAsync(Guid id)
        {
            return await _tripRepository.DeleteTripAsync(id);
        }
    }
}
