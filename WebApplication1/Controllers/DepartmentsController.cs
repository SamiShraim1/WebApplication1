using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.DTOs.Departments;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = context.Departments.Select(
                x => new DepartmentsGetAllDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                }
            );

            return Ok(departments);
        }


        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Employee not found");
            }
            var departmentDto = new DepartmentsGetDto
            {
                Id = department.Id,
                Name = department.Name
            };

            return Ok(department);
        }

        [HttpPost("Create")]
        public IActionResult Create(DepartmentsCreateDto depDto)
        {
            Department dep = new Department()
            {
                Name = depDto.Name
            };

            context.Departments.Add(dep);
            context.SaveChanges();

            return Ok(dep);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, DepartmentUpdateDto departmentDto)
        {
            var current = context.Departments.Find(id);
            if (current == null)
            {
                return NotFound("Department not found");
            }
            var updatedDepartmentDto = new DepartmentUpdateDto
            {
                Name = current.Name
            };
            context.SaveChanges();
            return Ok(current);
        }




        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department not found");
            }

            context.Departments.Remove(department);
            context.SaveChanges();

            return Ok("Department removed successfully");
        }





    }
}