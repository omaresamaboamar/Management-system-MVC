using AssignmentMVC.BLL.DTO.DepartmentsDTO;
using AssignmentMVC.BLL.DTO.EmployeesDTO;
using AssignmentMVC.BLL.Service.ServiceClasses;
using AssignmentMVC.BLL.Service.ServicesInterface;
using AssignmentMVC.DAL.Models.Employees;
using AssignmentMVC.PL.ViewModels;
using AssignmentMVC.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.PL.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService ,ILogger<EmployeeController> _logger,IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            ViewData["Message"] = "Hello View Data";

            ViewBag.Message = "Hello View Bag";
            return View(Employees);
        }

        #region Create Employee 

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(EmployeeViewModels employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeModelsView = new CreateEmployeeDto()
                    {
                        Name = employeeDto.Name,
                        Address = employeeDto.Address,
                        Age = employeeDto.Age,
                        Email = employeeDto.Email,
                        Salary = employeeDto.Salary,
                        IsActive = employeeDto.IsActive,
                        PhoneNumber = employeeDto.PhoneNumber,
                        Gender = employeeDto.Gender,
                        EmployeeType = employeeDto.EmployeeType,
                        HiringDate = employeeDto.HiringDate,
                    };
                    int result = _employeeService.CreateEmployee(employeeModelsView);
                    if (result > 0)
                    {
                        TempData["Message"] = "Employee Created Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Message"] = "Employee can't Created!!";
                        ModelState.AddModelError(string.Empty, "Department can't !!");
                        return RedirectToAction(nameof(Index));
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
            return View(employeeDto);
        }
        #endregion
        #region Details of Employee
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            else return View(employee);
        }
        #endregion
        #region Edit Employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeDto = new EmployeeViewModels()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
            };
            return View(employeeDto);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModels viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var employeeDto = new UpdateEmployeeDto()
                {
                    Id = id.Value,
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Age = viewModel.Age,
                    Salary = viewModel.Salary,
                    IsActive = viewModel.IsActive,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    HiringDate = viewModel.HiringDate,
                    Gender = viewModel.Gender,
                    EmployeeType = viewModel.EmployeeType,
                };

            
                var result = _employeeService.UpdateEmployee(employeeDto);
                if (result > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can't Edit!!");
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

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            try
            {
                var deleted = _employeeService.DeleteEmployee(id.Value);
                if (deleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can't delete!!");
                    return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }
    }
}
