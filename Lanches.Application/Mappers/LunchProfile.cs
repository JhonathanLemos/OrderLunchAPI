using AutoMapper;
using Lanches.Application.Commands.Lunchs.CreateLunch;
using Lanches.Application.Commands.Lunchs.UpdateLunch;
using Lanches.Application.Dtos.ViewModels;
using Lanches.Core.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Lanches.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class LunchProfile : Profile
    {
        public LunchProfile()
        {
            CreateMap<CreateLunchCommand, LunchViewModel>();
            CreateMap<UpdateLunchCommand, LunchViewModel>();
            CreateMap<UpdateLunchCommand, Lunch>()
                .ForMember(x => x.Ingredients, opt => opt.Ignore());
        }
    }
}
