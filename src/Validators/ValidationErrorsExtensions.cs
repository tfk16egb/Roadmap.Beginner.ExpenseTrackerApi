namespace Roadmap.Beginner.ExpenseTrackerApi.src.Validators;

public static class ValidationErrorsExtensions
{
    public static HttpValidationProblemDetails ToErrors(this IDictionary<string, string[]> validationErrors, string instance = default)
        => new HttpValidationProblemDetails(validationErrors)
        {
            Title = "Validation failed",
            Detail = "One or more validation errors occurred.",
            Instance = instance
        };

}
