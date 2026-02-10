using System.ComponentModel.DataAnnotations;

namespace CapStoneProject.Attributes
{
    public class AllowedEmailDomainsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedDomains = { "@gmail.com", "@divami.com", "@email.com"};

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var email = value.ToString()!.ToLower();

            foreach (var domain in _allowedDomains)
            {
                if (email.EndsWith(domain))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult($"Email must end with {string.Join(" or ", _allowedDomains)}");
        }
    }
}
