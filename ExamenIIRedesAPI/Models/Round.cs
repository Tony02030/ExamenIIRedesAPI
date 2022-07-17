namespace ExamenIIRedesAPI.Models
{
    public class Round
    {
        private int id;
        private List<Group> group;
        private string leader;
        private string winner;
        private static int count = 0;

        public Round(string leader)
        {
            this.id = count;
            count=count+1;
            group = new List<Group>();
            this.leader = leader;
        }

        public int Id { get => id; set => id = value; }
        public List<Group> Group { get => group; set => group = value; }
        public string Leader { get => leader; set => leader = value; }
        public string Winner { get => winner; set => winner = value; }
    }
}