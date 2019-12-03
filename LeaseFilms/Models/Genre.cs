using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaseFilms.Models
{
    public class Genre
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(127)]
        public string Name { get; set; }
    }
}