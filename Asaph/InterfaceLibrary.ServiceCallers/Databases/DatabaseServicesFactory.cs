using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Asaph.InterfaceLibrary.Shared;

namespace Asaph.InterfaceLibrary.ServiceCallers.Databases {
    public interface DatabaseServicesFactory {
        DatabaseConnectionProvider ConnectionProvider();
        DatabaseStoredProcedureCaller StoredProcedureCaller();
    }

    public interface DatabaseStoredProcedureCaller {
        Task<AsaphDbQueryResult<Model>> QueryStoredProcedure<Model>(
            string procedureName, IDbConnection connection, object parameters = null);
    }

    public interface DatabaseConnectionProvider {
        IDbConnection AsaphDb();
    }

    public interface AsaphDbQueryResult<Model> : AsaphOperationResult {
        IEnumerable<Model> Records { get; }
    }
}
