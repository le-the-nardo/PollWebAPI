using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollWebApi.Models.Responses
{
    public class PostPollResponse
    {
        public string Poll_description { get; set; }
        public List<PostOptionResponse> Options { get; set; }
    }

    public class PostOptionResponse
    {
        public string Option_description { get; set; }
    }
}