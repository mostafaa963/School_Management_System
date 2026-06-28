using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Services.UnitOfWork;
using School_Management_System.Utilities;
using System.Numerics;

namespace School_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public SubjectsController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? filter)
        {
      
            var subjects = await _unitOfWork.Subject.GetAllAsync();
            if (filter != null)
                subjects = subjects.Where(e => e.SubjectName.ToLower().Contains(filter.ToLower().Trim()));
            return View(subjects);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectVM createSubjectVM)
        {
            if(!ModelState.IsValid) {
                return View(createSubjectVM);
            }
           await _unitOfWork.Subject.AddAsync(new Subject()
            {
                SubjectName=createSubjectVM.SubjectName,
                SubjectCode= createSubjectVM.SubjectCode,
            });
           await _unitOfWork.SaveChangeAsync();

            TempData["success_notification"] = "Add Subject Successfully ";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var subject = await _unitOfWork.Subject.GetOneById(Id);
            if(subject is  null)
                return NotFound ();

            var updateSubject = new UpdateSubjectVM {
              SubjectID = Id,
              SubjectCode= subject.SubjectCode,
              SubjectName= subject.SubjectName,
            };

            return View(updateSubject);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateSubjectVM updateSubjectVM)
        {
            if(!ModelState.IsValid)
                return View(updateSubjectVM);
            var subject = new Subject
            {
                SubjectID= updateSubjectVM.SubjectID,
                SubjectCode= updateSubjectVM.SubjectCode,
                SubjectName= updateSubjectVM.SubjectName,
            };
            _unitOfWork.Subject.Update(subject);
            await _unitOfWork.SaveChangeAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var subjectDetails = await _unitOfWork.Subject.GetOneById(Id);
            if (subjectDetails is null)
                return NotFound();

            return View(subjectDetails);
        }
    }
}
