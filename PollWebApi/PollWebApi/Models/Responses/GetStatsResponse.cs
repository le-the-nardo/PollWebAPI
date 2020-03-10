using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollWebApi.Models.Responses
{
    public class GetStatsResponse
    {
        public int Views { get; set; }

        public List<GetVotesStatsResponse> Votes { get; set; }
    }

    public class GetVotesStatsResponse
    {
        public int Option_Id { get; set; }

        public int Qty { get; set; }
    }
}