﻿namespace MyRE.Core.Models.Web
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public ErrorResponse(string message)
        {
            this.Message = message;
        }
    }
}
