using AutoMapper;
using Folio3.Sbp.Service;
using HH.Lms.Common.Entity;
using HH.Lms.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HH.Lms.Service.Base
{
    public abstract class BaseService<TEntity, TDto>
        where TEntity : class, IBaseEntity
        where TDto : class, IDto
    {
        private GenericRepository<TEntity> GenericRepository { get; set; }

        private IMapper Mapper { get; set; }

        protected BaseService(
            GenericRepository<TEntity> genericRepository,
            IMapper mapper)
        {
            this.GenericRepository = genericRepository;
            this.Mapper = mapper;
        }

        public async Task<ServiceResult<TDto>> AddAsync(TDto dto)
        {
            TEntity res = GenericRepository.Add(Mapper.Map<TEntity>(dto));
            await GenericRepository.SaveChangesAsync();
            return Success(Mapper.Map<TDto>(res));
        }

        public async Task<ServiceResult<TDto>> UpdateAsync(TDto dto)
        {
            GenericRepository.Update(Mapper.Map<TEntity>(dto));
            await GenericRepository.SaveChangesAsync();
            return Success(dto);
        }

        public async Task<ServiceResult<TDto>> DeleteAsync(int id)
        {
            ServiceResult<TDto> dto = await GetAsync(id);
            GenericRepository.Delete(Mapper.Map<TEntity>(dto.Data));
            await GenericRepository.SaveChangesAsync();
            return Result(default(TDto), true);
        }

        public async Task<ServiceResult<TDto>> GetAsync(int id)
        {
            TEntity entity = await GenericRepository.GetByIdAsync(id);
            return Success(Mapper.Map<TDto>(entity));
        }

        public async Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync()
        {
            IEnumerable<TEntity> entity = await GenericRepository.GetAllAsync();
            return new ServiceResult<IEnumerable<TDto>> {
                    Success = true,
                    Errors =  new List<string>(),
                    Data = Mapper.Map<IEnumerable<TDto>>(entity)
                };
        }

        protected static ServiceResult<TDto> Success(TDto data)
        {
            return Result(data, true);
        }

        protected static ServiceResult<TDto> Failure(string errorMessage)
        {
            return Result(default(TDto), false, new List<string> { errorMessage });
        }

        protected static ServiceResult<TDto> Failure(List<string> errorMessages)
        {
            return Result(default(TDto), false, errorMessages);
        }

        protected static ServiceResult<TDto> Result(TDto data, bool success, List<string> errors = null)
        {
            return new ServiceResult<TDto>
            {
                Success = success,
                Data = data,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
