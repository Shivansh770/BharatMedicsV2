using AutoMapper;
using BharatMedicsV2.Models;
using BharatMedicsV2.Models.DTOs;

namespace BharatMedicsV2
{
    public class DtoConfiguration : Profile
    {
        public DtoConfiguration() 
        { 
            CreateMap<Drug, DrugDTO>();
            CreateMap<DrugDTO, Drug>();
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierDTO, Supplier>();
        }

    }
}
