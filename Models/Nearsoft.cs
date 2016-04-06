using System.Collections.Generic;

namespace SampleWebApp.Models
{
    public class Nearsoft
    {
        public List<Nearsoftian> Nearsoftians { get; set; }

        public Nearsoft()
        {
            Nearsoftians = new List<Nearsoftian>()
            {
                new Nearsoftian { Name = "Gus", Phone = "1234444" }
            };
        }
    }
}