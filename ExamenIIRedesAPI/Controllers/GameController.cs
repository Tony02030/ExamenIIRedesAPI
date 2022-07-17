using Microsoft.AspNetCore.Mvc;
using ExamenIIRedesAPI.Models;
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenIIRedesAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/game/")]

    public class GameController : ControllerBase

    {

        // GET: api/<GameController>
        [HttpGet]
        public List<GameGet> Get()
        {
            List<GameGet> games = new List<GameGet>();

            for (int i = 0; i < Util.Utility.gameList.Count(); i++)
            {

                GameGet game = new GameGet(Util.Utility.gameList[i].GameId, Util.Utility.gameList[i].Name);
                games.Add(game);

            }
            return games;
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public Game Get(string gameId, [FromHeader] string name, [FromHeader] string password)
        {
            Game game = new Game();

            for (int i = 0; i < Util.Utility.gameList.Count(); i++)
            {
                game = Util.Utility.gameList[i];
                if (game.GameId.Equals(gameId) && game.Name.Equals(name) && game.Password.Equals(password))
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
        public IActionResult Create(string? owner, [FromBody] GameBase game)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, "missing game name or password parameters");

            }
                
            if (owner == null || owner == "" || game.Name == null || game.Password == null || game.Name == "" || game.Password == "")
                
            {
                    
                return StatusCode(406, "missing name header or game name parameters");
                
            }
                
            else
                
            {
                    
                Game game1 = new Game(owner, game.Name, game.Password);
                    
                Util.Utility.gameList.Add(game1);
                    
                return Ok();
                
            }
                

            
            
           
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
    }
}
