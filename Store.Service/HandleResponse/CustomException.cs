﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandleResponse
{
    public class CustomException : Response
    {
      

        public CustomException(int statusCode, string? message=null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
        string? Details { get; set; }

    }
}
