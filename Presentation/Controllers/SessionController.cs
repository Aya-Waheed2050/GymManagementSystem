namespace Presentation.Controllers
{
    [Authorize]
    public class SessionController(ISessionServices _sessionServices) 
        : Controller
    {

        #region Get All Session
        public IActionResult Index()
        {
            var sessions = _sessionServices.GetAllSessions();
            return View(sessions);
        }
        #endregion

        #region Get Session Details

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id!";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionServices.GetSessionById(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found!";
                return RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        #endregion

        #region Create Session
        public IActionResult Create()
        {
            LoadDropdownsTrainers();
            LoadDropdownsCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSessionViewModel createdSession) 
        {
            if(!ModelState.IsValid)
            {
                LoadDropdownsTrainers();
                LoadDropdownsCategories();
                return View(createdSession);
            }

            var Result = _sessionServices.CreateSession(createdSession);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to Create Session!";
                LoadDropdownsTrainers();
                LoadDropdownsCategories();
                return View(createdSession);
            }

            else
            {
                TempData["SuccessMessage"] = "Session Created Successfully!";
                LoadDropdownsTrainers();
                LoadDropdownsCategories();
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion

        #region Edit Session

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id!";
                return RedirectToAction(nameof(Index));
            }

            var exists = _sessionServices.GetSessionById(id);
            if (exists is null)
            {
                TempData["ErrorMessage"] = "Session Not Found!";
                return RedirectToAction(nameof(Index));
            }
            var sessionToUpdate = _sessionServices.GetSessionToUpdate(id);
            if (sessionToUpdate is null)
            {
                TempData["ErrorMessage"] = "You can't edit an ongoing or completed session.";
                return RedirectToAction(nameof(Index));
            }
            LoadDropdownsTrainers();
            return View(sessionToUpdate);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, UpdateSessionViewModel updatedSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdownsTrainers();
                return View(updatedSession);
            }

            var Result = _sessionServices.UpdateSession(updatedSession, id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Session Edited Successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Edit Session!";
                LoadDropdownsTrainers();
                return View(updatedSession);
            }
        }
        #endregion

        #region Delete Session
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id!";
                return RedirectToAction(nameof(Index));
            }

            var exists = _sessionServices.GetSessionById(id);
            if (exists is null)
            {
                TempData["ErrorMessage"] = "Session Not Found!";
                return RedirectToAction(nameof(Index));
            }

            var sessionToDelete = _sessionServices.GetSessionToDelete(id);
            if (sessionToDelete is null)
            {
                TempData["ErrorMessage"] = "You can't delete an ongoing session or a session with active bookings.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View(sessionToDelete);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id!";
                return RedirectToAction(nameof(Index));
            }

            var exists = _sessionServices.GetSessionById(id);
            if (exists is null)
            {
                TempData["ErrorMessage"] = "Session Not Found!";
                return RedirectToAction(nameof(Index));
            }
            var sessionToDelete = _sessionServices.GetSessionToDelete(id);
            if (sessionToDelete is null)
            {
                TempData["ErrorMessage"] = "You can't delete an ongoing session or a session with active bookings.";
                return RedirectToAction(nameof(Index));
            }

            var result = _sessionServices.RemoveSession(id);
            TempData["SuccessMessage"] = result ? "Session Deleted Successfully!" : null;
            TempData["ErrorMessage"] = result ? null : "Failed to Delete Session!";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper Methods

        private void LoadDropdownsTrainers()
        {
            var Trainers = _sessionServices.GetTrainerForSession();
            ViewBag.Trainers = new SelectList(Trainers,"Id" , "Name");
        }

        private void LoadDropdownsCategories()
        {
            var Categories = _sessionServices.GetCategoryForSession();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");

        }


        #endregion


    }
}
