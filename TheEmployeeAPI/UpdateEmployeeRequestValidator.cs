using FluentValidation;

public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeResponse>
{
    /*private readonly HttpContext _httpContext;
    private readonly IRepository<Employee> _repository;

    public UpdateEmployeeRequestValidator(IHttpContextAccessor httpContextAccessor, IRepository<Employee> repository)
    {
        _httpContext = httpContextAccessor.HttpContext!;
        _repository = repository;

        RuleFor(x => x.Address1)
        .MustAsync(NotBeEmptyIfItIsSetOnEmployeeAlreadyAsync)
        .WithMessage("Address1 must not be empty.");
    }

    private async Task<bool> NotBeEmptyIfItIsSetOnEmployeeAlreadyAsync(string? address, CancellationToken token)
    {
        await Task.CompletedTask; // will actually make this async later
        var id = Convert.ToInt32(_httpContext.Request.RouteValues["id"]);
        var employee = _repository.GetById(id);

        if (employee!.Address1 != null && string.IsNullOrWhiteSpace(address))
        {
            return false;
        }

        return true;
    }*/
}