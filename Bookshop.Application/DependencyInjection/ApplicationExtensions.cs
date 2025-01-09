using Bookshop.Application.Business;
using Bookshop.Application.Profiles;
using Bookshop.Application.Validator.Assunto;
using Bookshop.Application.Validator.Autor;
using Bookshop.Application.Validator.Livro;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.Interfaces.Business;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookshop.Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(LivroProfile)); 
            services.AddAutoMapper(typeof(AutorProfile));
            services.AddAutoMapper(typeof(AssuntoProfile));

            services.AddTransient<IValidator<LivroRequest>, LivroValidator>();
            services.AddTransient<IValidator<AutorRequest>, AutorValidator>();
            services.AddTransient<IValidator<AssuntoRequest>, AssuntoValidator>();

            services.AddScoped<ILivroBusiness, LivroBusiness>();
            services.AddScoped<IAutorBusiness, AutorBusiness>();
            services.AddScoped<IAssuntoBusiness, AssuntoBusiness>(); 
            services.AddScoped<IRelatorioBusiness, RelatorioBusiness>();

            return services;
        }
    }
}
