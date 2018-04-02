using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApiMiddleware
{
    public static class WebApiExtentsion
    {
        /// <summary>
        /// Adds WebApi services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>An <see cref="IMvcBuilder"/> that can be used to further configure the MVC services.</returns>
        public static IMvcBuilder AddWebApi(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            IMvcCoreBuilder mvcCoreBuilder = services.AddMvcCore();
            mvcCoreBuilder.AddApiExplorer();
            mvcCoreBuilder.AddAuthorization();
            mvcCoreBuilder.AddFormatterMappings();
            mvcCoreBuilder.AddDataAnnotations();
            mvcCoreBuilder.AddJsonFormatters();
            mvcCoreBuilder.AddCors();
            return new MvcBuilder(mvcCoreBuilder.Services, mvcCoreBuilder.PartManager);
        }

        /// <summary>
        /// Adds WebApi services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="setupAction">An <see cref="Action{MvcOptions}"/> to configure the provided <see cref="MvcOptions"/>.</param>
        /// <returns>An <see cref="IMvcBuilder"/> that can be used to further configure the MVC services.</returns>
        public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            if (setupAction ==null)
            {
                throw new ArgumentNullException("setupAction");
            }
            IMvcBuilder mvcBuilder = services.AddWebApi();
            mvcBuilder.Services.Configure(setupAction);
            return mvcBuilder;
        }
    }
}