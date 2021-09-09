using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Asaph.Implementations.AppManagement.RecordRevisions.Models;
using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;
using Asaph.Shared.ExtensionMethods;
using Asaph.Implementations.Shared;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class RevisionsWriter : AsaphRevisionsWriter {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public RevisionsWriter(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public Task<AsaphOperationResult> Save<Model>(RecordRevision<Model> revision) where Model : class =>
            Save(revisions: new List<RecordRevision<Model>> { revision });

        public Task<AsaphOperationResult> Save<Model>(IEnumerable<RecordRevision<Model>> revisions) where Model : class {
            return Task.Run<AsaphOperationResult>(() => {
                try {
                    Task<AsaphDbQueryResult<RecordRevisionDbModel>> dbRequest =
                        databaseServicesFactory
                        .StoredProcedureCaller()
                        .QueryStoredProcedure<RecordRevisionDbModel>(
                            procedureName: "SP_SaveRevHistory",
                            connection: databaseServicesFactory.ConnectionProvider().AsaphDb(),
                            parameters: new {
                                Revisions = revisions.Select(revisionRecord => new RecordRevisionDbModel {
                                    Guid = revisionRecord.Guid,
                                    TenantId = revisionRecord.TenantId,
                                    Type = revisionRecord.Type,
                                    ReferenceId = revisionRecord.ReferenceId,
                                    UserId = revisionRecord.UserId,
                                    RevisionDateTime = revisionRecord.RevisionDateTime,
                                    EncodedData = RevisionDataEncoder.Encode(revisionRecord.RecordData),
                                }).ToDataTable(),
                            });
                    AsaphDbQueryResult<RecordRevisionDbModel> queryReadResult = dbRequest.Result;
                    if (queryReadResult.HasFailureStatus())
                        return new GeneralAsaphOperationResult() {
                            Status = queryReadResult.Status,
                            Message = $"Call to DB for Record Revisions failed [ {queryReadResult.Message} ]",
                        };
                    return new GeneralAsaphOperationResult() {
                        Status = OperationStatus.Success,
                        Message = $"Saved Revisions",
                    };
                } catch (Exception ex) {
                    return new GeneralAsaphOperationResult() {
                        Status = OperationStatus.Failure,
                        Message = $"Failed to Save Revisions [ {ex.Message} ]",
                    };
                }
            });
        }
    }
}
