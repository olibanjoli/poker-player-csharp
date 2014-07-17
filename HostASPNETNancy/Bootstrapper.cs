using Nancy;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.TinyIoc;

namespace HostASPNETNancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get
            {
                return new DiagnosticsConfiguration() { Password = "1337"};
            }
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            StaticConfiguration.EnableRequestTracing = true;
        }
    }
}