using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models
{
    public class GenericAPIResponse
    {
        public bool success { get; set; }
        public object result { get; set; }
        public string error { get; set; }
    }
}
