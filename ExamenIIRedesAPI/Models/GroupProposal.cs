namespace ExamenIIRedesAPI.Models
{
    public class GroupProposal
    {
        private List<string> players;

        public GroupProposal(List<string> players)
        {
            this.Players = players;
        }

        public List<string> Players { get => players; set => players = value; }
    }
}
