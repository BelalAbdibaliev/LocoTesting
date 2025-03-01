using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Domain.Entities;
using LocoTesting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LocoTesting.Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserProfile?> GetUserProfileAsync(string userId)
    {
        return await _context.UserProfiles.FirstOrDefaultAsync(u => u.AppUserId == userId);
    }
}