using CompanyAPI.Models;
using CompanyAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly CompanyContext _context;

        public EmployeeController(CompanyContext context)
        {
            _context = context;
        }

        #region [List]
        /// <summary>
        /// https://localhost:7178/api/v1/Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Employee> List()
        {
            try
            {
                var employee = _context.Employees
                    .Select(e => new Employee
                    {
                        EmployeeId = e.EmployeeId,
                        EmployeeName = e.EmployeeName,
                        DepartmentId = e.DepartmentId,
                        Code = e.Code,
                        HomeNumber = e.HomeNumber,
                        IdentificationNumber = e.IdentificationNumber,
                        IsManager = e.IsManager,
                        IsActive = e.IsActive,
                        Salary = e.Salary,
                        MobileNumber = e.MobileNumber,
                        WorkNumber = e.WorkNumber,
                        Children = e.Children,
                        Department = e.Department,

                    })
                    .ToList();

                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        #endregion

        #region [View]
        /// <summary>
        /// https://localhost:7178/api/v1/Employee/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult View(int id)
        {
            try
            {
                var employee = _context.Employees
                    .Where(e => e.EmployeeId == id)
                    .Select(e => new Employee
                    {
                        EmployeeId = e.EmployeeId,
                        EmployeeName = e.EmployeeName,
                        DepartmentId = e.DepartmentId,
                        Code = e.Code,
                        HomeNumber = e.HomeNumber,
                        IdentificationNumber = e.IdentificationNumber,
                        IsManager = e.IsManager,
                        IsActive = e.IsActive,
                        Salary = e.Salary,
                        MobileNumber = e.MobileNumber,
                        WorkNumber = e.WorkNumber,
                        Children = e.Children
                    })
                    .FirstOrDefault();

                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return Ok($"No Employee with id = {id}");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Add]
        /// <summary>
        /// https://localhost:7178/api/v1/Employee
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = _context.Employees
                    .Select(e => new Employee
                    {
                        EmployeeName = employeeDTO.EmployeeName,
                        DepartmentId = employeeDTO.DepartmentId,
                        Code = employeeDTO.Code,
                        HomeNumber = employeeDTO.HomeNumber,
                        IdentificationNumber = employeeDTO.IdentificationNumber,
                        IsManager = employeeDTO.IsManager,
                        IsActive = employeeDTO.IsActive,
                        Salary = employeeDTO.Salary,
                        MobileNumber = employeeDTO.MobileNumber,
                        WorkNumber = employeeDTO.WorkNumber,
                    })
                    .FirstOrDefault();
                if (employee != null)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return Ok(employee);
                }
                else
                    return Ok("There is no Employee in body");
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Edit]
        /// <summary>
        /// https://localhost:7178/api/v1/Employee/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IActionResult Edit(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = _context.Employees
                    .Select(e => new Employee
                    {
                        EmployeeId = id,
                        EmployeeName = employeeDTO.EmployeeName,
                        DepartmentId = employeeDTO.DepartmentId,
                        Code = employeeDTO.Code,
                        HomeNumber = employeeDTO.HomeNumber,
                        IdentificationNumber = employeeDTO.IdentificationNumber,
                        IsManager = employeeDTO.IsManager,
                        IsActive = employeeDTO.IsActive,
                        Salary = employeeDTO.Salary,
                        MobileNumber = employeeDTO.MobileNumber,
                        WorkNumber = employeeDTO.WorkNumber,
                    })
                    .FirstOrDefault();
                if (employee != null)
                {
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    return Ok(employee);
                }
                else
                    return Ok($"There is no Employee in body with ID = {id}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// https://localhost:7178/api/v1/Employee/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult actionResult(int id)
        {
            try
            {

                var employee = _context.Employees
                    .Where(e => e.EmployeeId == id)
                    .FirstOrDefault();

                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    return Ok($"Employee with id = {id} deleted successfully!!");
                }
                else
                {
                    return Ok($"There is no employee with id = {id}");
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion
    }
}
