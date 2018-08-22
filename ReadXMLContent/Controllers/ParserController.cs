// <copyright>
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Govardhan Pagidi</author>
// <date>08/22/2018 </date>
// <summary>Class representing a POC</summary>

using ReadXMLContent.App_Start;
using ReadXMLContent.Models;
using ReadXMLContent.Providers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;


namespace ReadXMLContent.Controllers
{
    public class ParserController : ApiController
    {
        [CustomExceptionFilter]
        public HttpResponseMessage Get(string text)
        {
            string validXML = XMLProvider.GetMatch(text);

            XDocument xmlRoot = null;
            ApiError error = new ApiError();

            if (!string.IsNullOrEmpty(validXML))
            {
                if (XMLProvider.IsValidRequest(validXML, ref xmlRoot, ref error))
                {
                    var result = XMLProvider.UpdateXMLwithDefalutValues(xmlRoot.Element("root"));
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    foreach (var item in result.Elements())
                    {
                        if (!values.ContainsKey(item.Name.LocalName))
                            values.Add(item.Name.LocalName, item.Value);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, values);
                }
            }
            else
            {
                error.ErrorCode = 1403;
                error.ErrorMessage = "No valid xml found in the given content";
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, error);
        }
    }
}
