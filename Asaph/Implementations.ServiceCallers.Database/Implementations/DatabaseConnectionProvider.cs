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
    internal class DbConnectionProvider : DatabaseConnectionProvider {
        private readonly IDictionary<string, ConnectionStringConfiguration> connectionStringsByName;
        public DbConnectionProvider(IConfiguration configuration) {
            ConnectionStringConfiguration[] connectionStrings =
                configuration.GetSection("ConnectionStrings").Get<ConnectionStringConfiguration[]>();
            this.connectionStringsByName =
                connectionStrings
                .GroupBy(connectionString => connectionString.Name, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.First())
                .ToDictionary(
                    keySelector: connectionString => connectionString.Name,
                    elementSelector: connectionString => connectionString,
                    comparer: StringComparer.OrdinalIgnoreCase);

            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("System.Data.MySqlClient", MySqlClientFactory.Instance);
        }

        internal class ConnectionStringConfiguration {
            public string Name { get; set; } = string.Empty;
            public string Provider { get; set; } = string.Empty;
            public string ConnectionString { get; set; } = string.Empty;
        }

        public IDbConnection AsaphDb() => _getDbConnection("Asaph");

        internal IDbConnection _getDbConnection(string connectionName) {
            if (!connectionStringsByName.TryGetValue(key: connectionName, out ConnectionStringConfiguration configuration)) 
                throw new Exception($"No DB config found for DB with Name [ {connectionName} ]");
            IDbConnection connection = DbProviderFactories.GetFactory(configuration.Provider).CreateConnection();
            connection.ConnectionString = configuration.ConnectionString;
            return connection;

        }
    }
}
