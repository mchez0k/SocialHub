using Autofac;
using SocialHub.Auth.Application.Commands;

namespace SocialHub.Auth.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(RegisterUserCommand).Assembly).AsSelf();
        }
    }
}
