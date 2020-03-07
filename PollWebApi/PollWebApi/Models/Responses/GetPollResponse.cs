using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollWebApi.Models.Responses
{
    public class GetPollResponse
    {
        public int poll_id { get; set; }
        public string poll_description { get; set; }
        public List<GetOptionResponse> options  { get; set; }
    }

    public class GetOptionResponse
    {
        public int option_id { get; set; }
        public string option_description { get; set; }
    }


}