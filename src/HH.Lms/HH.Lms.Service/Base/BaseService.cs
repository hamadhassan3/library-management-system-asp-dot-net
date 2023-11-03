using AutoMapper;
using Folio3.Sbp.Service;
using HH.Lms.Common.Entity;
using HH.Lms.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public ServiceResult<TDto> AddAsync(TDto dto)
        {
            GenericRepository.Add(Mapper.Map<TEntity>(dto));
            return Success(dto);
        }

        public ServiceResult<TDto> UpdateAsync(TDto dto)
        {
            GenericRepository.Update(Mapper.Map<TEntity>(dto));
            return Success(dto);
        }

        public async Task<ServiceResult<TDto>> DeleteAsync(int id)
        {
            ServiceResult<TDto> dto = await GetAsync(id);
            GenericRepository.Update(Mapper.Map<TEntity>(dto.Data));
            return Result(default(TDto), true);
        }

        public async Task<ServiceResult<TDto>> GetAsync(int id)
        {
            TEntity entity = await GenericRepository.GetByIdAsync(id);
            return Success(Mapper.Map<TDto>(entity));
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
