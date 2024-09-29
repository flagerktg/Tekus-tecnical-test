﻿using Application.DTOs;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ServiceRepository : GenericSqlRepository<Service>, IServiceRepository
    {
        private readonly SqlServerDbContext _context;
        private IMapper Mapper { get; }

        public ServiceRepository(
            Lazy<SqlServerDbContext> context,
            IMapper mapper
        ) : base(context)
        {
            _context = context.Value;
            Mapper = mapper;
        }

        public ListResultCollectionDto<ServiceDto> List(ServiceListRequestDto request)
        {
            IQueryable<Service> query = GetBaseEntityQuery();

            if (request.CountryId.HasValue)
            {
                query = query.Where(s => s.Countries!.Any(c => c.Id == request.CountryId.Value));
            }

            // Filtro de búsqueda por Query (nombre del servicio)
            if (!string.IsNullOrEmpty(request.Query))
            {
                var lowerQuery = request.Query.ToLower();
                query = query.Where(s =>
                    (s.Name != null && s.Name.ToLower().Contains(lowerQuery))
                );
            }

            // Ordenación
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                Expression<Func<Service, dynamic>>? orderByExpression = request.OrderBy.ToLower() switch
                {
                    "name" => s => s.Name!,
                    _ => s => s.Id
                };

                query = (request.OrderAsc ?? true)
                    ? query.OrderBy(orderByExpression)
                    : query.OrderByDescending(orderByExpression);
            }

            // Contar el total de resultados
            var totalCount = query.Count();

            // Paginación
            var services = query
                .Skip(request.Offset ?? 0)
                .Take(request.Limit ?? 100)
                .ToList();

            // Mapear las entidades a DTOs
            var resultItems = Mapper.Map<IEnumerable<ServiceDto>>(services);

            // Retornar el resultado
            return new ListResultCollectionDto<ServiceDto>
            {
                TotalCount = totalCount,
                Items = resultItems
            };
        }

    }
}
