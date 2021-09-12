using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EveriseAPI.Models;
namespace EveriseAPI.Models
{
    public class APIContext : DbContext
    {

        public Data data = null;

        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {

        }

        public DbSet<Orders_States> OrderStates { get; set; }
        public DbSet<Trs_PriceTypes> TrsPriceTypes { get; set; }
    }
}
