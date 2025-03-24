using AutoMapper;
using EcoShrimp.Admin.Areas.Admin.ViewModels.Categories;
using EcoShrimp.Data.Entities;

namespace EcoShrimp.Admin.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<CateVM, AppCategories>()
				 .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
		}
	}
}