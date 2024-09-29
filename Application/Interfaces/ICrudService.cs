using Domain.Exceptions;

namespace Application.Interfaces
{
    public interface ICrudService<TDto>
    {
        /// <summary>
        /// Creates a <see cref="TDto"/>
        /// </summary>
        /// <param name="dto"><see cref="TDto"/> to be created</param>
        /// <returns>Id of <see cref="TDto"/> created</returns>
        public long Create(TDto dto);

        /// <summary>
        /// Reads a <see cref="TDto"/>
        /// </summary>
        /// <param name="id">Id of <see cref="TDto"/> to read</param>
        /// <returns><see cref="TDto"/> matching ID specified</returns>
        /// <exception cref="TekusException">When <see cref="TDto"/> does not exist</exception>
        public TDto Read(long id);

        /// <summary>
        /// Updates a <see cref="TDto"/>
        /// </summary>
        /// <param name="dto"><see cref="TDto"/> to update</param>
        /// <exception cref="TekusException">When <see cref="TDto"/> does not exist</exception>
        public void Update(TDto dto);

        /// <summary>
        /// Deletes a <see cref="TDto"/>
        /// </summary>
        /// <param name="id">Id of <see cref="TDto"/> to delete</param>
        /// <exception cref="TekusException">When <see cref="TDto"/> does not exist</exception>
        public void Delete(long id);
    }
}
