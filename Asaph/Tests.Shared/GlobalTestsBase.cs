using System.Diagnostics.CodeAnalysis;

using Asaph.Global;

using Microsoft.Extensions.Configuration;

using Xunit.Abstractions;

namespace Asaph.Tests.Shared {
    /// <summary>
    /// Used for all Asaph TEST applications. Exposes Asaph's Globally available factories and functions.
    /// Uses testsettings.json for default configuration.
    /// </summary>
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
