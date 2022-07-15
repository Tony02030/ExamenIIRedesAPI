namespace ExamenIIRedesAPI.Models
{
    public class GameGet
    {
        private string gameId;
        private string name;

        public GameGet(string gameId, string name)
        {
            this.gameId = gameId;
            this.name = name;
        }

        public string GameId { get => gameId; set => gameId = value; }
        public string Name { get => name; set => name = value; }
    }
}
