using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveriseAPI.Models
{
    public class DBTableInfo
    {
        public string TableName { get; set; }
        public string IDColl { get; set; }
        public string EnumTag { get; set; }


        public DBTableInfo(string tableName, string idColl, string enumTag)
        {
            TableName = tableName;
            IDColl = idColl;
            EnumTag = enumTag;
        }
    }
}
