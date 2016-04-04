using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;


/*
 * Good for logic testing
 * For UI based project, it is not veru appropriate.
 * 
 * */
namespace UnitTestProject1
{
    public class CsvParser
    {
        /// <summary>
        /// Parsing rule: 
        ///     1. A cell may or may not start with a single quote
        ///     2. If a cell starts with a quote, it must end with a quote
        ///     3. If a quote exists as a cell component , the cell must start with quote
        ///     4. quotes within a cell are double escaped  r.g ""
        ///     5. if a commma exists as a cell component, the cell must start and end with quotes
        ///     
        ///     
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<String> ParseRow(string row)
        {
            // simple row parsing

            //string[] tmp;
            //tmp = row.Split(',');
            //return tmp.ToList();


            List<string> retList = new List<string>();
            string at = "";
            for (int iter = 0; iter < row.Length; iter++)
            {
                if (row[iter] == '"')
                {
                    iter++;
                    if (row[iter] == '"')
                    {
                        at += row[iter];
                        iter++;
                    }
                    while (iter < row.Length && row[iter] != '"')
                    {
                        at += row[iter];
                        iter++;
                        if ((iter < row.Length - 1) 
                            && (row[iter] == '"') 
                            && (row[iter + 1] == '"'))
                        {
                            at += row[iter];
                            iter += 2;
                        }
                    }
                }
                else if (row[iter] == ',')
                {
                    retList.Add(at);
                    at = "";
                }
                else
                {
                    at += row[iter];
                }
            }
            retList.Add(at);
            return retList;
        }



        public List<String> ParseRowReg(string row)
        {
            // Decent method using regular expression
            // source: http://stackoverflow.com/questions/18757097/writing-data-into-csv-file
            Regex regex = new Regex(@"^(?:""(?<item>[^""]*)""|(?<item>[^,]*))(?:,(?:""(?<item>[^""]*)""|(?<item>[^,]*)))*$");
            List<String> list = regex
              .Match(row)
              .Groups["item"]
              .Captures
              .Cast<Capture>()
              .Select(c => c.Value)
              .ToList();

            return list;
        }



    }
}
