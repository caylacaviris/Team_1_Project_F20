using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team_1_Project.Models
{
    public class coreValuesRecognition
    {
        public int ID { get; set; }
        
        [Display(Name = "Core Value Recognized")]
        public CoreValues award { get; set; }

        [Display(Name = "ID of Person Giving the Recognition")]
        public Guid recognizorID { get; set; }
        [ForeignKey ("recognizorID")]
        public virtual userData recognizor { get; set; }

        [Display(Name = "ID of Person Receiving the Recognition")]
        public Guid recognizedID { get; set; }
        [ForeignKey ("recognizedID")]
        public virtual userData recognized { get; set; }

        [Display(Name = "Date Recognition Given")]
        public DateTime recognizationDate { get; set; }

        public enum CoreValues
        {
            Excellence = 1,
            Integrity = 2,
            Stewardship = 3,
            Innovate = 4,
            Balance = 5
        }

    }
}