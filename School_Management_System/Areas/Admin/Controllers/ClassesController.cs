using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Services.UnitOfWork;
using School_Management_System.Utilities;

namespace School_Management_System.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class ClassesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManger;

        public ClassesController(UserManager<User> userManger, IUnitOfWork unitOfWork)
        {
            _userManger = userManger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string? filter = null)
        {
            var classes = await _unitOfWork.Class.GetAllAsync();
            if (filter != null)
                classes = classes.Where(e => e.ClassName.ToLower().Contains(filter.ToLower().Trim()));

            return View(classes.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            return View(new CreateClassVM());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClassVM createClassVM)
        {
            if (!ModelState.IsValid)
                return View(createClassVM);

            var createClass = new Class()
            {
                ClassName = createClassVM.ClassName,
                Section = createClassVM.Section,
                Capacity = createClassVM.Capacity,
            };

            await _unitOfWork.Class.AddAsync(createClass);
            await _unitOfWork.SaveChangeAsync();

            TempData["success_notification"] = "Add Class Successfully ";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var findClass = await _unitOfWork.Class.GetOneById(Id);
            if (findClass == null)
                return NotFound();
            var updateClass = new UpdateClassVM
            {
                Id = Id,
                Section = findClass.Section,
                Capacity = findClass.Capacity,
                ClassName = findClass.ClassName,
            };
            return View(updateClass);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateClassVM updateClassVM)
        {
            if (!ModelState.IsValid)
                return View(updateClassVM);


            _unitOfWork.Class.Update(new Class
            {
                ClassID = updateClassVM.Id,
                Section = updateClassVM.Section,
                Capacity = updateClassVM.Capacity,
                ClassName = updateClassVM.ClassName,
            });
            await _unitOfWork.SaveChangeAsync();

            TempData["success_notification"] = "Update Class Successfully ";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var classDetails = await _unitOfWork.Class.GetFirstOne(e=>e.ClassID==Id ,isNoTracking:true,include:e=>e.Students);

            if (classDetails is null)
                return NotFound();
            var details = new ClassDetailsVM
            {
                Id =Id,
                ClassName = classDetails.ClassName,
                Capacity = classDetails.Capacity,
                Section = classDetails.Section,
                Count = classDetails.Students.Count
            };
            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var findClass = await _unitOfWork.Class.GetOneById(Id);
                if (findClass is null)
                    return NotFound();
                _unitOfWork.Class.Delete(findClass);
                await _unitOfWork.SaveChangeAsync();
                TempData["success"] = "Delete  Class Successfully ";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error_notification"] = "You Must Delete Student first";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
