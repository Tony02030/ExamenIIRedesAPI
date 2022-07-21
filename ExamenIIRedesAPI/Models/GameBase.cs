namespace ExamenIIRedesAPI.Models
{
    public class GameBase
    {

        private string name;//Generar Id aleatorio


        private string password;



        public GameBase(string name,string password)
        {
            this.name = name;
            this.password = password;
        }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        
    }
}
