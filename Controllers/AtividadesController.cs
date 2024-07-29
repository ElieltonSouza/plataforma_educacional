using Microsoft.AspNetCore.Mvc;
using plataforma_educacional.Models;
using System.Collections.Generic;

namespace plataforma_educacional.Controllers
{
    public class AtividadesController : Controller
    {
        private readonly Repositorio<Atividade> _repositorio;

        public AtividadesController()
        {
            _repositorio = new Repositorio<Atividade>();
        }

        public IActionResult Index()
        {
            List<Atividade> atividades = _repositorio.Listar();
            return View(atividades);
        }

        public IActionResult Create()
        {
            return View("Form", new Atividade());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Atividade atividade)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Adicionar(atividade);
                return RedirectToAction(nameof(Index));
            }
            return View("Form", atividade);
        }

        public IActionResult Edit(int id)
        {
            var atividade = _repositorio.Buscar(id);
            if (atividade == null)
            {
                return NotFound();
            }
            return View("Form", atividade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Atividade atividade)
        {
            if (id != atividade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repositorio.Atualizar(atividade);
                return RedirectToAction(nameof(Index));
            }
            return View("Form", atividade);
        }

        public IActionResult Delete(int id)
        {
            var atividade = _repositorio.Buscar(id);
            if (atividade == null)
            {
                return NotFound();
            }
            return View(atividade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repositorio.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}