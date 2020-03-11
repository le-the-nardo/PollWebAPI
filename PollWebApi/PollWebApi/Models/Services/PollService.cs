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
        
        public GetPollResponse GetPollByID(int id)
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

            return poll.FirstOrDefault();
        }

        public Poll AddPoll(PostPollResponse poll)
        {
            db.Configuration.ProxyCreationEnabled = false;
            using (var DBContext = db.Database.BeginTransaction())
            {
                var p = new Poll()
                {
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

        public bool AddVote(int op)
        {
            db.Configuration.ProxyCreationEnabled = false;
            using (var DBContext = db.Database.BeginTransaction())
            {
                var o = db.Options.Find(op);

                if (o == null)
                    return false;

                var v = new Votes()
                {
                    Option_Id = op,
                    Date = DateTime.Now
                };

                db.Votes.Add(v);
                db.SaveChanges();
                DBContext.Commit();

                return true;
            }
        }

        public void AddView(int poll_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            using (var DBContext = db.Database.BeginTransaction())
            {
                var v = new Views()
                {
                    View_Id = (from views in db.Views
                               select views).Count() + 1,
                    Poll_Id = poll_id
                };

                db.Views.Add(v);
                db.SaveChanges();
                DBContext.Commit();
            }
        }

        public GetStatsResponse GetStatsById(int poll_id)
        {
            var pollViews = (from views in db.Views
                             where (views.Poll_Id == poll_id)
                             select views).Count();

            List<GetVotesStatsResponse> votes = new List<GetVotesStatsResponse>();
            
            votes = (from o in db.Options
                           where (o.Poll_Id == poll_id)
                           select (new GetVotesStatsResponse
                           {
                               Option_Id = o.Option_Id,
                               Qty = db.Votes.Where(q => q.Option_Id == o.Option_Id).Count()
                           })).ToList();

            
            return new GetStatsResponse
            {
                Views = pollViews,
                Votes = votes
            };
        }
    }
}
