using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    }
}
