using AutoMapper;
using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public abstract class CrudService<TDto, TEntity, TRepository> : ICrudService<TDto>
        where TEntity : BaseEntity
        where TDto: BaseEntityDto
        where TRepository : IGenericRepository<TEntity>
    {
        protected IMapper Mapper { get; }
        protected TRepository Repository { get; }

        protected CrudService(
            IMapper mapper,
            TRepository repository
        )
        {
            Mapper = mapper;
            Repository = repository;
        }

        public virtual long Create(TDto dto)
        {
            TEntity? entity = Mapper.Map<TEntity>( dto );

            InitEntity(dto, entity);

            entity = Repository.Create(entity);

            Repository.Save();

            if (entity == null)
                throw new TekusException($"Unable to create entity [{typeof(TEntity).Name}]");

            return entity.Id;
        }

        public void Delete(long id)
        {
            TEntity entity = CheckEntity(id);

            Repository.Delete(entity);

            Repository.Save();
        }

        public TDto Read(long id)
        {
            return Mapper.Map<TDto>(
                CheckEntity(id)
                );
        }

        public virtual void Update(TDto dto)
        {
            TEntity entity = CheckEntity(dto.Id);

            UpdateEntity(dto, entity);

            Repository.Save();
        }

        /// <summary>
        /// Initializes entity WHEN IT'S BEING CREATED 
        /// </summary>
        /// <param name="entity">Entity to be initialized</param>
        protected virtual void InitEntity(TDto dto, TEntity entity)
        {
        }

        /// <summary>
        /// Checks whether an entity exists
        /// </summary>
        /// <param name="entityId">ID of entity to check</param>
        /// <returns>Entity matching ID specified</returns>
        /// <exception cref="TekusException">When entity does not exist</exception>
        protected TEntity CheckEntity(long entityId)
        {
            TEntity? entity = Repository.GetById(entityId);

            if (entity == null)
                throw new TekusException($"Entity [{entityId}] of type [{typeof(TEntity).Name}] does not exist");

            return entity;
        }

        /// <summary>
        /// Establishes the mapping between the updated entity
        /// </summary>
        /// <param name="dto">Updated entity</param>
        /// <param name="existingEntity">Existing entity to be updated</param>
        protected virtual void UpdateEntity(TDto dto, TEntity existingEntity) =>
            Mapper.Map(dto, existingEntity);
    }
}
