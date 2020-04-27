using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Model.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
