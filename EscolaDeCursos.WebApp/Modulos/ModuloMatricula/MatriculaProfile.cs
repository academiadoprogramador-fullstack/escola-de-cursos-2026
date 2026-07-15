using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public class MatriculaProfile : Profile
{
    public MatriculaProfile()
    {
        CreateMap<ListarMatriculaDto, ListarMatriculaViewModel>();
        CreateMap<CadastrarMatriculaViewModel, CadastrarMatriculaDto>();
        CreateMap<EditarMatriculaViewModel, EditarMatriculaDto>();
        CreateMap<DetalhesMatriculaDto, EditarMatriculaViewModel>();
        CreateMap<DetalhesMatriculaDto, ExcluirMatriculaViewModel>();
    }
}
