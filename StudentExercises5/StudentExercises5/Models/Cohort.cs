using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentExercises5.Models
{
    public class Cohort
    {
        public int Id { get; set; }

        [Required]
        [StringLength( 11, MinimumLength = 3 )]
        public string Name { get; set; }
    
        public List<Student> Students {get; set;}

        public List<Instructor> Instructors { get; set; }
    }
}
