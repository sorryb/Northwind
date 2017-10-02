using System;

namespace NorthwindWeb.Models.ExceptionHandler
{
    /// <summary>
    /// The keeps all the exceptions in case that we can't delete
    /// </summary>
    public class DeleteException : Exception
    {
        /// <summary>
        /// The message of Delete exception
        /// </summary>
        /// <param name="message"></param>
        public DeleteException(string message) : base(message)
        {

        }
    }
}