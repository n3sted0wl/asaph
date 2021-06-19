using System.Diagnostics.CodeAnalysis;

using Asaph.Global;

using Microsoft.Extensions.Configuration;

using Xunit.Abstractions;

namespace Asaph.Tests.Shared {
    [ExcludeFromCodeCoverage]
    public class GlobalTestsBaseClass {
        private readonly AsaphInjectionManager _injectionManager;
        
        protected readonly IConfiguration Configuration;
        protected readonly ITestOutputHelper OutputHelper;
        protected AsaphInjectionManager AsaphInjectionManager() => _injectionManager;

        public GlobalTestsBaseClass(ITestOutputHelper outputHelper) {
            OutputHelper = outputHelper;
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"testsettings.json", false, true)
                .Build();
            _injectionManager = new AsaphInjectionManager(configuration: Configuration);
        }
    }
}
