using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatCRM.Models
{
    public class Rider
    {
        public Rider()
        {
            this.Proposals = new HashSet<Proposal>();
        }
        public int RiderId { get; set; }
        public string RiderName { get; set; }

        //Navigational Propersties
        public virtual ICollection<Proposal> Proposals { get; set; } // many to many relationship with proposals
    }
}