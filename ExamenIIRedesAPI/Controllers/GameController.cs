using Microsoft.AspNetCore.Mvc;
using ExamenIIRedesAPI.Models;
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenIIRedesAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class GameController : ControllerBase
    {
        public List<Game> gamesList;

        public GameController()
        {
            this.gamesList = new List<Game>();
        }

        // GET: api/<GameController>
        [HttpGet]
        public IEnumerable<Game> Get()
        {

            return this.gamesList;
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public Models.Game Get(string id)
        {
            Models.Game game = new Models.Game();

            for (int i = 0; i < gamesList.Count(); i++)
            {
                game = gamesList[i];
                if (game.GameId.Equals(id))
                {
                    return game;
                }
                else
                {
                    return game;
                }
            }


            return game;

        }

        // POST api/game/create
        [HttpPost]
        public void Create(string owner, [FromBody] GameBase game)
        {
            //gamesList.Add(new Models.Game("Juego", "Jenny", "123"));
            Game game1 = new Game(owner, game.Name, game.Password);
            game1.GameId = idGames();
            this.gamesList.Add(game1);
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        public string idGames()
        {
            var bytesarray = new byte[0];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                var bits = (40 * 6);
                var byte_size = ((bits + 7) / 8);
                bytesarray = new byte[byte_size];
                crypto.GetBytes(bytesarray);
            }

            return Convert.ToBase64String(bytesarray);
        }
    }
}
