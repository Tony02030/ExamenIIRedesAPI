﻿

using System.Security.Cryptography;

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
        private List<bool> psychoWin;
        private string status;
        private List<Round> rounds;
        private static Random random = new Random();
        public Game()
        {
            this.gameId = "";//Generarlo aleatorio
            this.name = "";
            this.owner = "";
            this.password = "";
            this.players = new List<string>();
            this.psychos = new List<string>();
            this.rounds = new List<Round>();
        }

        public Game(string name, string owner, string password)
        {
            this.gameId = idGames();//Generarlo aleatorio
            this.name = name;
            this.owner = owner;
            this.password = password;
            this.players = new List<string>();
            this.players.Add(owner);
            this.psychos = new List<string>();
            this.psychoWin = new List<bool>();
            this.rounds = new List<Round>();
            this.status = "lobby";
        }
        [SwaggerSchemaExample("WWMmTUllMLbJjKO2JlmdTonmJpYVUL9OMD83vue1")]
        public string GameId { get => gameId; set => gameId = value; }
        [SwaggerSchemaExample("GameTest")]
        public string Name { get => name; set => name = value; }
        [SwaggerSchemaExample("Ivan")]
        public string Owner { get => owner; set => owner = value; }
        public string Password { get => password; set => password = value; }
        public List<string> Players { get => players; set => players = value; }
        public List<string> Psychos { get => psychos; set => psychos = value; }
        public List<bool> PsychoWin { get => psychoWin; set => psychoWin = value; }
        public string Status { get => status; set => status = value; }
        public List<Round> Rounds { get => rounds; set => rounds = value; }

        public string idGames()
        { 
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(characters, 40)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }


    
}
