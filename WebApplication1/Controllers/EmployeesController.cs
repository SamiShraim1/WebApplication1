using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.DTOs.Employees;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employees = context.Employees.ToList();
            var response = employees.Adapt<IEnumerable<EmployeesGetAllDto>>();
            return Ok(employees);
        }

        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            var employeeDto = employee.Adapt<EmployeesGetDto>();
            return Ok(employeeDto);
        }

        [HttpPost("Create")]
        public IActionResult Create(EmployeesCreateDto empDto)
        {
            var employee = empDto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, EmployeesGetDto employeeDto)
        {
            var current = context.Employees.Find(id);
            if (current == null)
            {
                return NotFound("Employee not found");
            }

            employeeDto.Adapt(current);

            context.SaveChanges();

            var updatedEmployeeDto = current.Adapt<EmployeesGetDto>();

            return Ok(updatedEmployeeDto);
        }


        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            context.Employees.Remove(employee);
            context.SaveChanges();

            return Ok("Employee removed successfully");
        }





    }
}
