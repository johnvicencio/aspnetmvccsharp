﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatCRM.Models.ViewModels
{
    public partial class RecordViewModels
    {
        public Proposal Proposal { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
    }
}