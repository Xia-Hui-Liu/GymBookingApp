using AutoMapper;
using GymBookingApp.Models;
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

            // Map the list of GymClass entities directly to IndexViewModel
            var dtos = _mapper.Map<IndexViewModel>(gymClasses);

            return dtos;
        }


        public async Task<GymClass> GetByIdAsync(int? id)
        {
            var gymClass = await _gymClassRepository.GetByIdAsync(id);
            return gymClass;
        }


        //public async Task<GameDto> UpdateAsync(Guid id, GameForUpdateDto dto)
        //{
        //    var game = _mapper.Map<Game>(dto);
        //    _unitOfWork.GameRepository.Update(game);
        //    await _unitOfWork.CompleteAsync();
        //    return _mapper.Map<GameDto>(game);
        //}

        public async Task<GymClass?> PostAsync(GymClass gymClass)
        {
            try
            {
                _gymClassRepository.Add(gymClass);
                await _gymClassRepository.CompleteAsyncTask().ConfigureAwait(false);
                return gymClass; // Returning the added GymClass directly
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //public async Task<GameDto> DeleteAsync(Guid id)
        //{
        //    var existGame = await _unitOfWork.GameRepository.GetAsync(id);

        //    return _mapper.Map<GameDto>(existGame);

        //}


    }
}
