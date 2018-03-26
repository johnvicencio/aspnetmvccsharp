using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreatCRM.Models
{
    public class Broker
    {
        [Display(Name = "Broker ID")]
        public int BrokerId { get; set; }

        [Display(Name = "Broker")]
        public string Name { get; set; }
        public int Age { get; set; }

        //Navigational Properties
        public virtual ICollection<Client> Clients { get; set; }

    }
}