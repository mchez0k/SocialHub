using Autofac;
using Microsoft.EntityFrameworkCore;
using SocialHub.Auth.Persistance;
using SocialHub.Shared;

namespace SocialHub.Auth.AutofacModules;

public class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c =>
        {
            var context = c.Resolve<IComponentContext>();
            var configuration = context.Resolve<IConfiguration>();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlDB"), sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                          .EnableRetryOnFailure()
                          .CommandTimeout(10)
                          .MigrationsHistoryTable("__EFMigrationsHistory", "Auth");
            });

            return new AppDbContext(optionsBuilder.Options);
        }).As<DbContext>().InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();
    }
}