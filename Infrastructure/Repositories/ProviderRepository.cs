using Application.DTOs;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.SQLServer;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ProviderRepository : GenericSqlRepository<Provider>, IProviderRepository
    {
        private IMapper Mapper { get; }

        public ProviderRepository(
            Lazy<SqlServerDbContext> context,
            IMapper mapper
        ) : base(context)
        {
            Mapper = mapper;
        }

        public ListResultCollectionDto<ProviderDto> List(ProviderListRequestDto request)
        {
            IQueryable<Provider> query = GetBaseEntityQuery();

            // Filtro por CountryId a través de los servicios asociados
            if (request.CountryId.HasValue)
            {
                query = query.Where(p => p.Services!.Any(s => s.Countries!.Any(c => c.Id == request.CountryId.Value)));
            }

            // Filtro de búsqueda por Query (nombre o email del proveedor)
            if (!string.IsNullOrEmpty(request.Query))
            {
                var lowerQuery = request.Query.ToLower();
                query = query.Where(p =>
                    (p.Name != null && p.Name.ToLower().Contains(lowerQuery)) ||
                    (p.Email != null && p.Email.ToLower().Contains(lowerQuery))
                );
            }

            // Ordenación
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                Expression<Func<Provider, dynamic>>? orderByExpression = request.OrderBy.ToLower() switch
                {
                    "name" => p => p.Name!,
                    _ => p => p.Id
                };

                query = (request.OrderAsc ?? true)
                    ? query.OrderBy(orderByExpression)
                    : query.OrderByDescending(orderByExpression);
            }

            // Contar el total de resultados
            var totalCount = query.Count();

            // Paginación
            var providers = query
                .Skip(request.Offset ?? 0)
                .Take(request.Limit ?? 100)
                .ToList();

            // Mapear las entidades a DTOs
            var resultItems = Mapper.Map<IEnumerable<ProviderDto>>(providers);

            // Retornar el resultado
            return new ListResultCollectionDto<ProviderDto>
            {
                TotalCount = totalCount,
                Items = resultItems
            };
        }
    }
}
