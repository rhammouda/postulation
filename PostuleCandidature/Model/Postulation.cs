using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostuleCandidature.Model
{
    public class Postulation
    {
        public int Id { get; set; }
        public string nomSociete { get; set; }
        public string link { get; set; }
        public string poste { get; set; }
        public string reference { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
        public bool emailSent { get; set; }
             
    }
}
