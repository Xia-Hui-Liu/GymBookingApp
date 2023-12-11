using AutoMapper;
using GymBookingApp.Models;
using GymBookingApp.Models.ViewModels;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymBookingApp.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GymClass, GymClassesViewModel>()
                .ForMember(dest => dest.Attending, from =>
                from.MapFrom<AttendingResolver>());

            CreateMap<IEnumerable<GymClass>, IndexViewModel>()
                .ForMember(dest => dest.GymClasses, from =>
                from.MapFrom(g => g.ToList()));
        }
    }

    public class AttendingResolver : IValueResolver<GymClass, GymClassesViewModel, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AttendingResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Resolve(GymClass source, GymClassesViewModel destination, bool destMember, ResolutionContext context)
        {
            return source.ApplicationUsers is null ? false :
                source.ApplicationUsers.Any(a => a.ApplicationUserId ==
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        }
    }
}