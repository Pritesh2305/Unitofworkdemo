using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unitofworkdemo.Dtos
{
    public class CityDto
    {
        public Int64 id { get; set; }

        [Required (ErrorMessage ="Name is Required.")]
        public string cityname { get; set; }
        public string remark { get; set; }
        [Required]
        public string country { get; set; }
    }
}
