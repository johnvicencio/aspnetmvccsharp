using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatCRM.Models
{
    public class Proposal
    {
        public Proposal()
        {
            this.Riders = new HashSet<Rider>();
        }
        public int ProposalId { get; set; }
        public string Insurance { get; set; }

        //Navigational Properties
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }


        public virtual ICollection<Rider> Riders { get; set; }



    }
}