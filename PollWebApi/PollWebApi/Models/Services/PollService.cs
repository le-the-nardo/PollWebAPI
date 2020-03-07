using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PollWebApi.Entities;
using PollWebApi.Models.DTO;

/*using System.Text.Json;
using System.Text.Json.Serialization;*/

namespace PollWebApi.Models.Services
{
    public class PollService
    {
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

        public IQueryable<PollDTO> GetPollByID(int id)
        {
            //using (var db = new PollDataBaseEntities())
            //{
            //    var poll = from p in db.Poll
            //            join o in db.Options on p.Poll_Id equals o.Poll_Id
            //            where (p.Poll_Id == id)
            //            select( new PollDTO()
            //            {
            //                poll_id = p.Poll_Id,
            //                Poll_Description = p.Description,
            //                Options = p.Options.Select(dt => new PollDetailDTO()
            //                {
            //                    option_id = o.Option_Id,
            //                    option_description = o.Description
            //                })
            //            });

            //    return poll;
            //}
            return null;
            
        }
            
    }
}