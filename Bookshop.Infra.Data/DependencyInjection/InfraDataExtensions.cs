using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;
using Bookshop.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookshop.Infra.Data.DependencyInjection
{
    public static class InfraDataExtensions
    {
        public static IServiceCollection AddPostgreSqlConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MainContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IAssuntoRepository, AssuntoRepository>();
            services.AddScoped<IRelatorioRepository, RelatorioRepository>();
            services.AddScoped<ICanalRepository, CanalRepository>();
            services.AddScoped<ICanalPrecoRepository, CanalPrecoRepository>();

            // Register IBaseRepository for each entity type
            services.AddScoped<IBaseRepository<LivroEntity>, BaseRepository<LivroEntity>>();
            services.AddScoped<IBaseRepository<AutorEntity>, BaseRepository<AutorEntity>>();
            services.AddScoped<IBaseRepository<AssuntoEntity>, BaseRepository<AssuntoEntity>>();
            services.AddScoped<IBaseRepository<CanalPrecoEntity>, BaseRepository<CanalPrecoEntity>>();
            services.AddScoped<IBaseRepository<CanalEntity>, BaseRepository<CanalEntity>>();

            return services;
        }
    }
}