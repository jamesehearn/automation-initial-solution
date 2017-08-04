using System.Linq;
using System.Xml.Linq;

namespace Framework
{
    public class SecurityPaper
    {
        //private static string Environment = Tests.Properties.Settings.Default.Environment;

        public string PaperNumber(string state, string type)
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            string theNumber;

            XElement securityPaper = XElement.Load("../../../SecurityPaper" + UI_Tests.BaseTest.Environment + ".xml");

            var paperNumber = (from number in securityPaper.Elements("PaperNumber")
                               where (string)number.Element("State") == state &&
                                     (string)number.Element("Type") == type
                               select number).FirstOrDefault();

            theNumber = paperNumber?.Element("Number")?.Value;

            paperNumber?.Element("Number")?.SetValue((int)paperNumber.Element("Number") + 1);

            securityPaper.Save("../../../SecurityPaper" + UI_Tests.BaseTest.Environment + ".xml");

            return theNumber;
        }
    }
}