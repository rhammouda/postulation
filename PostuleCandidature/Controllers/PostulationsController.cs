using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostuleCandidature.Model;

namespace PostuleCandidature.Controllers
{
    [Route("api/[controller]")]
    public class PostulationsController : Controller
    {
        private Model.PostuleContext _context;

        public PostulationsController(Model.PostuleContext context)
        {
            this._context = context;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Postulation> Get()
        {
            return _context.postulations.OrderByDescending(p=>p.date);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Postulation postulation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if(! force && _context.postulations.Any(p=>p.email == postulation.email))
            //{
            //    return StatusCode(206, "deja postulé au meme email");
            //}
            postulation.date = DateTime.Now;
            _context.postulations.Add(postulation);
            _context.SaveChanges();

            return Ok(postulation);
        }

        [Route("sendMail")]
        public void sendMail(int id)
        {
            var postulation = _context.postulations.Find(id);
            Service.EmailService.SendMail("Candidature " + postulation.poste + (!string.IsNullOrEmpty(postulation.reference) ? " (REF:" + postulation.reference + ")" : ""),
              "Bonjour,\n \n"
            + "Je suis intéressé par votre offre de mission " + postulation.poste + ".\n"
            + "Mes précédentes expériences dans le domaine du développement C# m'ont permis de maîtriser le langage C# et les différentes versions du framework .NET & .NET Core.\n"
            + "J'ai également beaucoup travaillé sur la partie front ce qui m'a permis d'avoir de solides compétences en développement ASP.NET MVC, webapi, Angular, JavaScript et CSS.\n \n"

            + "Je vous transmets mon curriculum vitae et je me tiens à votre disposition pour tout complément d'information.\n \n"

            + "Cordialement \n"
            + "Riadh HAMMOUDA \n"
            + "06 25 67 35 12", postulation.email.Split(';'));
            postulation.emailSent = true;
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var postule = _context.postulations.Find(id);
            _context.postulations.Remove(postule);
            _context.SaveChanges();
        }
    }
}
