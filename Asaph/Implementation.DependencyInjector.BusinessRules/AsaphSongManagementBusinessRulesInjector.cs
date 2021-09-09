using Asaph.Implementation.SongTitles;
using Asaph.Implementations.ServiceCallers.Database;
using Asaph.InterfaceLibrary.BusinessRules.Injectors;
using Asaph.InterfaceLibrary.BusinessRules.SongTitles;
using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;

using Implementations.AppManagement.RecordRevisions;

using Microsoft.Extensions.Configuration;

namespace Asaph.Implementation.DependencyInjectors {

    public abstract class InjectorsBaseClass {
        internal readonly IConfiguration _baseConfiguration;
        public InjectorsBaseClass(IConfiguration configuration) {
            IConfigurationRoot mergedConfiguration =
                new ConfigurationBuilder()
                .AddJsonFile($"appsettings.global.json", false, true)
                .AddConfiguration(configuration)
                .Build();
            this._baseConfiguration = mergedConfiguration;
        }

        public DatabaseServicesFactory DatabaseServicesFactory() =>
            new AsaphDatabaseServicesFactory(configuration: _baseConfiguration.GetSection("Database"));
    }

    public class AsaphAppManagementInjector : InjectorsBaseClass, AppManagementInjector {
        public AsaphAppManagementInjector(IConfiguration configuration) : base(configuration) { }

        public AsaphRevisionsFactory RevisionsFactory() => new RevisionsFactory(DatabaseServicesFactory());
    }

    public class AsaphSongManagementBusinessRulesInjector : InjectorsBaseClass, SongTitlesAppInjector {
        public AsaphSongManagementBusinessRulesInjector(IConfiguration configuration) : base(configuration) { }

        public SongTitlesFactory SongTitlesFactory() => new SongTitlesBusinessRulesFactory();
    }
}
