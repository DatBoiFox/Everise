using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EveriseAPI.Models
{
    public class Trs_PriceTypes
    {
        [Key]
        public int PriceTypeId { get; set; }
        public string Description { get; set; }
        public string TagName { get; set; }

    }
}
