using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NonManagedDomains
{
    class Program
    {
        /// <summary>
        /// Entry point: Compare values in two (2) CSV files and output to a new file the unmatched items
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        
       {
            //Open files containing data for comparison
            var salesforceReader = new StreamReader(File.OpenRead(@"C:\salesforce.csv"));
            var domainReader = new StreamReader(File.OpenRead(@"C:\domains.csv"));

            //Create lists to populate the data from the CSV files
            List<string> salesforceList = new List<string>();
            List<string> domainList = new List<string>();

            //Read each line in Salesforce CSV, format it for comparison and add to
            //salesforceList
            while (!salesforceReader.EndOfStream)
            {
                string line = salesforceReader.ReadLine();
                string value = line.Substring(line.LastIndexOf(",") + 1).ToLower();
                value = value.Replace("http://www.","").Replace("www.","").Replace("http://","").Trim('/');
                salesforceList.Add(value);
            }

            //Read each line in Domains CSV, format it for comparison and add to
            //domainList
            while (!domainReader.EndOfStream)
            {
                string line = domainReader.ReadLine();
                domainList.Add(line.ToLower().Trim(','));
            }

            //Compare both lists and add to RESULTS.TXT the unmatched domains.
            List<string> results = salesforceList.Where(a => !domainList.Contains(a) && a.Contains('.'))
                                                 .Distinct()
                                                 .ToList();
            File.WriteAllLines(@"C:\results.txt", results);
        }
    }
}
