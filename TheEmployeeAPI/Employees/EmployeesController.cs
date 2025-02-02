using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheEmployeeAPI;

public class EmployeesController : BaseController
{
    private readonly ILogger<EmployeesController> _logger;
    private readonly AppDbContext _dbContext;

    public EmployeesController(
        ILogger<EmployeesController> logger,
        AppDbContext dbContext)
    {
        _logger = logger;
        this._dbContext = dbContext;
    }

    /// <summary>
    /// Get all employees.
    /// </summary>
    /// <returns>An array of all employees.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetEmployeeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllEmployees([FromQuery] GetAllEmployeesRequest request)
    {
        int page = request?.Page ?? 1;
        int numberOfRecords = request?.RecordsPerPage ?? 100;

        IQueryable<Employee> query = _dbContext.Employees
            .Include(e => e.Benefits)
            .Skip((page - 1) * numberOfRecords)
            .Take(numberOfRecords);

        if (request != null)
        {
            if (!string.IsNullOrWhiteSpace(request.FirstNameContains))
            {
                query = query.Where(e => e.FirstName.Contains(request.FirstNameContains));
            }
            
            if (!string.IsNullOrWhiteSpace(request.LastNameContains))
            {
                query = query.Where(e => e.LastName.Contains(request.LastNameContains));
            }
        }

        var employees = await query.ToArrayAsync();

        return Ok(employees.Select(EmployeeToGetEmployeeResponse));
    }

    /// <summary>
    /// Gets an employee by ID.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <returns>The single employee record.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _dbContext.Employees.SingleOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            return NotFound();
        }

        var employeeResponse = EmployeeToGetEmployeeResponse(employee);
        return Ok(employeeResponse);
    }

/*
    [HttpPost]
    [ProducesResponseType(typeof(GetEmployeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult CreateEmployee([FromBody] CreateEmployeeRequest employeeRequest)
    {
        var newEmployee = new Employee
        {
            FirstName = employeeRequest.FirstName!,
            LastName = employeeRequest.LastName!,
            SocialSecurityNumber = employeeRequest.SocialSecurityNumber,
            Address1 = employeeRequest.Address1,
            Address2 = employeeRequest.Address2,
            City = employeeRequest.City,
            State = employeeRequest.State,
            ZipCode = employeeRequest.ZipCode,
            PhoneNumber = employeeRequest.PhoneNumber,
            Email = employeeRequest.Email
        };

        _repository.Create(newEmployee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GetEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeResponse employeeRequest)
    {
        _logger.LogInformation("Updating employee with id {EmployeeId}", id);
        var existingEmployee = _repository.GetById(id);
        if (existingEmployee == null)
        {
            _logger.LogWarning("Employee with id {EmployeeId} was not found", id);
            return NotFound();
        }

        existingEmployee.Address1 = employeeRequest.Address1;
        existingEmployee.Address2 = employeeRequest.Address2;
        existingEmployee.City = employeeRequest.City;
        existingEmployee.State = employeeRequest.State;
        existingEmployee.ZipCode = employeeRequest.ZipCode;
        existingEmployee.PhoneNumber = employeeRequest.PhoneNumber;
        existingEmployee.Email = employeeRequest.Email;

        _repository.Update(existingEmployee);
        return Ok(existingEmployee);
    }

        /// <summary>
    /// Gets the benefits for an employee.
    /// </summary>
    /// <param name="employeeId">The ID to get the benefits for.</param>
    /// <returns>The benefits for that employee.</returns>
    [HttpGet("{employeeId}/benefits")]
    [ProducesResponseType(typeof(IEnumerable<GetEmployeeResponseEmployeeBenefit>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetBenefitsForEmployee(int employeeId)
    {
        var employee = _repository.GetById(employeeId);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee.Benefits);
    }
*/
    private GetEmployeeResponse EmployeeToGetEmployeeResponse(Employee employee)
    {
        return new GetEmployeeResponse
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Address1 = employee.Address1,
            Address2 = employee.Address2,
            City = employee.City,
            State = employee.State,
            ZipCode = employee.ZipCode,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
            Benefits = employee.Benefits.Select(benefit => new GetEmployeeResponseEmployeeBenefit
            {
                Id = benefit.Id,
                EmployeeId = benefit.EmployeeId,
                BenefitType = benefit.BenefitType,
                Cost = benefit.Cost
            }).ToList()
        };
    }
}
