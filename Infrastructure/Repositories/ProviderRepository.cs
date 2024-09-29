using Application.DTOs;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
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

            if (request.CountryId.HasValue)
            {
                query = query.Where(p => p.Services!.Any(s => s.Countries!.Any(c => c.Id == request.CountryId.Value)));
            }

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
        public Dictionary<string, int> GetProvidersByCountry()
        {
            return Context.Value.Providers
                .Include(p => p.Services!)
                .ThenInclude(s => s.Countries!)
                .SelectMany(p => p.Services!.SelectMany(s => s.Countries!))
                .GroupBy(c => c.Code ?? "UNKNOWN") 
                .ToDictionary(g => g.Key, g => g.Count());
        }


    }
}
