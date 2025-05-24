using Microsoft.AspNetCore.Mvc;
using PC3SALAZAR.Data;
using PC3SALAZAR.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace PC3SALAZAR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackContext _context;

        public FeedbackController(FeedbackContext context)
        {
            _context = context;
        }

        // GET: api/feedback
        [HttpGet]
        public IActionResult GetAll()
        {
            var feedbacks = _context.Feedbacks.ToList();
            return Ok(feedbacks);
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody] Feedback feedback)
        {
            if (feedback == null)
                return BadRequest("Feedback no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(feedback.Reaccion))
                return BadRequest("La reacción es obligatoria.");

            if (feedback.PostId <= 0)
                return BadRequest("PostId inválido.");

            // Normalizar reacción para evitar problemas (ejemplo: "Like", "like ", etc.)
            var reaccionNormalized = feedback.Reaccion.Trim().ToLower();

            if (reaccionNormalized != "like" && reaccionNormalized != "dislike")
                return BadRequest("La reacción debe ser 'like' o 'dislike'.");

            bool existe = _context.Feedbacks.Any(f => f.PostId == feedback.PostId);
            if (existe)
                return BadRequest("Este post ya tiene feedback.");

            feedback.Reaccion = reaccionNormalized;
            feedback.Fecha = DateTime.UtcNow;

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(feedback);
        }
    }
}
