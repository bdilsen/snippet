﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Server.Models;

namespace WebApplication21.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeeRepository employeeRepository;

		public EmployeesController(IEmployeeRepository employeeRepository)
		{
			this.employeeRepository = employeeRepository;
		}

		[HttpGet]
		public async Task<ActionResult<Employee >> CreateEmployee(Employee employee)
{
			try
			{
				if (employee == null)
					return BadRequest();

				var createdEmployee = await employeeRepository.AddEmployee(employee);

				return CreatedAtAction(nameof(GetEmployees), new { id = createdEmployee.EmployeeId }, createdEmployee);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee record");
			}
		}

		[HttpGet]
		public async Task<ActionResult> GetEmployees()
		{
			try
			{
				return Ok(await employeeRepository.GetEmployees());
			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
			}
		}
	}
}
