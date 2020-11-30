using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team_1_Project.Models
{
    public class peerRecognition
    {
        
        public int ID { get; set; }
       
        [Display(Name = "ID of the Person giving the recognition")]
        public Guid recognizor { get; set; }

        [Display(Name = "ID of the Person receiving the recognition")]
        public Guid recognized { get; set; }

        [Display(Name ="Date recognition given")]
        public DateTime recognizationDate { get; set; }
    }
}