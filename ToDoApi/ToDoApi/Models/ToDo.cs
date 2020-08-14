using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public DateTime ExpiryDate { get; set; }     
        
        [Range(0,100, ErrorMessage = "Percent complete should be between 0 and 100.")]
        public double PercentComplete { get; set; }
    }
}
