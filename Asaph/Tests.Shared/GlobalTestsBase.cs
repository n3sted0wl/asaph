using System.Diagnostics.CodeAnalysis;

using Asaph.Implementation.DependencyInjectors;
using Asaph.InterfaceLibrary.BusinessRules.Injectors;

using Microsoft.Extensions.Configuration;

using Xunit.Abstractions;

namespace Asaph.Tests.Shared {
    /// <summary>
    /// Used for all Asaph TEST applications. Exposes Asaph's Globally available factories and functions.
    /// Uses testsettings.json for default configuration.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GlobalTestsBaseClass {
        private readonly AppManagementInjector _appManagementInjector;
        private readonly SongTitlesAppInjector _songTitlesInjector;
        
        protected readonly IConfiguration Configuration;
        protected readonly ITestOutputHelper OutputHelper;

        protected AppManagementInjector AsaphInjectionManager() => _appManagementInjector;
        protected SongTitlesAppInjector SongTitlesAppInjector() => _songTitlesInjector;

        public GlobalTestsBaseClass(ITestOutputHelper outputHelper) {
            OutputHelper = outputHelper;
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"testsettings.json", false, true)
                .Build();
            _appManagementInjector = new AsaphAppManagementInjector(configuration: Configuration);
            _songTitlesInjector = new AsaphSongManagementBusinessRulesInjector(configuration: Configuration);
        }
    }
}
