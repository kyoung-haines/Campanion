using Campanion.Shared.Dtos.CampgroundDtos;

namespace App.API.Services
{
    public interface ICampgroundService
    {
        public Task<Result<bool>> DeleteCampgroundAsync(int id);
        public Task<Result<List<CampgroundDto>>> GetAllCampgroundsAsync();
        public Task<Result<CampgroundDto>> GetCampgroundByIdAsync(int id);
        public Task<Result<CampgroundDto>> UpdateCampgroundAsync(CampgroundDto originalCampgroundDto);
        public Task<Result<CampgroundDto>> AddCampgroundAsync(CampgroundDto campgroundDto);
        public Task<Result<bool>> CampgroundIdIsExistsAsync(int newCampId);
        public Task<Result<bool>> IsCampgroundUpdatedAsync(CampgroundDto originalCampgroundDto, CampgroundDto updatedCampgroundDto);
        public Task<Result<bool>> IsCampgroundAddedAsync(int newCampgroundId);
    }
}
