using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollWebApi.Models.DTO
{
    public class PollDTO
    {
        public int poll_id { get; set; }
        public string Poll_Description { get; set; }
        public IEnumerable<PollDetailDTO> Options  { get; set; }
    }
}