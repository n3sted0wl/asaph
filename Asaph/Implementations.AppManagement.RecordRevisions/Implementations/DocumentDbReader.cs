using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Asaph.Implementations.AppManagement.RecordRevisions.Models;
using Asaph.Implementations.Shared;
using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

using Implementations.AppManagement.RecordRevisions;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class DocumentDbReader : AsaphRevisionsReader {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public DocumentDbReader(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public Task<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>> Revisions<Model>(
            Guid tenantId, string typeId, string referenceId) where Model : class =>
            Revisions<Model>(tenantId: tenantId, typeId: typeId, referenceIds: new List<string> { referenceId });

        public Task<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>> Revisions<Model>(
            Guid tenantId, string typeId, IEnumerable<string> referenceIds) where Model : class =>
            Task.Run<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>>(() => {
                Task<AsaphStorageReadResult<Revision_DocumentDbModel<Model>>> readRequest =
                    databaseServicesFactory
                    .DocDbCaller()
                    .Read<Revision_DocumentDbModel<Model>>(
                        dbName: DocumentDbCollectionInfo.DATABASE_NAME,
                        tableName: DocumentDbCollectionInfo.TABLE_NAME);
                AsaphStorageReadResult<Revision_DocumentDbModel<Model>> readResult = readRequest.Result;
                if (readResult.HasFailureStatus())
                    return new GeneralAsaphOperationResult<IEnumerable<RecordRevision<Model>>> {
                        Status = readResult.Status,
                        Message = $"Failed to read revision documents [ {readResult.Message} ]",
                        Payload = new List<RecordRevision<Model>>()
                    };
                List<RevisionRecordForPersistence<Model>> revisionRecords =
                    readResult
                    .Records
                    .Select(document => new RevisionRecordForPersistence<Model>(document))
                    .ToList();
                return new GeneralAsaphOperationResult<IEnumerable<RecordRevision<Model>>> {
                    Status = OperationStatus.Success,
                    Message = $"Read revision documents",
                    Payload = revisionRecords
                };
            });
    }
}
