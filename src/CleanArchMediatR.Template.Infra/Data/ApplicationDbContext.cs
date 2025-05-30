using CleanArchMediatR.Template.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Infra.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<UserEntity> USERS {  get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
