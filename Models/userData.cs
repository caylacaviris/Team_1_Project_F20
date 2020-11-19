using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team_1_Project.Models
{
    public class userData
    {

        [Required]
        public Guid ID { get; set; }

        [Required]
        [Display (Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Full Name")]
        public string fullName
        {
            get
            {
                return lastName + ", " + firstName;
            }
        }
        [Required]
        [Phone]
        [Display(Name = "Number")]
        public string phoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Office Location")]
        public string officeLocation { get; set; }

        [Display(Name = "Position")]
        public string position { get; set; }

        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime hireDate { get; set; }

        [ForeignKey("recognizedID")]
        public ICollection<coreValuesRecognition> recognizee { get; set; }

        [ForeignKey("recognizorID")]

        public ICollection<coreValuesRecognition> recognizer { get; set; }
    }
}