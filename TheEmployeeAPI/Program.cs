var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IRepository<Employee>, EmployeeRepository>();

var app = builder.Build();

var employeeRoute = app.MapGroup("/employees");

employeeRoute.MapGet(string.Empty, (IRepository<Employee> repo) => {
    var employees = repo.GetAll();
    return Results.Ok(employees.Select(employee => new GetEmployeeResponse {
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Address1 = employee.Address1,
        Address2 = employee.Address2,
        City = employee.City,
        State = employee.State,
        ZipCode = employee.ZipCode,
        PhoneNumber = employee.PhoneNumber,
        Email = employee.Email
    }));
});

employeeRoute.MapGet("{id:int}", (int id, IRepository<Employee> repo) => {
    var employee = repo.GetById(id);
    if (employee == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new GetEmployeeResponse {
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Address1 = employee.Address1,
        Address2 = employee.Address2,
        City = employee.City,
        State = employee.State,
        ZipCode = employee.ZipCode,
        PhoneNumber = employee.PhoneNumber,
        Email = employee.Email
    });
});

employeeRoute.MapPost(string.Empty, (CreateEmployeeRequest employee, IRepository<Employee> repo) => {
    var newEmployee = new Employee {
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        SocialSecurityNumber = employee.SocialSecurityNumber ?? "123-45-6789",
        Address1 = employee.Address1,
        Address2 = employee.Address2,
        City = employee.City,
        State = employee.State,
        ZipCode = employee.ZipCode,
        PhoneNumber = employee.PhoneNumber,
        Email = employee.Email
    };
    repo.Create(newEmployee);
    return Results.Created($"/employees/{newEmployee.Id}", employee);
});

employeeRoute.MapPut("{id}", (UpdateEmployeeRequest employee, int id, IRepository<Employee> repo) => {
    var existingEmployee = repo.GetById(id);
    if (existingEmployee == null)
    {
        return Results.NotFound();
    }

    existingEmployee.Address1 = employee.Address1;
    existingEmployee.Address2 = employee.Address2;
    existingEmployee.City = employee.City;
    existingEmployee.State = employee.State;
    existingEmployee.ZipCode = employee.ZipCode;
    existingEmployee.PhoneNumber = employee.PhoneNumber;
    existingEmployee.Email = employee.Email;
    repo.Update(existingEmployee);

    return Results.Ok(existingEmployee);
});

app.Run();

public partial class Program {}