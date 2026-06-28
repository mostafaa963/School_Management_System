using Microsoft.AspNetCore.Mvc;
using School_Management_System.Services.UnitOfWork;
using School_Management_System.Utilities;

namespace School_Management_System.Areas.Admin
{
    [Area(SD.ADMIN_AREA)]
    public class UniqueNameController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UniqueNameController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> CheckName(string SubjectCode)
        {
            if (SubjectCode == null)
                return Json(false);
            var checkIfExist = await _unitOfWork.Subject.GetFirstOne(e => e.SubjectCode == SubjectCode);
            if (checkIfExist == null)
                return Json(true);
            return Json(false);
        }
    }
}
