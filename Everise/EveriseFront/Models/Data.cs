using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveriseFront.Models
{
    public class Data
    {
        public string scvFileData { get; set; }
        public string csFileData { get; set; }

        public Data(string dbTableInfo, string csFile)
        {
            this.scvFileData = dbTableInfo;
            this.csFileData = csFile;
        }

    }
}
