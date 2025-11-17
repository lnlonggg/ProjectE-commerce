using MyShop.Core.Entities;

namespace MyShop.Core.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}