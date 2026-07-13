using AutoMapper;
using FluentResults;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso;

public class CursoController(
    ServicoCurso servicoCurso,
    ServicoAula servicoAula,
    IMapper mapeador
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarCursoDto> dtos = servicoCurso.SelecionarTodos();

        List<ListarCursoViewModel> listarVms = mapeador.Map<List<ListarCursoViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCursoViewModel cadastrarVm = new CadastrarCursoViewModel(
            string.Empty,
            null,
            null,
            null
        );

        CarregarCategorias();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCursoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarCategorias();
            return View(cadastrarVm);
        }

        CadastrarCursoDto dto = mapeador.Map<CadastrarCursoDto>(cadastrarVm);

        Result resultado = servicoCurso.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            CarregarCategorias();
            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesCursoDto> resultado = servicoCurso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        EditarCursoViewModel editarVm = mapeador.Map<EditarCursoViewModel>(resultado.Value);

        CarregarCategorias();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCursoViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarCategorias();
            return View(editarVm);
        }

        EditarCursoDto dto = mapeador.Map<EditarCursoDto>(editarVm);

        Result resultado = servicoCurso.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            CarregarCategorias();
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesCursoDto> resultado = servicoCurso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirCursoViewModel excluirVm = mapeador.Map<ExcluirCursoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCursoViewModel excluirVm)
    {
        Result resultado = servicoCurso.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult GerenciarAulas(Guid id)
    {
        Result<DetalhesCursoDto> resultadoCurso = servicoCurso.SelecionarPorId(id);

        if (resultadoCurso.IsFailed)
        {
            TempData.AddErrorMessage(resultadoCurso);
            return RedirectToAction(nameof(Listar));
        }

        List<ListarAulaDto> aulas = servicoAula.SelecionarPorCursoId(id);
        List<ListarAulaViewModel> aulasVm = mapeador.Map<List<ListarAulaViewModel>>(aulas);

        DetalhesCursoDto curso = resultadoCurso.Value;

        GerenciarAulasViewModel gerenciarVm = new GerenciarAulasViewModel(
            curso.Id,
            curso.Nome,
            curso.Nivel.ToString(),
            curso.CargaHoraria,
            curso.NomeCategoria,
            aulasVm
        );

        return View(gerenciarVm);
    }

    [HttpPost]
    public ActionResult AdicionarAula(AdicionarAulaViewModel adicionarVm)
    {
        if (!ModelState.IsValid)
        {
            TempData["MensagemErro"] = "Verifique os campos da aula antes de adicionar.";
            return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });
        }

        AdicionarAulaDto dto = mapeador.Map<AdicionarAulaDto>(adicionarVm);

        Result resultado = servicoAula.Adicionar(dto);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });
    }

    [HttpPost]
    public ActionResult EditarAula(EditarAulaViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            TempData["MensagemErro"] = "Verifique os campos da aula antes de salvar.";
            return RedirectToAction(nameof(GerenciarAulas), new { id = editarVm.CursoId });
        }

        EditarAulaDto dto = mapeador.Map<EditarAulaDto>(editarVm);

        Result resultado = servicoAula.Editar(dto);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(GerenciarAulas), new { id = editarVm.CursoId });
    }

    [HttpPost]
    public ActionResult RemoverAula(RemoverAulaViewModel removerVm)
    {
        Result resultado = servicoAula.Remover(removerVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(GerenciarAulas), new { id = removerVm.CursoId });
    }

    private void CarregarCategorias()
    {
        List<OpcaoCategoriaCursoDto> categorias = servicoCurso.SelecionarCategorias();

        ViewBag.Categorias = categorias
            .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
            .ToList();
    }
}

public record GerenciarAulasViewModel(
    Guid CursoId,
    string NomeCurso,
    string Nivel,
    int CargaHoraria,
    string NomeCategoria,
    List<ListarAulaViewModel> Aulas
);
