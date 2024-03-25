
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace Degerlendirme.Controller
//{
//    using Degerlendirme.Data;
//    using Degerlendirme.Model;
//    // RatingController.cs
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.Extensions.Configuration;
//    using Microsoft.AspNetCore.Http;
//    using Newtonsoft.Json.Linq;


//    [Route("[controller]")]
//    public class RatingController : Controller
//    {
//        private readonly ApplicationDbContext _dbContext;
//        private readonly ILogger<RatingController> _logger;

//        public RatingController(ApplicationDbContext dbContext, ILogger<RatingController> logger)
//        {
//            _dbContext = dbContext;
//            _logger = logger;
//        }

//        [HttpPost("SaveRating")]
//        public IActionResult SaveRating([FromBody] RatingData ratingData)
//        {
//            try
//            {
//                var existingRating = _dbContext.Ratings.SingleOrDefault(r => r.StarId == ratingData.ClickedStarId);

//                if (existingRating != null)
//                {
//                    existingRating.VoteCount += 1;
//                    _dbContext.SaveChanges();

//                    // Değişiklikler başarıyla kaydedildi, view dosyasını çağır
//                    return View(ratingData);
//                }
//                else
//                {
//                    var newRating = new Rating
//                    {
//                        StarId = ratingData.ClickedStarId,
//                        VoteCount = 1
//                    };

//                    _dbContext.Ratings.Add(newRating);

//                    _logger.LogError($"Değişiklikler kaydedilemedi");
//                    _dbContext.SaveChanges();
//                    _logger.LogError($"Değişiklikler kaydedildi");
//                    // Değişiklikler başarıyla kaydedildi, view dosyasını çağır
//                    return View(ratingData);
//                }
//            }
//            catch (Exception ex)
//            {
//                // Hata durumunda
//                _logger.LogError($"Rating kaydedilirken bir hata oluştu: {ex.Message}");
//                return StatusCode(500, "Internal Server Error");
//            }
//        }
//    }
//}











using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Degerlendirme.Controllers
{
    using Degerlendirme.Data;
    using Degerlendirme.Model;
    // RatingController.cs
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json.Linq;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Route("[controller]")]
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ApplicationDbContext dbContext, ILogger<RatingController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        [HttpPost("SaveRating")]
        public IActionResult SaveRating([FromBody] RatingData ratingData)
        {
            try
            {
                _logger.LogError($"Veri alınamıyor");
                var existingRating = _dbContext.Ratings.SingleOrDefault(r => r.StarId == ratingData.ClickedStarId);

                if (existingRating != null)
                {
                    existingRating.VoteCount += 1;
                }
                else
                {
                    var newRating = new Rating
                    {
                        StarId = ratingData.ClickedStarId ?? throw new ArgumentNullException(nameof(ratingData.ClickedStarId)),
                        VoteCount = 1
                        
                    };

                    _dbContext.Ratings.Add(newRating);
                }

                // Değişiklikleri kaydetmek için SaveChanges metodu çağrılabilir
                _logger.LogError($"Veri kaydedilemedi");
                int affectedRows = _dbContext.SaveChanges();

                if (affectedRows > 0)
                {

                    return View("SaveRating");
                    
                    //return RedirectToPage("/Views/Rating/SaveRating");
                    //return Ok(new { redirectUrl = "/Rating/SaveRating" });
                }
                else
                {
                    _logger.LogError($"Değişiklikler kaydedilemedi");
                    return StatusCode(500, "Internal Server Error");
                }

            }
            catch (Exception ex)
            {
                // Hata durumunda
                _logger.LogError($"Rating kaydedilirken bir hata oluştu: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        public IActionResult Index()
        {
            // Rating/Index sayfasını döndür
            return View("SaveRating");
        }

    }
}

