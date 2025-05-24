using Microsoft.AspNetCore.Mvc;
using PC3SALAZAR.Models;
using PC3SALAZAR.Services;
using System.Threading.Tasks;

namespace PC3SALAZAR.Controllers
{
    public class NewsController : Controller
    {
        private readonly IExternalApiService _apiService;

        public NewsController(IExternalApiService apiService)
        {
            _apiService = apiService;
        }

        // Acción para mostrar la lista de posts
        public async Task<IActionResult> Index()
        {
            var posts = await _apiService.GetPostsAsync();
            return View(posts);
        }

        // Acción para mostrar detalles de un post específico
        public async Task<IActionResult> Details(int id)
        {
            var posts = await _apiService.GetPostsAsync();
            var post = posts.Find(p => p.id == id);

            if (post == null)
                return NotFound();

            var author = await _apiService.GetUserAsync(post.userId);
            var comments = await _apiService.GetCommentsAsync(id);

            ViewData["Author"] = author;
            ViewData["Comments"] = comments;

            return View(post);
        }

        // API para recibir feedback (reacción) desde frontend
        [HttpPost]
        public async Task<IActionResult> SendFeedback(int postId, string reaccion)
        {
            if (postId <= 0)
                return BadRequest(new { message = "PostId inválido." });

            if (string.IsNullOrWhiteSpace(reaccion))
                return BadRequest(new { message = "La reacción es obligatoria." });

            var reaccionNormalized = reaccion.Trim().ToLower();
            if (reaccionNormalized != "like" && reaccionNormalized != "dislike")
                return BadRequest(new { message = "La reacción debe ser 'like' o 'dislike'." });

            bool success = await _apiService.SendFeedbackAsync(postId, reaccionNormalized);
            if (success)
                return Ok(new { message = "Feedback enviado." });
            
            return BadRequest(new { message = "Error al enviar feedback." });
        }
    }
}
