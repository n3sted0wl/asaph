using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

using Dapper;

namespace Asaph.Implementations.ServiceCallers.Database.Implementations {
    internal class SpExecutor : RDBStoredProcedureCaller {
        public Task<AsaphStorageReadResult<Model>> QueryStoredProcedure<Model>(
            string procedureName, IDbConnection connection, object parameters = null) {
            return Task.Run<AsaphStorageReadResult<Model>>(() => {
                try {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    stopwatch.Start();
                    IEnumerable<Model> databaseRecords;
                    using (connection) {
                        connection.Open();
                        databaseRecords = connection.Query<Model>(
                            sql: procedureName,
                            param: parameters,
                            commandType: CommandType.StoredProcedure);
                    }
                    stopwatch.Stop();
                    return new GeneralStorageResult<Model>(
                        status: OperationStatus.Success,
                        message: $"SP [ {procedureName} ] executed in [ {stopwatch.ElapsedMilliseconds} ] ms",
                        records: databaseRecords);
                } catch (Exception ex) {
                    return new GeneralStorageResult<Model>(
                        status: OperationStatus.Failure,
                        message: $"SP [ {procedureName} ] failed to execute [ {ex.Message} ]");
                }
            });
        }
    }
}
