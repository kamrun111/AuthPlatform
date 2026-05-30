using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Auth.EF
{


    namespace AuthPlatform.Infrastructure.Auth.EF
    {
        public class AuthUserGroupRepository : IAuthUserGroupRepository
        {
            private readonly ApplicationDbContext _context;

            public AuthUserGroupRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task AddAsync(AuthUserGroup entity)
            {
                await _context.AuthUserGroups.AddAsync(entity);

                await _context.SaveChangesAsync();
            }

            public async Task<List<AuthUserGroup>> GetByUserIdAsync(int authUserId)
            {
                return await _context.AuthUserGroups
                    .Include(x => x.AuthGroup)
                    .Where(x => x.AuthUserId == authUserId && x.IsActive)
                    .ToListAsync();
            }

            public async Task DeleteAsync(AuthUserGroup entity)
            {
                _context.AuthUserGroups.Remove(entity);

                await _context.SaveChangesAsync();
            }
        }
    }
}
