using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EveriseAPI.Models
{
    public class Orders_States
    {
        [Key]
        public int StateId { get; set; }
        public string Description { get; set; }
        public string EnumTag { get; set; }

    }
}
