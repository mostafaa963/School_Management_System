using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Services.UnitOfWork;
using School_Management_System.Utilities;

namespace School_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    [Authorize(Roles = $"{RolesNames.ADMIN_ROLES}")]
    public class TeacherAllocationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TeacherAllocationsController> _logger;
        private readonly UserManager<User> _userManager;

        public TeacherAllocationsController(ILogger<TeacherAllocationsController> logger, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string? filter)
        {
            var allocations = await _unitOfWork.TeacherAllocation.
                GetAllAsync(include: [c=> c.Class
                ,t=>t.Teacher
                ,s=>s.Subject]);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var cleanFilter = filter.Trim().ToLower();
                allocations = allocations.Where(e =>
                    e.Teacher?.FirstName != null &&
                    e.Teacher.FirstName.ToLower().Contains(cleanFilter)
                );
            }
            var allocationIndex = allocations.Select(e => new TeacherAllocationsVM
            {
                Class= e.Class,
                Subject= e.Subject,
                Teacher= e.Teacher,
            });
                return View(allocationIndex);
            
        }
    }
}
