using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Asaph.Implementations.Shared;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

using MongoDB.Driver;

namespace Asaph.Implementations.ServiceCallers.Database.Implementations {
    internal class DocumentDatabaseCaller : DocDbCaller {
        public DocumentDatabaseCaller() {
        }

        public Task<AsaphOperationResult> Insert<DocumentModel>(
            string dbName, string tableName, DocumentModel document) where DocumentModel : class =>
            Insert(dbName: dbName, tableName: tableName, document: new List<DocumentModel> { document });

        public Task<AsaphOperationResult> Insert<DocumentModel>(
            string dbName, string tableName, IEnumerable<DocumentModel> documents) where DocumentModel : class {
            try {
                return Task.Run<AsaphOperationResult>(() => {
                    MongoClient client = new();
                    IMongoDatabase database = client.GetDatabase(name: dbName);
                    IMongoCollection<DocumentModel> collection = database.GetCollection<DocumentModel>(name: tableName);
                    Task insertRequest = collection.InsertManyAsync(documents: documents);
                    return new GeneralAsaphOperationResult {
                        Status = OperationStatus.Success,
                        Message = $"Insert documents into [ {dbName} . {tableName} ]"
                    };
                });
            } catch (Exception ex) {
                return Task.Run<AsaphOperationResult>(() => {
                    return new GeneralAsaphOperationResult {
                        Status = OperationStatus.Failure,
                        Message = $"Failed to insert documents into [ {dbName} . {tableName} ]"
                    };
                });
            }
        }
    }
}
