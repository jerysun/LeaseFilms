using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaseFilms.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = validationContext.ObjectInstance as Customer;
            if (customer == null)
                return new ValidationResult("Min 18 years validation failed");

            // The former is "not to choose", while the latter is "Pay as You Go"
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.Birthdate == null && customer.MembershipTypeId != 1)
                return new ValidationResult("Birthdate is required.");

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to gon on a membership.");
        }
    }
}