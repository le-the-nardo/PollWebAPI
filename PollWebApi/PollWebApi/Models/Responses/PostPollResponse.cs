using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PollWebApi.Models.Responses
{
    public class PostPollResponse
    {
        public string Poll_description { get; set; }
        public List<String> Options { get; set; }
    }

    public class PostOptionResponse
    {
        public int Option_id { get; set; }

        public string Description { get; set; }

        public int Poll_id { get; set; }
    }
}