using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso;

public class CursoProfile : Profile
{
    public CursoProfile()
    {
        CreateMap<ListarCursoDto, ListarCursoViewModel>();
        CreateMap<CadastrarCursoViewModel, CadastrarCursoDto>();
        CreateMap<EditarCursoViewModel, EditarCursoDto>();
        CreateMap<DetalhesCursoDto, EditarCursoViewModel>();
        CreateMap<DetalhesCursoDto, ExcluirCursoViewModel>();
    }
}
