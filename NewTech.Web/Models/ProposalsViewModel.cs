using Lifepoem.Foundation.Web.Helpers;
using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewTech.Web.Models
{
    public class ProposalsViewModel
    {
        public ProposalFilter Filter { get; set; }
        public IEnumerable<Proposal> Proposals { get; set; }
        public WebPagingOption PagingOption { get; set; }
    }
}