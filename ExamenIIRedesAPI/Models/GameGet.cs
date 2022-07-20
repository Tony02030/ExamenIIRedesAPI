using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerSchemaExample("WWMmTUllMLbJjKO2JlmdTonmJpYVUL9OMD83vue1")]
        public string GameId { get => gameId; set => gameId = value; }
        [SwaggerSchemaExample("GameTest")]
        public string Name { get => name; set => name = value; }
    }
}
