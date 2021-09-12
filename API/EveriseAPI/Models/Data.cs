using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EveriseAPI.Models
{
    public class Data
    {
        public string scvFileData { get; set; }
        public string csFileData { get; set; }


        public override string ToString()
        {
            return base.ToString() + $"scvFileData: {scvFileData}, csFileData: {csFileData}";
        }

    }
}
