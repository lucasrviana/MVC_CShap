using DevOI.UI.WebModelo.Data;
using DevOI.UI.WebModelo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DevOI.UI.WebModelo.Modulos.Area.Controllers
{
    [Area("Area")]
    [Route("testeCrud")]
    public class TesteCrudContoller : Controller
    {
        private readonly Contexto _contexto;

        public TesteCrudContoller(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var aluno = new Aluno
            {
                Nome = "Eduardo",
                DataNascimento = DateTime.Now,
                Email = "email@email.com"
            };

            _contexto.Alunos.Add(aluno);
            _contexto.SaveChanges();

            var alundo2 = _contexto.Alunos.Find(aluno.Id);
            var alundo3 = _contexto.Alunos.FirstOrDefault(a => a.Email == "email@email.com");
            var alundo4 = _contexto.Alunos.Where(a => a.Nome == "Eduardo");

            aluno.Nome = "joão";

            _contexto.Alunos.Update(aluno);
            _contexto.SaveChanges();

            _contexto.Alunos.Remove(aluno);
            _contexto.SaveChanges();


            return View("_Layout");
        }
    }
}
