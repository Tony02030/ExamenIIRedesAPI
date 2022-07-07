namespace ExamenIIRedesAPI.Models
{
    public class Group
    {
        private string name;
        private bool psycho;

        public Group(string name, bool psycho)
        {
            this.name = name;
            this.psycho = psycho;
        }

        public string Name { get => name; set => name = value; }
        public bool Psycho { get => psycho; set => psycho = value; }
    }


}