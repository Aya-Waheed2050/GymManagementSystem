namespace Presentation.Controllers
{
    [Authorize]
    public class PlanController(IPlanServices _planServices) 
        : Controller
    {

        #region Get All Plans
        public IActionResult Index()
        {
            var plans = _planServices.GetAllPlans();
            return View(plans);
        }

        #endregion

        #region Get plan Details
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "Invalid Plan ID.";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planServices.GetPlanById(id);
            if (plan is null)
            {
                TempData["Error"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }
        #endregion

        #region Edit Plan

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var result = _planServices.GetPlanToUpdate(id);
            if (!result.IsValid)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Plan);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, UpdatePlanViewModel updatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Please correct the errors in the form.");
                return View(updatedPlan);
            }
            var planResult = _planServices.GetPlanToUpdate(id);
         
            bool Result = _planServices.UpdatePlan(id, updatedPlan);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Update Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Plan";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Active & Deactive - Soft Delete
        [HttpPost]
        public IActionResult Activate(int id)
        {
            var Result = _planServices.ToggleStatus(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Change Plan Status. Please try again.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}