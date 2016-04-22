using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IpagooLibrary.UI.Models
{
    public class BookSearch : IValidatableObject
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ISBN) && string.IsNullOrWhiteSpace(Title)
                && string.IsNullOrWhiteSpace(AuthorName) && string.IsNullOrWhiteSpace(Genre))
                yield return new ValidationResult("Please enter a search criteria", new[] { "ISBN", "Title", "AuthorName", "Genre" });

           
        }
    }
}