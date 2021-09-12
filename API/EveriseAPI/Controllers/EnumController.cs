using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EveriseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EveriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {

        private readonly APIContext _context;

        public EnumController(APIContext context)
        {
            _context = context;

            if (_context.OrderStates.Count() <= 0)
            {

                for (int i = 0; i < 10; i++)
                {
                    _context.OrderStates.Add(
                        new Orders_States
                        {
                            Description = "Good",
                            EnumTag = "YES_" + i
                        });
                }
            }

            if (_context.TrsPriceTypes.Count() <= 0)
            {

                for (int i = 0; i < 10; i++)
                {
                    _context.TrsPriceTypes.Add(
                        new Trs_PriceTypes
                        {
                            Description = "Good",
                            TagName = "NO_" + i
                        });
                }
            }
            _context.SaveChanges();
        }


        [HttpGet(Name = "Get")]
        public string Get()
        {
            if (_context.data != null)
                return InsertEnums(_context.data);
            else
                return "No data";
        }

        // POST: api/Enum
        [HttpPost]
        public string Post([FromBody] Data value)
        {
            if(value != null)
            {
                _context.data = value;
            }

            if (_context.data != null)
                return InsertEnums(_context.data);
            else
                return "No data";

        }

        // PUT: api/Enum/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        private string GenerateEnums(Data data)
        {
            var tablesInfo =  GetTablesInfo(data);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var table in tablesInfo)
            {
                switch (table.TableName)
                {
                    case "Orders_States":
                        stringBuilder.AppendLine("\t");
                        stringBuilder.AppendLine("\tpublic enum OrderStates : int");
                        stringBuilder.AppendLine("\t{");
                        foreach (var temp in _context.OrderStates)
                        {
                            if (temp.EnumTag != null || temp.EnumTag != "")
                                stringBuilder.AppendLine("\t\t" + temp.EnumTag + " = " + temp.StateId + ",");
                        }
                        stringBuilder.AppendLine("\t};");
                        break;
                    case "Trs_PriceTypes":
                        stringBuilder.AppendLine("\t");
                        stringBuilder.AppendLine("\tpublic enum TrsPriceTypes : int");
                        stringBuilder.AppendLine("\t{");
                        foreach (var temp in _context.TrsPriceTypes)
                        {
                            if (temp.TagName != null || temp.TagName != "")
                                stringBuilder.AppendLine("\t\t" + temp.TagName + " = " + temp.PriceTypeId + ",");
                        }
                        stringBuilder.AppendLine("\t};");
                        break;
                }
            }

            return stringBuilder.ToString();
        }

        public string InsertEnums(Data data)
        {
            List<string> csFileContent = Regex.Split(data.csFileData, "\r\n|\r|\n").ToList();

            int from = 0, to = 0;

            for(int i = 0; i < csFileContent.Count; i++)
            {
                if (csFileContent[i].Contains("#region DB enumerations"))
                    from = i + 1;

                if (csFileContent[i].Contains("#endregion"))
                    to = i;
            }

            csFileContent.RemoveRange(from, to - from);
            csFileContent.Insert(from, GenerateEnums(data));

            return string.Join("\r\n", csFileContent);

        }
        

        private List<DBTableInfo> GetTablesInfo(Data data)
        {

            List<DBTableInfo> dbTablesInfo = new List<DBTableInfo>();
            var scvLines = Regex.Split(data.scvFileData, "\r\n|\r|\n");

            foreach(string line in scvLines)
            {
                var temp = line.Split(";");
                dbTablesInfo.Add(new DBTableInfo(temp[0], temp[1], temp[2]));
            }

            return dbTablesInfo;
        }

    }
}
