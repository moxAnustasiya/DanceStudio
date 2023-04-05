using DanceStudio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics.CodeAnalysis;


namespace DanceStudio.Context
{
    public class HallContext : DbContext
    {
        public DbSet<Hall> Halls { get; set; } = null!;
    }
}
