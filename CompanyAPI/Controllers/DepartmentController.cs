using CompanyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly CompanyContext _context;

        public DepartmentController(CompanyContext context) 
        {
            _context = context;
        }

        #region [List]
        /// <summary>
        /// https://localhost:7178/api/v1/Department
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Department> List()
        {
            try
            {
                return _context.Departments.Select(d => new Department
                {
                    DepartmentId = d.DepartmentId,
                    Budget = d.Budget,
                    DepartmentName = d.DepartmentName,
                    DepartmentNumber = d.DepartmentNumber,
                    Employees=d.Employees                 
                }).ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        #endregion

        #region [View]
        /// <summary>
        /// https://localhost:7178/v1/api/Department/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult View(int id)
        {
            try
            {
                var department = _context.Departments
                    .Where(d => d.DepartmentId == id)
                    .Select(d=>new Department
                    {
                        DepartmentId=id,
                        Budget = d.Budget,
                        DepartmentName = d.DepartmentName,
                        DepartmentNumber = d.DepartmentNumber,
                        Employees=d.Employees
                    })
                    .FirstOrDefault();

                return Ok(department);
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Add]
        /// <summary>
        /// https://localhost:7178/api/v1/Department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] Department department)
        {

            try
            {
                var Department = _context.Departments.Select(d => new Department
                {
                    Budget = department.Budget,
                    DepartmentName = department.DepartmentName,
                    DepartmentNumber = department.DepartmentNumber,
                }).FirstOrDefault();

                _context.Add(Department);
                _context.SaveChanges();

                return Ok(Department);
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Edit]
        /// <summary>
        /// https://localhost:7178/v1/api/Department/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IActionResult Edit(int id, [FromBody] Department department)
        {
            try
            {
                var Department = _context.Departments
                    .Where(d => d.DepartmentId == id)
                    .Select(d => new Department
                    {
                        DepartmentId = id,
                        Budget = department.Budget,
                        DepartmentName = department.DepartmentName,
                        DepartmentNumber = department.DepartmentNumber,
                    }).FirstOrDefault();

                if(Department != null)
                {
                    _context.Departments.Update(Department);
                    _context.SaveChanges();
                    return Ok(Department);
                }
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// https://localhost:7178/v1/api/Department/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var department=_context.Departments
                    .Where(d=>d.DepartmentId==id)
                    .FirstOrDefault();
                if(department != null)
                {
                    _context.Departments.Remove(department);
                    _context.SaveChanges();
                    return Ok($"Department with id = {id} has been deleted");
                }
                else
                    return Ok($"There is no department with ID = {id}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion
    }
}
