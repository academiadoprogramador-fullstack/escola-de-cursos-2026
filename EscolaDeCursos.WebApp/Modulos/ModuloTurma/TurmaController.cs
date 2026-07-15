using AutoMapper;
using FluentResults;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public class TurmaController(
    ServicoTurma servicoTurma,
    IMapper mapeador
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarTurmaDto> dtos = servicoTurma.SelecionarTodos();

        List<ListarTurmaViewModel> listarVms = mapeador.Map<List<ListarTurmaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTurmaViewModel cadastrarVm = new CadastrarTurmaViewModel(
            string.Empty,
            null,
            null,
            null,
            null,
            null
        );

        CarregarCursosEInstrutores();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTurmaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(cadastrarVm);
        }

        CadastrarTurmaDto dto = mapeador.Map<CadastrarTurmaDto>(cadastrarVm);

        Result resultado = servicoTurma.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            CarregarCursosEInstrutores();
            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesTurmaDto> resultado = servicoTurma.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        EditarTurmaViewModel editarVm = mapeador.Map<EditarTurmaViewModel>(resultado.Value);

        CarregarCursosEInstrutores();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarTurmaViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(editarVm);
        }

        EditarTurmaDto dto = mapeador.Map<EditarTurmaDto>(editarVm);

        Result resultado = servicoTurma.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            CarregarCursosEInstrutores();
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesTurmaDto> resultado = servicoTurma.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirTurmaViewModel excluirVm = mapeador.Map<ExcluirTurmaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTurmaViewModel excluirVm)
    {
        Result resultado = servicoTurma.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    private void CarregarCursosEInstrutores()
    {
        List<OpcaoCursoTurmaDto> cursos = servicoTurma.SelecionarCursos();
        List<OpcaoInstrutorTurmaDto> instrutores = servicoTurma.SelecionarInstrutores();

        ViewBag.Cursos = cursos
            .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
            .ToList();

        ViewBag.Instrutores = instrutores
            .Select(i => new SelectListItem(i.Nome, i.Id.ToString()))
            .ToList();
    }
}
