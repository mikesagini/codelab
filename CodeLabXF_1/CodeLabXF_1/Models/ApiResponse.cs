using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLabXF_1.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }
}


// 200 - Success
// 201 - Successfully created
// 404 - Not Found
// 203, 403, 401 - Unauthorized