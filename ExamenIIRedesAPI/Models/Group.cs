namespace ExamenIIRedesAPI.Models
{
    public class Group
    {
        private string name;
        private bool? psycho;

        public Group(string name)
        {
            this.name = name;
            psycho = null;
        }

        public string Name { get => name; set => name = value; }
        public bool? Psycho { get => psycho; set => psycho = value; }
    }


}