using AssignmentMVC.BLL.DTO;
using AssignmentMVC.BLL.DTO.DepartmentsDTO;
using AssignmentMVC.BLL.Service.ServicesInterface;
using AssignmentMVC.PL.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
        public IActionResult Create(DepartmentViewModels departmentDto)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    var departmentModelsView = new CreateDepartmentDto()
                    {
                        Name = departmentDto.Name,
                        Code = departmentDto.Code,
                        Description = departmentDto.Description,
                        DateOfCreation = departmentDto.DateOfCreation
                    };
                    int result = _departmentService.AddDepartment(departmentModelsView);
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
            var departmentViewModel = new DepartmentViewModels()
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
        public IActionResult Edit([FromRoute]int? id, DepartmentViewModels viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);
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
        #region Delete Department
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if(!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
             if(id == 0) return BadRequest();
            try
            {
                bool Result = _departmentService.DeleteDepartment(id);
                if (Result) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not Deleted!!");
                    return RedirectToAction(nameof(Delete),new {id});
                }

            }
            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("Erorr");
                }

            }

        }
        #endregion
    }
}
