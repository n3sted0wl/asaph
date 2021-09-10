using Asaph.Implementations.ServiceCallers.Database.Implementations;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;

using Microsoft.Extensions.Configuration;

namespace Asaph.Implementations.ServiceCallers.Database {
    public class AsaphDatabaseServicesFactory : DatabaseServicesFactory {
        private readonly IConfiguration configuration;
        public AsaphDatabaseServicesFactory(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public DatabaseConnectionProvider ConnectionProvider() => new DbConnectionProvider(configuration);

        public RDBStoredProcedureCaller StoredProcedureCaller() => new SpExecutor();

        public DocDbCaller DocDbCaller() => new DocumentDatabaseCaller();
    }
}
