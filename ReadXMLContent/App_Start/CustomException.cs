// <copyright>
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Govardhan Pagidi</author>
// <date>08/22/2018 </date>
// <summary>Class representing a POC</summary>

using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ReadXMLContent.App_Start
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }
            //Log the exception message

            //We can log this exception message to the file or database.  
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by service."),
                ReasonPhrase = "Internal Server Error.Please check your input once again and try again."
            };
            actionExecutedContext.Response = response;
        }
    }
}