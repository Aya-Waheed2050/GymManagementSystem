namespace Presentation.Controllers
{
    [Authorize]
    public class TrainerController(ITrainerServices _trainerServices) 
        : Controller
    {

        #region Get All Trainers
        public IActionResult Index()
        {
            var trainers = _trainerServices.GetAllTrainers();
            return View(trainers);
        }
        #endregion

        #region Get Trainer Details
        public IActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer ID.";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerServices.GetTrainerDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }
        #endregion

        #region Create Trainer
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), createTrainer);
            }

            bool Result = _trainerServices.CreateTrainer(createTrainer);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Create Trainer!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Trainer

        public IActionResult EditTrainer(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer ID.";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerServices.GetTrainerToUpdate(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        
        [HttpPost]
        public IActionResult EditTrainer([FromRoute]int id, UpdateTrainerViewModel updateTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(updateTrainer);
            }

            bool Result = _trainerServices.UpdateTrainerDetails(id, updateTrainer);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Edited Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Edit Trainer!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Remove Trainer

        public IActionResult DeleteTrainer(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer ID.";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerServices.GetTrainerDetails(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            ViewBag.TrainerName = trainer.Name;
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmDelete([FromForm]int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer ID.";
                return RedirectToAction(nameof(Index));
            }
            bool Result = _trainerServices.RemoveTrainer(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Delete Trainer!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
