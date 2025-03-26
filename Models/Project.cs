using System;
using System.ComponentModel.DataAnnotations;

namespace RTUI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty; // Example: "In Progress", "Completed", "Pending"

        public DateTime CreatedDate { get; set; } = DateTime.Now; // Auto-assign current date


        [Required(ErrorMessage = "Please select a project type.")]
        public string ProjectType { get; set; } = string.Empty;
    } 
}

