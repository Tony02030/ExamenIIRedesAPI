namespace ExamenIIRedesAPI.Models
{
    public class PsychoSelection
    {
        private bool psycho;

        public PsychoSelection(bool psycho)
        {
            this.Psycho = psycho;
        }

        public bool Psycho { get => psycho; set => psycho = value; }
    }
}
