using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Services.UnitOfWork;
using School_Management_System.Utilities;

namespace School_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    [Authorize(Roles = $"{RolesNames.ADMIN_ROLES}")]
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TeachersController> _logger;
        private readonly UserManager<User> _userManager;

        public TeachersController(ILogger<TeachersController> logger, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public async Task<IActionResult> Index(string filter)
        {
            var teacher = await _unitOfWork.Teacher.GetAllAsync(include: e => e.Allocations);
            if (filter is not null)
                teacher = teacher.Where(e => e.FirstName.ToLower().Contains(filter.ToLower().Trim()));

            return View(teacher);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new CreateTeacherVM
            {
                HireDate = DateTime.Today,
                Users = await _userManager.GetUsersInRoleAsync(RolesNames.TEACHER_ROLES)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeacherVM createTeacherVM)
        {
            if (!ModelState.IsValid)
            {

                createTeacherVM.Users = await _userManager.GetUsersInRoleAsync(RolesNames.TEACHER_ROLES);
                return View(createTeacherVM);
            }

            var user = await _userManager.FindByIdAsync(createTeacherVM.UserID);
            if (user == null)
            {
                ModelState.AddModelError("", "Selected User does not exist.");

                createTeacherVM.Users = await _userManager.GetUsersInRoleAsync(RolesNames.TEACHER_ROLES);
                return View(createTeacherVM);
            }

            await _unitOfWork.Teacher.AddAsync(new Teacher
            {
                FirstName = createTeacherVM.FirstName,
                LastName = createTeacherVM.LastName,
                Qualification = createTeacherVM.Qualification,
                PhoneNumber = createTeacherVM.PhoneNumber,
                HireDate = createTeacherVM.HireDate,
                UserID = createTeacherVM.UserID
            });

            await _unitOfWork.SaveChangeAsync();
            TempData["success_notification"] = "Add Teacher Successfully ";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var teacher = await _unitOfWork.Teacher.GetOneById(Id);
            if (teacher is null)
                return NotFound();

            var updateTeacher = new UpdateTeacherVM
            {
                Id = teacher.TeacherID,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Qualification = teacher.Qualification,
                PhoneNumber = teacher.PhoneNumber,
            };
            return View(updateTeacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateTeacherVM updateTeacherVM)
        {
            if (!ModelState.IsValid)
                return View(updateTeacherVM);

            var teacher = await _unitOfWork.Teacher.GetOneById(updateTeacherVM.Id);
            if (teacher is null)
                return NotFound();
            teacher.FirstName = updateTeacherVM.FirstName;
            teacher.LastName = updateTeacherVM.LastName;
            teacher.PhoneNumber = updateTeacherVM.PhoneNumber;
            teacher.Qualification = updateTeacherVM.Qualification;
            try
            {
                _unitOfWork.Teacher.Update(teacher);
                await _unitOfWork.SaveChangeAsync();
                TempData["success_notification"] = "Teacher profile updated successfully.";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating teacher with ID {TeacherId}", updateTeacherVM.Id);
                ModelState.AddModelError(string.Empty, "Unable to save changes. Please try again, and if the problem persists, contact your system administrator.");
                return View(updateTeacherVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var teacher = await _unitOfWork.Teacher.GetFirstOne(e => e.TeacherID == id, isNoTracking: true);
            if (teacher is null) return NotFound();

            return View(new TeacherDetailsVM
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber = teacher.PhoneNumber,
                Qualification = teacher.Qualification,
                HireDate = teacher.HireDate,
            });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _unitOfWork.Teacher.GetFirstOne(e => e.TeacherID == id, isNoTracking: true);
            if (teacher is null) return NotFound();
            try
            {
                _unitOfWork.Teacher.Delete(teacher);
                await _unitOfWork.SaveChangeAsync();
                TempData["success_notification"] = "Teacher profile Deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Deleting teacher with ID {TeacherId}", teacher.TeacherID);
                TempData["error_notification"] = $"An error occurred while Deleting teacher with ID {teacher.TeacherID}";
            }
                return RedirectToAction(nameof(Index));
        }
    }
}
