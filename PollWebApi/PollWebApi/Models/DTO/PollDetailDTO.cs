using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollWebApi.Models.DTO
{
    public class PollDetailDTO
    {
        public int option_id { get; set; }
        public string option_description { get; set; }
    }
}