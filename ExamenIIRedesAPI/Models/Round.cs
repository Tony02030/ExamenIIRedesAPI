namespace ExamenIIRedesAPI.Models
{
    public class Round
    {
        private int id;
        private List<Group> group;
        private string leader;
        private string winner;

        public Round(int id, List<Group> group, string leader, string winner)
        {
            this.id = id;
            this.group = group;
            this.leader = leader;
            this.winner = winner;
        }

        public int Id { get => id; set => id = value; }
        public List<Group> Group { get => group; set => group = value; }
        public string Leader { get => leader; set => leader = value; }
        public string Winner { get => winner; set => winner = value; }
    }
}