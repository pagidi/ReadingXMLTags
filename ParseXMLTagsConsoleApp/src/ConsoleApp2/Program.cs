using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp2
{
    public class Program
    {
        static void Main()
        {
            string xmlstring = @"Hi Yvaine,
                            Please create an expense claim for the below. Relevant details are marked up as requested…
                            <expense><cost_centre>DEV002</cost_centre> <total>890.55</total><payment_method>personal
                            card</payment_method>
                            </expense>
                            From: Ivan Castle
                            Sent: Friday, 16 February 2018 10:32 AM
                            To: Antoine Lloyd <Antoine.Lloyd@example.com>
                            Subject: test
                            Hi Antoine,
                            Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
                            team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
                            arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
                            Regards,
                            Ivan
                            Your";
            string validXML = GetMatch(xmlstring);

            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?><root>";
            xml += validXML;
            xml += "</root>";
            XDocument xmlRoot = null;
            ApiError error = new ApiError();
            if (!string.IsNullOrEmpty(xml))
            {
                if (IsValidRequest(xml, ref xmlRoot, ref error))
                {
                    var result = UpdateXMLwithDefalutValues(xmlRoot.Element("root"));
                    Console.WriteLine(result);
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    foreach (var item in result.Elements())
                    {
                        if (!values.ContainsKey(item.Name.LocalName))
                        {
                            values.Add(item.Name.LocalName, item.Value);
                            Console.WriteLine(item.Name.LocalName +":"+ item.Value);
                        }
                    }
                    Console.ReadKey();
                }
            }
            Console.ReadKey();

        }


        private static string GetMatch(string text)
        {
            Regex RegexTagsPattern = new Regex(@"<[^>]+>[^<]*</[^>]+>");
            StringBuilder output = new StringBuilder();

            bool matchFound = false;
            foreach (var item in RegexTagsPattern.Matches(text))
            {
                matchFound = true;
                output.Append(item);
            }

            if (matchFound)
                return output.ToString();
            else
                return string.Empty;
        }

        private static bool IsValidRequest(string xml, ref XDocument xmlRoot, ref ApiError error)
        {
            try
            {
                //Load the given string into xml document object
                xmlRoot = XDocument.Parse(xml);

                List<string> mandatoryElements = new List<string> { "total" };
                foreach (var elementName in mandatoryElements)
                {
                    // XML should have total element in it.
                    if (!xmlRoot.Descendants(elementName).Any())
                    {
                        error.ErrorCode = 1402;
                        error.ErrorMessage = $"{elementName} tag is missing in the request!";
                        Console.WriteLine(error.ErrorMessage);
                        return false;
                    }
                }

                //All validations passed
                return true;
            }
            catch (Exception ex)
            {
                error.ErrorCode = 1401;
                error.ErrorMessage = "Input XML is not formatted correctly, please check and try again !";
                Console.WriteLine("Input data is incorrect! Your Request is rejected!");
            }
            return true;
        }

        private static XElement UpdateXMLwithDefalutValues(XElement xmlRoot)
        {
            if (!xmlRoot.Elements("cost_centre").Any())
            {
                XElement csElement = new XElement("cost_centre", "UNKNOWN");
                xmlRoot.Add(csElement);
            }
            return xmlRoot;
        }

    }
    class ApiError
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

    }
}