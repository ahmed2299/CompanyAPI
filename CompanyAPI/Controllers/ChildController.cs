using CompanyAPI.Models;
using CompanyAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly CompanyContext _context;

        public ChildController(CompanyContext context) 
        {
            _context = context;
        }

        #region [List]
        /// <summary>
        /// https://localhost:7178/api/v1/Child
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Child> List()
        {
            try
            {
                var child = _context.Children
                    .Select(c => new Child
                    {
                        ChildId=c.ChildId,
                        ChildName=c.ChildName,
                        ChildAge=c.ChildAge,
                        EmployeeId=c.EmployeeId,
                        Employee = c.Employee
                    })
                    .ToList();

                return child;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        #endregion

        #region [View]
        /// <summary>
        /// https://localhost:7178/api/v1/Child/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult View(int id)
        {
            try
            {
                var child = _context.Children
                    .Where(c => c.ChildId == id)
                    .Select(c => new Child
                    {
                        ChildId = c.ChildId,
                        ChildName = c.ChildName,
                        ChildAge = c.ChildAge,
                        EmployeeId = c.EmployeeId,
                        Employee = c.Employee
                    })
                    .FirstOrDefault();

                if (child != null)
                {
                    return Ok(child);
                }
                else
                {
                    return Ok($"No Child with id = {id}");
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
        /// https://localhost:7178/api/v1/Child
        /// </summary>
        /// <param name="childDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] ChildDTO childDTO)
        {
            try
            {
                var child = _context.Children
                    .Select(c => new Child
                    {
                        ChildName = childDTO.ChildName,
                        ChildAge = childDTO.ChildAge,
                        EmployeeId = childDTO.EmployeeId,
                    })
                    .FirstOrDefault();

                if (child != null)
                {
                    _context.Children.Add(child);
                    _context.SaveChanges();
                    return Ok(child);
                }
                else
                    return Ok("There is no Child in body");
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Edit]
        /// <summary>
        /// https://localhost:7178/api/v1/Child/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="childDTO"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IActionResult Edit(int id, [FromBody] ChildDTO childDTO)
        {
            try
            {
                var child = _context.Children
                    .Select(e => new Child
                    {
                        ChildId = id,
                        ChildName = childDTO.ChildName,
                        ChildAge = childDTO.ChildAge,
                        EmployeeId = childDTO.EmployeeId
                    })
                    .FirstOrDefault();
                if (child != null)
                {
                    _context.Children.Update(child);
                    _context.SaveChanges();
                    return Ok(child);
                }
                else
                    return Ok($"There is no Child in body with ID = {id}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// https://localhost:7178/api/v1/Child/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult actionResult(int id)
        {
            try
            {
                var child = _context.Children
                    .Where(c => c.ChildId == id)
                    .FirstOrDefault();
                if (child != null)
                {
                    _context.Children.Remove(child);
                    _context.SaveChanges();
                    return Ok($"Child with id = {id} deleted successfully!!");
                }
                else
                {
                    return Ok($"There is no child with id = {id}");
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
