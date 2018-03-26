using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreatCRM.Models
{
    public class ProposalRider
    {
        public int ProposalRiderId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProposalId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int RiderId { get; set; }
     

        [ForeignKey("ProposalId")]
        public Proposal Proposal { get; set; }

        [ForeignKey("RiderId")]
        public Rider Rider { get; set; }


    }
}