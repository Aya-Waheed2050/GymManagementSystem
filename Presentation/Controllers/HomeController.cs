namespace Presentation.Controllers
{
    [Authorize]
    public class HomeController(IAnalyticsServices _analyticsService) : Controller
    {

        public IActionResult Index()
        {
            var Data = _analyticsService.GetAnalyticsData();
            return View(Data);
        }

    }
}
