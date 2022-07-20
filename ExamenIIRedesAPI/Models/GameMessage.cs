using System.ComponentModel;

namespace ExamenIIRedesAPI.Models
{
    public class GameMessage
    {
        
        private string message;

        public GameMessage(string message)
        {
            this.Message = message;
        }
        [DefaultValue("Operation successful")]
        public string Message { get => message; set => message = value; }
    }
}
