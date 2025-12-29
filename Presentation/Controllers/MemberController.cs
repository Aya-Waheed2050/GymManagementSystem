namespace Presentation.Controllers
{
    [Authorize]
    public class MemberController(IMemberServices _memberServices) 
        : Controller
    {

        #region GetAllMembers
        public IActionResult Index()
        {
            var members = _memberServices.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get Member Details

        public IActionResult MemberDetails(int id) {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction(nameof(Index));
            }

            var memberDetails = _memberServices.GetMemberDetails(id);
            if (memberDetails == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(memberDetails);
        }

        #endregion

        #region Get Health Record
        public IActionResult HealthRecordDetails(int id) 
        { 
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction(nameof(Index));
            }

            var healthRecord = _memberServices.GetMemberHealthDetails(id);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = "Health record not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(healthRecord);
        }
        #endregion

        #region Create Member
        public IActionResult Create() 
        {
            return View();
        }

        public IActionResult CreateMember(CreateMemberViewModel CreatedMember) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), CreatedMember);
            }

            bool Result = _memberServices.CreateMember(CreatedMember);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Create Member ,Check Email and Phone!!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edite Member
        public IActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberServices.UpdateMemberById(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Health record not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult MemberEdit([FromRoute]int id, UpdateMemberViewModel updatedMember) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(updatedMember);
            }
            var Result = _memberServices.UpdateMemberDetails(id, updatedMember);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Update Member!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Member
        public IActionResult Delete(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberServices.GetMemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = Member.Name;
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmedDelete([FromForm]int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction(nameof(Index));
            }
            var Result = _memberServices.DeleteMember(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Delete Member!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}