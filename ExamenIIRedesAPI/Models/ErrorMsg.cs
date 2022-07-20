namespace ExamenIIRedesAPI.Models
{
    public class ErrorMsg
    {
        private string error;
        public ErrorMsg(string error)
        {
            this.Error = error;
        }

        public string Error { get => error; set => error = value; }
    }
}
