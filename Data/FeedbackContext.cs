using Microsoft.EntityFrameworkCore;
using PC3SALAZAR.Models;

namespace PC3SALAZAR.Data
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options) { }

        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
