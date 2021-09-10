using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Asaph.InterfaceLibrary.Shared;

namespace Asaph.InterfaceLibrary.ServiceCallers.Databases {
    public interface DatabaseServicesFactory {
        DatabaseConnectionProvider ConnectionProvider();
        RDBStoredProcedureCaller StoredProcedureCaller();
        DocDbCaller DocDbCaller();
    }

    public interface RDBStoredProcedureCaller {
        Task<AsaphDbQueryResult<RecordModel>> QueryStoredProcedure<RecordModel>(
            string procedureName, IDbConnection connection, object parameters = null);
    }

    public interface DocDbCaller {
        Task<AsaphOperationResult> Insert<DocumentModel>(
            string dbName, string tableName, DocumentModel document) where DocumentModel : class;
        Task<AsaphOperationResult> Insert<DocumentModel>(
            string dbName, string tableName, IEnumerable<DocumentModel> documents) where DocumentModel : class;
    }

    public interface DatabaseConnectionProvider {
        IDbConnection AsaphDb();
    }

    public interface AsaphDbQueryResult<Model> : AsaphOperationResult {
        IEnumerable<Model> Records { get; }
    }
}
