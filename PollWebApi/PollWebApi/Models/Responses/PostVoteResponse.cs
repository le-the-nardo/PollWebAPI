using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollWebApi.Models.Responses
{
    public class PostVoteResponse
    {
        public int Option_id { get; set; }
        public DateTime Date { get; set; }
    }
}