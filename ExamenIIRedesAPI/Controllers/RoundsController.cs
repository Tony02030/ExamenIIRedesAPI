using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ExamenIIRedesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenIIRedesAPI.Controllers
{
    [Produces("application/json")]
    [Route("/game/")]
    /// <summary>
    /// Game Actions
    /// </summary>
    public class RoundsController : ControllerBase
    {
        ///<summary>
        ///Extract an arbitrary game
        ///</summary>
        ///<remarks>Extract information for an arbitrary game. Please notice this is the same as using gameId filters on /</remarks>
        ///<param name="gameId" >Game Identificator</param>
        ///<param name="name">Player's name</param>
        ///<param name="password">Game's password</param>
        ///<response code="200">returns game object</response>
        ///<response code="403">You are not part of the players list</response>
        ///<response code="404">Invalid Game's id</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Game))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorMsg))]
        [HttpGet("{gameId}")]
        public IActionResult Get(string gameId, [FromHeader][Required] string name, [FromHeader] string? password)
        {
            Game game;

            if (Util.Utility.existGameId(gameId))
            {
                game = Util.Utility.getGame(gameId);
                if (Util.Utility.existPlayer(game, name))
                {
                    if (game.Password.Equals(password) || game.Password=="")
                    {
                        return StatusCode(200, game);

                    }
                    else
                    {
                        return StatusCode(401, new ErrorMsg("Unathorized"));
                    }
                }
                else
                {
                    return StatusCode(403, new ErrorMsg("You are not part of the players list"));

                }
            }
            else
            {
                return StatusCode(404, new ErrorMsg("Invalid Game's id"));

            }




        }

        ///<summary>
        ///Add player to game
        ///</summary>
        ///<remarks>Add player to an arbitrary game</remarks>
        ///<param name="gameId" >Game Identificator</param>
        ///<param name="name">Player's name</param>
        ///<param name="password">Game's password</param>
        ///<response code="200">returns game object</response>
        ///<response code="403">You are not part of the players list</response>
        ///<response code="404">Invalid Game's id</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GameMessage))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ErrorMsg))]
        [HttpPut]
        [Route("{gameId}/[action]")]
        public ActionResult join(string gameId, [FromHeader] [Required] string name, [FromHeader] string? password)
        {
            Game game;

            if (!ModelState.IsValid)
            {
                return StatusCode(406, "Data provided is invalid");
            }
            else
            {
                if (Util.Utility.existGameId(gameId))
                {
                    game = Util.Utility.getGame(gameId);
                    if (game.Password.Equals(password) || game.Password == "")
                    {
                        if (!game.Players.Contains(name) )
                        {
                            if (game.Status.Equals("started") || game.Players.Count() >= 10)
                            {
                                return StatusCode(406, new ErrorMsg("Game has already started or is full"));
                            }
                            else
                            {
                                game.Players.Add(name);
                                return StatusCode(200, new GameMessage("Player was added to the ongoing game"));
                            }
                        }
                        else
                        {
                            return StatusCode(409, new ErrorMsg("You are already part of this game"));
                        }
                    }
                    else
                    {
                        return StatusCode(401, new ErrorMsg("Unathorized"));
                    }

                }
                else
                {
                    return StatusCode(404, new ErrorMsg("Invalid Game's id"));
                }
            }


        }

        ///<summary>
        ///Starts the game
        ///</summary>
        ///<remarks>If you are the game owners, it will start the game</remarks>
        ///<param name="gameId" >Game Identificator</param>
        ///<param name="name">Player's name</param>
        ///<param name="password">Game's password</param>
        ///<response code="200">returns game object</response>
        ///<response code="401">You are not the game's owner or Unathorized</response>
        ///<response code="403">You are not part of the players list</response>
        ///<response code="404">Invalid Game's id</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GameMessage))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorMsg))]

        [HttpHead]
        [Route("{gameId}/[action]")]
        public ActionResult start(String gameId, [FromHeader] [Required] String name, [FromHeader] String? password)
        {
            Game game;
            if (Util.Utility.existGameId(gameId))
            {
                game = Util.Utility.getGame(gameId);
                if (game.Owner.Equals(name))
                {
                    if (game.Password.Equals(password) || game.Password == "")
                    {

                        if (Util.Utility.verifyPlayersCount(game))
                        {
                            if (game.Status == "lobby")
                            {
                                game.Status = "leader";
                                int countPsychos = Util.Utility.getPsychosCount(game);
                                int count = 0;
                                while (count < countPsychos)
                                {
                                    string psycho = Util.Utility.getRandomLeader(game);
                                    if (!game.Psychos.Contains(psycho))
                                    {
                                        game.Psychos.Add(psycho);
                                        count++;
                                    }
                                }

                                Round round = new Round(Util.Utility.getRandomLeader(game));
                                game.Rounds.Add(round);
                                return Ok("Operation successful");
                            }
                            else
                            {
                                return StatusCode(401, new ErrorMsg("Unathorized"));
                            }
                        }
                        else
                        {
                            return StatusCode(406, new ErrorMsg("Not Acceptable"));
                        }
                    }
                    else
                    {
                        return StatusCode(401, new ErrorMsg("Unathorized"));
                    }

                }
                else
                {
                    return StatusCode(401, new ErrorMsg("You are not the game's owner"));


                }
            }
            else
            {
                return StatusCode(404, new ErrorMsg("Invalid Game's id"));
            }


        }

        ///<summary>
        ///Group proposal
        ///</summary>
        ///<remarks>The round leader can propose a group for the current round</remarks>
        ///<param name="gameId" >Game Identificator</param>
        ///<param name="name">Player's name</param>
        ///<param name="password">Game's password</param>
        ///<param name="playersGroup">Player's name's array</param>
        ///<response code="200">Group was added to the ongoing round</response>
        ///<response code="401">Unathorized</response>
        ///<response code="403">You are not part of the players list</response>
        ///<response code="404">Invalid Game's id</response>
        ///<response code="406">Game is not in the groups stage or provided group has invalid parameters (size/players)</response>
        ///<response code="409">There is already a group added for this round</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GameMessage))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ErrorMsg))]

        [HttpPost]
        [Route("{gameId}/[action]")]
        public ActionResult group(string gameId, [FromHeader] [Required] string name, [FromHeader] string? password, [FromBody][Required] GroupProposal playersGroup)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, new ErrorMsg("Provided group has invalid parameters"));
            }
            else
            {
                Game game;
                if (Util.Utility.existGameId(gameId))
                {
                    game = Util.Utility.getGame(gameId);
                    if (Util.Utility.verifyPlayersCount(game, playersGroup.Group.Count()) && Util.Utility.verifyPlayersExist(game, playersGroup))
                    {
                        if (game.Players.Contains(name))
                        {
                            if (game.Password.Equals(password) || game.Password == "")
                            {
                                if (game.Status == "leader")
                                {
                                    game.Status = "rounds";

                                    for (int i = 0; i < playersGroup.Group.Count(); i++)
                                    {
                                        Group group = new Group(playersGroup.Group[i]);
                                        game.Rounds[Util.Utility.getRounds(game)].Group.Add(group);


                                    }

                                    return Ok("Operation successful");
                                }
                                else
                                {
                                    return StatusCode(406, new ErrorMsg("Game is not in the groups stage"));
                                }

                            }
                            else
                            {
                                return StatusCode(401, new ErrorMsg("Unathorized"));
                            }

                        }
                        else
                        {
                            return StatusCode(403, new ErrorMsg("You are not part of the players list"));


                        }
                    }
                    else
                    {
                        return StatusCode(406, new ErrorMsg("Provided group has invalid parameters (size/players)"));
                    }
                }

                else
                {
                    return StatusCode(404, new ErrorMsg("Invalid Game's id"));
                }

            }
        }


        ///<summary>
        ///Go into round
        ///</summary>
        ///<param name="gameId" >Game Identificator</param>
        ///<param name="name">Player's name</param>
        ///<param name="password">Game's password</param>
        ///<param name="psycho">Activate psycho mode</param>
        ///<response code="200">Group was added to the ongoing round</response>
        ///<response code="401">You are not part of the round group list</response>
        ///<response code="403">You are not part of the players list</response>
        ///<response code="404">Invalid Game's id</response>
        ///<response code="406">Data provided is invalid</response>
        ///<response code="409">There is already an entry for you this round</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GameMessage))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, Type = typeof(ErrorMsg))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ErrorMsg))]

        [HttpPost]
        [Route("{gameId}/[action]")]
        public ActionResult go(string gameId, [FromHeader] [Required] string name, [FromHeader] string? password, [FromBody][Required] PsychoSelection psycho)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, "Data provided is invalid");
            }
            else
            {
                Game game;
                if (Util.Utility.existGameId(gameId))
                {
                    game = Util.Utility.getGame(gameId);
                    if (Util.Utility.verifyPlayerSelection(game, name))
                    {
                        if (game.Players.Contains(name))
                        {
                            if (Util.Utility.verifyGroupList(game, name))
                            {


                                if (game.Password.Equals(password) || game.Password == "")
                                {
                                    if (game.Status == "rounds")
                                    {
                                        Util.Utility.GetGroup(game, name).Psycho = psycho.Psycho;

                                        if (Util.Utility.verifyAllGroupSelection(game))
                                        {
                                            if (Util.Utility.verifyPsychoWin(game))
                                            {

                                                game.PsychoWin.Add(true);
                                                if (Util.Utility.verifyGameWinner(game))
                                                {
                                                    game.Status = "ended";
                                                }
                                                else
                                                {
                                                    game.Rounds[Util.Utility.getRounds(game)].Winner = "psychos";
                                                    game.Status = "leader";
                                                    Round round = new Round(Util.Utility.getRandomLeader(game));
                                                    game.Rounds.Add(round);
                                                }


                                            }
                                            else
                                            {

                                                game.PsychoWin.Add(false);
                                                if (Util.Utility.verifyGameWinner(game))
                                                {
                                                    game.Status = "ended";
                                                }
                                                else
                                                {
                                                    game.Rounds[Util.Utility.getRounds(game)].Winner = "players";
                                                    game.Status = "leader";
                                                    Round round = new Round(Util.Utility.getRandomLeader(game));
                                                    game.Rounds.Add(round);
                                                }

                                            }


                                        }

                                        return Ok("Operation successful");
                                    }
                                    else
                                    {
                                        return StatusCode(406, new ErrorMsg("Game is not in the rounds stage"));
                                    }
                                }
                                else
                                {
                                    return StatusCode(401, new ErrorMsg("Unathorized"));
                                }
                            }
                            else
                            {
                                return StatusCode(401, new ErrorMsg("You are not part of the round list"));
                            }


                        }
                        else
                        {
                            return StatusCode(403, new ErrorMsg("You are not part of the players list"));


                        }
                    }

                    else
                    {
                        return StatusCode(409, new ErrorMsg("There is already an entry for you this round"));
                    }
                }
                else
                {
                    return StatusCode(404, new ErrorMsg("Invalid Game's id"));
                }

            }

        }
    }
}
