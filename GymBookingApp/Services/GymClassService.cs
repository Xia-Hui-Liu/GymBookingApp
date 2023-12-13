using AutoMapper;
using GymBookingApp.Models.ViewModels;
using GymBookingApp.Repositories;
using static GymBookingApp.Services.GymClassService;

namespace GymBookingApp.Services
{
    public class GymClassService : IGymClassService
    {

        private readonly IGymClassRepository _gymClassRepository;
        private readonly IMapper _mapper;

        public GymClassService(IGymClassRepository gymClassRepository, IMapper mapper)
        {
            _gymClassRepository = gymClassRepository;
            _mapper = mapper;

        }

        public async Task<IndexViewModel> GetAllAsync()
        {
            var gymClasses = await _gymClassRepository.GetAllAsync();

            // Map the list of GymClass entities directly to GymClassesViewModel
            var dtos = _mapper.Map<IndexViewModel>(gymClasses);

            // Wrap each GymClassesViewModel in an IndexViewModel
            //var indexViewModels = dtos.Select(dto => new IndexViewModel { GymClasses = new List<GymClassesViewModel> { dto } });

            return dtos;
        }


        //public async Task<GameDto> GetAsync(Guid id)
        //{
        //    var game = await _unitOfWork.GameRepository.GetAsync(id);

        //    var dtos = _mapper.Map<GameDto>(game);

        //    return dtos;
        //}


        //public async Task<GameDto> UpdateAsync(Guid id, GameForUpdateDto dto)
        //{
        //    var game = _mapper.Map<Game>(dto);
        //    _unitOfWork.GameRepository.Update(game);
        //    await _unitOfWork.CompleteAsync();
        //    return _mapper.Map<GameDto>(game);
        //}

        //public async Task<GameDto?> PostAsync(GameForCreationDto dto)
        //{
        //    var game = _mapper.Map<Game>(dto);
        //    _unitOfWork.GameRepository.Add(game);
        //    await _unitOfWork.CompleteAsync();
        //    return _mapper.Map<GameDto>(game);
        //}

        //public async Task<GameDto> DeleteAsync(Guid id)
        //{
        //    var existGame = await _unitOfWork.GameRepository.GetAsync(id);

        //    return _mapper.Map<GameDto>(existGame);

        //}

    }
}
