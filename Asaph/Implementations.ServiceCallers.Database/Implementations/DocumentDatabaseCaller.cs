using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Asaph.Implementations.Shared;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Asaph.Implementations.ServiceCallers.Database.Implementations {
    internal class DocumentDatabaseCaller : DocDbCaller {
        class AsaphMongoDBDateTimeSerializer : DateTimeSerializer {
            public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) {
                var obj = base.Deserialize(context, args);
                return new DateTime(obj.Ticks, DateTimeKind.Unspecified);
            }

            public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value) {
                var utcValue = new DateTime(value.Ticks, DateTimeKind.Utc);
                base.Serialize(context, args, utcValue);
            }
        }

        class AsaphSerializationProvider : IBsonSerializationProvider {
            public IBsonSerializer GetSerializer(Type type) {
                if (type == typeof(Guid)) {
                    return new GuidSerializer(BsonType.String);
                }
                if (type == typeof(DateTime)) {
                    return new AsaphMongoDBDateTimeSerializer();
                }
                return null;
            }
        }

        public DocumentDatabaseCaller() {
            BsonSerializer.RegisterSerializationProvider(provider: new AsaphSerializationProvider());
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
                    insertRequest.Wait();
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

        public Task<AsaphStorageReadResult<DocumentModel>> Read<DocumentModel>(
            string dbName, string tableName) where DocumentModel : class {
            try {
                return Task.Run<AsaphStorageReadResult<DocumentModel>>(() => {
                    MongoClient client = new();
                    IMongoDatabase database = client.GetDatabase(name: dbName);
                    IMongoCollection<DocumentModel> collection = database.GetCollection<DocumentModel>(name: tableName);
                    List<DocumentModel> readResult = collection.Find(new BsonDocument()).ToList();
                    return new GeneralStorageResult<DocumentModel>(
                        status: OperationStatus.Success,
                        message: $"Read documents from [ {dbName} . {tableName} ]",
                        records: readResult);
                });
            } catch (Exception ex) {
                return Task.Run<AsaphStorageReadResult<DocumentModel>>(() => {
                    return new GeneralStorageResult<DocumentModel>(
                        status: OperationStatus.Failure,
                        message: $"Failed to read documents from [ {dbName} . {tableName} ] [ {ex.Message} ]");
                });
            }
        }
    }
}
