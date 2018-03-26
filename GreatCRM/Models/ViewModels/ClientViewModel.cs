using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatCRM.Models.ViewModels
{
    public partial class RecordViewModels
    {
        public Client Client { get; set; }
        public ICollection<Client> Clients { get; set; }


    }
}