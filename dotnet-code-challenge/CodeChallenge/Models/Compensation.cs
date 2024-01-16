using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        [Key]
        public String EmployeeId { get; set; }
        public double Salary { get; set; }
        
        // Should be denoted as a time from epoch string
        public String EffectiveDate { get; set; }
    }
}
