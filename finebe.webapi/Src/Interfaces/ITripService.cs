using finebe.webapi.Src.Persistence.DomainModel;

namespace finebe.webapi.Src.Interfaces;

public interface ITripService
{
    Task<List<Trip>> GetAllTripsAsync();
    Task<Trip> GetTripByIdAsync(Guid id);
    Task<Trip> CreateTripAsync(Trip trip);
    Task<Trip> UpdateTripAsync(Trip trip);
    Task<Trip> DeleteTripAsync(Guid id);
}

