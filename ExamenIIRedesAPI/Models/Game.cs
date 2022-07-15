

namespace ExamenIIRedesAPI.Models
{
    public class Game
    {
        private string gameId;//Generar Id aleatorio

        
        private string name;

        
        private string owner;

       
        private string password;

        private List<string> players;
        private List<string> psychos;
        private bool psychoWin;
        private string status;
        private List<Round> rounds;
        public Game()
        {
            this.gameId = "";//Generarlo aleatorio
            this.name = "";
            this.owner = "";
            this.password = "";
            this.players = new List<string>();
            this.psychos = new List<string>();
            this.psychoWin = false;
            this.rounds = new List<Round>();
        }

        public Game(string name, string owner, string password)
        {
            this.gameId = "Prueba";//Generarlo aleatorio
            this.name = name;
            this.owner = owner;
            this.password = password;
            this.players = new List<string>();
            this.psychos = new List<string>();
            this.psychoWin = false;
            this.rounds = new List<Round>();
        }

        public string GameId { get => gameId; set => gameId = value; }
        public string Name { get => name; set => name = value; }
        public string Owner { get => owner; set => owner = value; }
        public string Password { get => password; set => password = value; }
        public List<string> Players { get => players; set => players = value; }
        public List<string> Psychos { get => psychos; set => psychos = value; }
        public bool PsychoWin { get => psychoWin; set => psychoWin = value; }
        public string Status { get => status; set => status = value; }
        public List<Round> Rounds { get => rounds; set => rounds = value; }
    }
}
