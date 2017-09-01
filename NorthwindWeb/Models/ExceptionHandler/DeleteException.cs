using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.Models.ExceptionHandler
{
    public class DeleteException : Exception
    {
        public DeleteException(string message) : base(message)
        {

        }
    }
}