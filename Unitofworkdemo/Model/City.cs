using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unitofworkdemo.Model
{
    public class City
    {
        [Key]
        public Int64 id { get; set; }
        public string cityname { get; set; }
    }
}
