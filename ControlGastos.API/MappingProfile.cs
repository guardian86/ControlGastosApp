using AutoMapper;
using ControlGastos.Core.DTOs;
using ControlGastos.Core.Entities;

namespace ControlGastos.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TipoGasto, TipoGastoDto>().ReverseMap();
            CreateMap<FondoMonetario, FondoMonetarioDto>().ReverseMap();
            CreateMap<Deposito, DepositoDto>().ReverseMap();
            CreateMap<Presupuesto, PresupuestoDto>().ReverseMap();
            // Add other mappings as needed
        }
    }
}
