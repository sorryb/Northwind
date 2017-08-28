using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Web;  
using System.Web.Mvc;  
   
namespace NorthwindWeb.ViewModels
{
    public class CustomizeViewEngine : RazorViewEngine
    {
        public CustomizeViewEngine()
        {
            //here we are resetting the ViewLocationFormats  
            ViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml"};
        }
    }
}