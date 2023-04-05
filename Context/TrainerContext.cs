using DanceStudio.Models;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Context
{
    public class TrainerContext : DbContext
    {
        public DbSet<Trainer> Trainers { get; set; } = null!;
    }
}
