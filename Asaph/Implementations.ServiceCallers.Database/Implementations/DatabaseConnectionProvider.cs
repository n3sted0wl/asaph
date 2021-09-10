using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

using Asaph.InterfaceLibrary.ServiceCallers.Databases;

using Microsoft.Extensions.Configuration;

using MySql.Data.MySqlClient;

namespace Asaph.Implementations.ServiceCallers.Database.Implementations {
    internal enum ConnectionStringNames {
        Asaph,
    }

    internal class DbConnectionProvider : DatabaseConnectionProvider {
        private readonly IDictionary<ConnectionStringNames, ConnectionStringConfiguration> connectionStringsByName;
        public DbConnectionProvider(IConfiguration configuration) {
            IConfiguration connectionStringsConfigSection = configuration.GetSection("ConnectionStrings");
            ConnectionStringNames[] names = (ConnectionStringNames[])Enum.GetValues(typeof(ConnectionStringNames));
            this.connectionStringsByName =
                names
                .ToDictionary(
                    keySelector: connectionStringName => connectionStringName,
                    elementSelector: connectionName =>
                        connectionStringsConfigSection
                        .GetSection(connectionName.ToString())
                        .Get<ConnectionStringConfiguration>());

            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("System.Data.MySqlClient", MySqlClientFactory.Instance);
        }

        internal class ConnectionStringConfiguration {
            public string Provider { get; set; } = string.Empty;
            public string ConnectionString { get; set; } = string.Empty;
        }

        public IDbConnection AsaphDb() => _getDbConnection(ConnectionStringNames.Asaph);

        internal IDbConnection _getDbConnection(ConnectionStringNames connectionName) {
            if (!connectionStringsByName.TryGetValue(key: connectionName, out ConnectionStringConfiguration configuration)) 
                throw new Exception($"No DB config found for DB with Name [ {connectionName} ]");
            IDbConnection connection = DbProviderFactories.GetFactory(configuration.Provider).CreateConnection();
            connection.ConnectionString = configuration.ConnectionString;
            return connection;

        }
    }
}
