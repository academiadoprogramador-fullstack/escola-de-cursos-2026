using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public class MatriculaProfile : Profile
{
    public MatriculaProfile()
    {
        CreateMap<ListarMatriculaDto, ListarMatriculaViewModel>();
        CreateMap<AdicionarMatriculaViewModel, AdicionarMatriculaDto>();
    }
}
