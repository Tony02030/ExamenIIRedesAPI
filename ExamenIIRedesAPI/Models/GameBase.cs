namespace ExamenIIRedesAPI.Models
{
    public class GameBase
    {

        private string name;//Generar Id aleatorio


        private string password;



        public GameBase(string password, string name)
        {
            this.password = password;
            this.name = name;
        }

        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
    }
}
