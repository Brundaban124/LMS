using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Model.Entities
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
