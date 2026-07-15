using AutoMapper;
using FluentResults;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
using EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public class MatriculaController(
    ServicoMatricula servicoMatricula,
    ServicoTurma servicoTurma,
    ServicoAluno servicoAluno,
    IMapper mapeador
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMatriculaDto> dtos = servicoMatricula.SelecionarTodos();

        List<ListarMatriculaViewModel> listarVms = mapeador.Map<List<ListarMatriculaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMatriculaViewModel cadastrarVm = new(TurmaId: null, AlunoId: null);

        CarregarTurmas();
        CarregarAlunos();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMatriculaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(cadastrarVm);
        }

        CadastrarMatriculaDto dto = mapeador.Map<CadastrarMatriculaDto>(cadastrarVm);

        Result resultado = servicoMatricula.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            CarregarTurmas();
            CarregarAlunos();
            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesMatriculaDto> resultado = servicoMatricula.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        EditarMatriculaViewModel editarVm = mapeador.Map<EditarMatriculaViewModel>(resultado.Value);

        CarregarTurmas();
        CarregarAlunos();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMatriculaViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(editarVm);
        }

        EditarMatriculaDto dto = mapeador.Map<EditarMatriculaDto>(editarVm);

        Result resultado = servicoMatricula.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            CarregarTurmas();
            CarregarAlunos();
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesMatriculaDto> resultado = servicoMatricula.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirMatriculaViewModel excluirVm = mapeador.Map<ExcluirMatriculaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMatriculaViewModel excluirVm)
    {
        Result resultado = servicoMatricula.Remover(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    private void CarregarTurmas()
    {
        List<ListarTurmaDto> turmas = servicoTurma.SelecionarTodos();

        ViewBag.Turmas = turmas
            .Select(t => new SelectListItem(t.Nome, t.Id.ToString()))
            .ToList();
    }

    private void CarregarAlunos()
    {
        List<ListarAlunoDto> alunos = servicoAluno.SelecionarTodos();

        ViewBag.Alunos = alunos
            .Select(a => new SelectListItem($"{a.Nome} ({a.NumeroMatricula})", a.Id.ToString()))
            .ToList();
    }
}
