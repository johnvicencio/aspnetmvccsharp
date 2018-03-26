using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreatCRM.Models.ViewModels
{
    public partial class RecordViewModels
    {
        public Broker Broker { get; set; }
        public ICollection<Broker> Brokers { get; set; }

    }
}