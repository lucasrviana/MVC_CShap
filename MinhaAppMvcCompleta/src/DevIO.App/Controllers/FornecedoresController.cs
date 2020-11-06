using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;

namespace DevIO.App.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedor _Fornecedorcontext;
        private readonly IFornecedorService _FornecedorService;

        public FornecedoresController(IFornecedor context, IFornecedorService fornecedorService, IMapper mapper): base(mapper)
        {
            _Fornecedorcontext = context;
            _FornecedorService = fornecedorService;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(mapearLista<FornecedorViewModel>(await _Fornecedorcontext.ObterTodos()));
        }

        // GET: Fornecedores/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View(new FornecedorViewModel());
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornencedor = mapear<Fornecedor>(fornecedorViewModel);
            await _FornecedorService.Adicionar(fornencedor);

            return RedirectToAction(nameof(Index));


        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }
            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(fornecedorViewModel);


            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
                await _Fornecedorcontext.Atualizar(fornecedor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FornecedorViewModelExists(fornecedorViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));


        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (!await FornecedorViewModelExists(fornecedorViewModel.Id))
                NotFound();


            await _Fornecedorcontext.Remover(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FornecedorViewModelExists(Guid id)
        {
            return await ObterFornecedorEndereco(id) != null;
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return mapear<FornecedorViewModel>(await _Fornecedorcontext.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return mapear<FornecedorViewModel>(await _Fornecedorcontext.ObterFornecedorProdutosEndereco(id));
        }
    }
}
