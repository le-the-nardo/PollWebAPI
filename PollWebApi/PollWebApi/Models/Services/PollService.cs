using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PollWebApi.Entities;
using PollWebApi.Models.Responses;

/*using System.Text.Json;
using System.Text.Json.Serialization;*/

namespace PollWebApi.Models.Services
{
    public class PollService
    {
        private PollDataBaseEntities db;

        public PollService(PollDataBaseEntities db)
        {
            this.db = db;
        }

        /*public string returnJsonArray(p)
        {
           var obj = new
           {
               options = new object[] { foreach()new { option_id = "Huguinho", idade = 10 },
                                        new { option_id = "Zezinho", idade = 12 },
                                        new { option_id = "Luizinho", idade = 10 }}
           };

           JavaScriptSerializer js = new JavaScriptSerializer();
           string strJson = js.Serialize(obj);
           return strJson;
        }*/

        public List<GetPollResponse> GetPollByID(int id)
        {
            var poll = from p in db.Poll
                       where (p.Poll_Id == id)
                       select (
                       new GetPollResponse()
                       {
                           Poll_id = p.Poll_Id,
                           Poll_description = p.Description,
                           Options = db.Options.Where(o => o.Poll_Id == p.Poll_Id).Select(o => new GetOptionResponse
                           {
                               Option_id = o.Option_Id,
                               Option_description = o.Description
                           }).ToList()
                       });

            return poll.ToList();

        }

        public Poll AddPoll(PostPollResponse poll)
        {
            db.Configuration.ProxyCreationEnabled = false;
            using (var DBContext = db.Database.BeginTransaction())
            {
                var p = new Poll()
                {
                    Poll_Id = db.Poll.Count() + 1,
                    Description = poll.Poll_description
                };

                db.Poll.Add(p);

                List<Options> options = new List<Options>();

                foreach (var op in poll.Options)
                {
                    options.Add(new Options
                    {
                        Description = op,
                        Poll_Id = p.Poll_Id
                    });
                }

                db.Options.AddRange(options);
                db.SaveChanges();
                DBContext.Commit();

                return p;
            }
           
        }





    }
}
