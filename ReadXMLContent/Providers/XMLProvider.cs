// <copyright>
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Govardhan Pagidi</author>
// <date>08/22/2018 </date>
// <summary>Class representing a POC</summary>

using ReadXMLContent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace ReadXMLContent.Providers
{
    public class XMLProvider
    {
        protected internal static string GetMatch(string text)
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
                return $"<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?><root>{output.ToString()}</root>";
            else
                return string.Empty;
        }

        /// <summary>
        /// Read the xml and append default values if element is missed
        /// </summary>
        /// <param name="xmlRoot"></param>
        /// <returns></returns>
        protected internal static XElement UpdateXMLwithDefalutValues(XElement xmlRoot)
        {
            if (!xmlRoot.Elements("cost_centre").Any())
            {
                XElement csElement = new XElement("cost_centre", "UNKNOWN");
                xmlRoot.Add(csElement);
            }
            return xmlRoot;
        }

        /// <summary>
        /// Find the mandatory fields and throw if missed
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xmlRoot"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        protected internal static bool IsValidRequest(string xml, ref XDocument xmlRoot, ref ApiError error)
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
    }
}