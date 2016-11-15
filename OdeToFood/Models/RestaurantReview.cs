using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class MaxWordsAttribute : ValidationAttribute   //custom attribute (this is a bad place for it, but just for example)
    {
        private readonly int _maxWords;
        public MaxWordsAttribute(int maxWords) : base("{0} has too many words.")
        {
            _maxWords = maxWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > _maxWords)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
    public class RestaurantReview : IValidatableObject
    {
        public int Id { get; set; }

        [Range(1,10, ErrorMessage = "That's the incorrect range.")]
        public int Rating { get; set; }

        [Required]
        [StringLength(1024)]
        public string Body { get; set; }

        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "anonymous")]
        [MaxWords(3)]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // for example, to keep away reviewed named Bill who always gives low reviews
            if (Rating < 2 && ReviewerName.ToLower().StartsWith("bill"))
            {
                yield return new ValidationResult("Sorry Bill, you can't do that thing.");
            }
        }
    }
}