using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso;

public class AulaProfile : Profile
{
    public AulaProfile()
    {
        CreateMap<ListarAulaDto, ListarAulaViewModel>();
        CreateMap<AdicionarAulaViewModel, AdicionarAulaDto>();
        CreateMap<EditarAulaViewModel, EditarAulaDto>();
        CreateMap<DetalhesAulaDto, EditarAulaViewModel>();
    }
}
