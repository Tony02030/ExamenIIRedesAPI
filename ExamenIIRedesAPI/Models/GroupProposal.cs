using System.ComponentModel;

namespace ExamenIIRedesAPI.Models
{
    public class GroupProposal
    {
        private List<string> group;

        public GroupProposal(List<string> group)
        {
            this.group = group;
        }
        public List<string> Group { get => group; set => group = value; }
    }
}
