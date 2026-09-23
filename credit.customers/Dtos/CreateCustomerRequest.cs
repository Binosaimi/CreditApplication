using System.ComponentModel.DataAnnotations;


namespace credit.customers.Dtos;

public record CreateCustomerRequest(
    [Required(ErrorMessage = "Name is required."), MaxLength(40)] string Name,
    [RegularExpression(@"^\d{12}$", ErrorMessage = "Civil ID must contain exactly 12 digits."), Required(ErrorMessage = "Civil ID is required.")] string CivilId,
    [Required(ErrorMessage = "Dob is required.")] DateOnly Dob
    );