using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions
{
    public static class ServiceExtensionCollection
    {
        public static void AddServicesExtencions(this IServiceCollection service)
        {
            var assembly = typeof(ServiceExtensionCollection).Assembly;

            service.AddMediatR(opt => opt.RegisterServicesFromAssembly(assembly));
            service.AddAutoMapper(assembly);
            service.AddValidatorsFromAssembly(assembly)
                .AddFluentValidationAutoValidation();

            service.AddHttpContextAccessor();
            service.AddScoped<IUserContext, UserContext>();

        }
    }
}
