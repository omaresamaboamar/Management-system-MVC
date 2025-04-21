using AssignmentMVC.BLL.DTO;
using AssignmentMVC.BLL.Service;
using AssignmentMVC.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AssignmentMVC.PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService, ILogger<DepartmentController> _logger, IWebHostEnvironment _environment) : Controller
    {
        // private readonly IDepartmentService _departmentService = departmentService;

        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartment();
            return View(department);
        }

        #region create Department

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    int result = _departmentService.AddDepartment(departmentDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department can't !!");

                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(departmentDto);
        }
        #endregion
        #region Department Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if ((!id.HasValue)) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);

        }
        #endregion

        #region Department Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if ((!id.HasValue)) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,
            };
            return View(departmentViewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id, DepartmentEditViewModel viewModel)
        {
            if(!ModelState.IsValid) return BadRequest();
            try
            {
                var updateDepartment = new UpdateDepartmentDto()
                {
                    Id = id.Value,
                    Name = viewModel.Name,
                    Code = viewModel.Code,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };
                var result = _departmentService.UpdateDepartment(updateDepartment);
                if (result > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department can't !!");
                }

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(viewModel);
        }
       #endregion
    }
}
