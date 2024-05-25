﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailStream_Server.Models.Other
{
    internal class ServerResponse
    {
        public bool Status;

        public string? Content;

        public ServerResponse(bool status, string content)
        {
            Status = status;
            Content = content;
        }


    }
}
