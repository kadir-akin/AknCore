using Core.Database.Concrate;
using Core.Database.EF.Abstract;
using Core.Database.EF.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Extantions
{
    public static class AddEFExtantions
    {
        public static IServiceCollection AddEFAknDbContext(this IServiceCollection services)
        {

            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var msSqlSection = _configuration.GetSection("MsSqlConfiguration");
            if (!msSqlSection.Exists())
                throw new System.Exception("MsSqlConfiguration Not Found");

            services.Configure<MsSqlConfiguration>(msSqlSection);

            var msSqlConfig =services.BuildServiceProvider().GetService<IOptions<MsSqlConfiguration>>();
           
            services.AddDbContext<AknDbContext>(options =>
            {
                options.UseSqlServer($"Server={msSqlConfig.Value.Server};Database={msSqlConfig.Value.Database};trusted_connection=true;User Id={msSqlConfig.Value.UserName};Password={msSqlConfig.Value.Password};");
            });

            if (msSqlConfig.Value.UseUnitOfWork)
            {
                services.AddSingleton(typeof(IEfUnitofWork), typeof(EfUnitOfWork));
            }

            return services;
        }
    }
}
