using System;
using System.ComponentModel.DataAnnotations;

namespace PC3SALAZAR.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        [Required]
        public string Reaccion { get; set; } // "like" o "dislike"

        public DateTime Fecha { get; set; }
    }
}