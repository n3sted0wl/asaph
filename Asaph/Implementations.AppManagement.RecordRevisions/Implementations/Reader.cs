using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Asaph.Implementations.AppManagement.RecordRevisions.Models;
using Asaph.Implementations.Shared;
using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;
using Asaph.Shared.ExtensionMethods;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class RevisionsReader : AsaphRevisionsReader {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public RevisionsReader(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public Task<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>> Revisions<Model>(
            Guid tenantId, string typeId, string referenceId) where Model : class =>
            Revisions<Model>(tenantId: tenantId, typeId: typeId, referenceIds: new List<string> { referenceId });

        public Task<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>> Revisions<Model>(
            Guid tenantId, string typeId, IEnumerable<string> referenceIds) where Model : class {
            return Task.Run<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>>(() => { 
                try {
                    Task<AsaphDbQueryResult<RecordRevisionDbModel>> dbRequest =
                        databaseServicesFactory
                        .StoredProcedureCaller()
                        .QueryStoredProcedure<RecordRevisionDbModel>(
                            procedureName: "SP_ReadRevHistory",
                            connection: databaseServicesFactory.ConnectionProvider().AsaphDb(),
                            parameters: new {
                                TenantId = tenantId,
                                TypeId = typeId,
                                ReferenceIds = referenceIds.Select(refId => new { ReferenceId = refId }).ToDataTable(),
                            });
                    AsaphDbQueryResult<RecordRevisionDbModel> queryReadResult = dbRequest.Result;
                    if (queryReadResult.HasFailureStatus())
                        return new GeneralAsaphOperationResult<IEnumerable<RecordRevision<Model>>>() {
                            Status = queryReadResult.Status,
                            Message = $"Call to DB for Record Revisions failed [ {queryReadResult.Message} ]",
                            Payload = new List<RecordRevision<Model>>(),
                        };
                    List<RecordRevision<Model>> records =
                        queryReadResult
                        .Records
                        .AsParallel()
                        .Select(dbModel => new RevisionRecordForPersistence<Model>(dbModel: dbModel) as RecordRevision<Model>)
                        .ToList();
                    return new GeneralAsaphOperationResult<IEnumerable<RecordRevision<Model>>>() {
                        Status = OperationStatus.Success,
                        Message = $"Read Revisions",
                        Payload = records
                    };
                } catch (Exception ex) {
                    return new GeneralAsaphOperationResult<IEnumerable<RecordRevision<Model>>>() {
                        Status = OperationStatus.Success,
                        Message = $"Failed to Read Revisions [ {ex.Message} ]",
                        Payload = new List<RecordRevision<Model>>(),
                    };
                }
            });
        }
    }
}
