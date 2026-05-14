namespace App.API.Services
{
    public interface IRolesService
    {
        Task<List<string>> GetAllRolesAsync();
    }
}
