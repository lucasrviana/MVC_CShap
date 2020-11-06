using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DevIO.App.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProduto _Produtocontext;
        private readonly IFornecedor _Fornecedorcontext;

        public ProdutosController(IProduto produtocontext,
                                  IFornecedor fornecedorcontext,
                                  IMapper mapper) : base(mapper)
        {
            _Produtocontext = produtocontext;
            _Fornecedorcontext = fornecedorcontext;
        }


        // GET: ProdutoViewModels
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = await ObterProduto()
            return View(mapearLista<ProdutoViewModel>(await _Produtocontext.ObterProdutosFornecedor()));
        }

        // GET: ProdutoViewModels/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        // GET: ProdutoViewModels/Create
        public async Task<IActionResult> Create()
        {
            var produtoviewModel = await PopularFonecedor(new ProdutoViewModel());
            return View(produtoviewModel);
        }

        // POST: ProdutoViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            var produtoviewModel = await PopularFonecedor(produtoViewModel);
            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo)) {
                return View(produtoviewModel);
            }

            produtoviewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _Produtocontext.Adicionar(mapear<Produto>(produtoViewModel));

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoViewModels/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        // POST: ProdutoViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);

            if (!ModelState.IsValid) return View(produtoViewModel);

            produtoViewModel.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;

            if(produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(produtoViewModel);
                }

                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;


            await _Produtocontext.Atualizar(mapear<Produto>(produtoAtualizacao));

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoViewModels/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        // POST: ProdutoViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            await _Produtocontext.Remover(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = mapear<ProdutoViewModel>(await _Produtocontext.ObterProdutoFornecedor(id));
            produto.Fornecedores = mapearLista<FornecedorViewModel>(await _Fornecedorcontext.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFonecedor(ProdutoViewModel produto)
        {
            produto.Fornecedores = mapearLista<FornecedorViewModel>(await _Fornecedorcontext.ObterTodos());
            return produto;
        }

        private async Task<bool> UploadArquivo(IFormFile imagemUpload, string imgPrefixo)
        {
            if (imagemUpload.Length <= 0) return false;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagens", imgPrefixo + imagemUpload.FileName);

            if(System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já eciste um arquivo com este nome!");
                return false;
            }


            using( var stream = new FileStream(path, FileMode.Create))
            {
                await imagemUpload.CopyToAsync(stream);
            }
            return true;
        }

    }
}
