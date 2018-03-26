using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreatCRM.Models
{
    public class Client
    {
        [Display(Name = "Client ID")]
        public int ClientId { get; set; }

        [Display(Name = "Client")]
        public string Name { get; set; }

        //Navigational Properties
        public int BrokerId { get; set; }
        public virtual Broker Broker { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }


    }
}