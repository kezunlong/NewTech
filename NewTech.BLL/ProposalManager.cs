using Lifepoem.Foundation.Utilities.DBHelpers;
using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class ProposalManager : BaseManager
    {
        public ProposalManager(NewTechBll bll) : base(bll) { }

        public Proposal SelectProposal(int id)
        {
            return _dal.ProposalRepository.SelectProposal(id);
        }

        public List<Proposal> SelectProposals(ProposalFilter filter, PagingOption option)
        {
            return _dal.ProposalRepository.SelectProposals(filter, option);
        }

        public void InsertProposal(Proposal item)
        {
            _dal.ProposalRepository.InsertProposal(item);
        }

        public void UpdateProposal(Proposal item)
        {
            _dal.ProposalRepository.UpdateProposal(item);
        }

        public Proposal DeleteProposal(int id)
        {
            var item = SelectProposal(id);
            if (item != null)
            {
                _dal.ProposalRepository.DeleteProposal(id);
            }
            return item;
        }
    }
}
