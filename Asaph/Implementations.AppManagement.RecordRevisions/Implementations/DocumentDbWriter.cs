using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Asaph.Implementations.AppManagement.RecordRevisions.Models;
using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

using Implementations.AppManagement.RecordRevisions;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class DocumentDbWriter : AsaphRevisionsWriter {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public DocumentDbWriter(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public Task<AsaphOperationResult> Save<Model>(RecordRevision<Model> revision) where Model : class =>
            Save(revisions: new List<RecordRevision<Model>> { revision });

        public Task<AsaphOperationResult> Save<Model>(IEnumerable<RecordRevision<Model>> revisions) where Model : class {
            List<Revision_DocumentDbModel<Model>> revisionDocuments = 
                revisions
                .Select(revision => new Revision_DocumentDbModel<Model>(revision))
                .ToList();
            Task<AsaphOperationResult> insertRequest =
                databaseServicesFactory
                .DocDbCaller()
                .Insert(
                    dbName: DocumentDbCollectionInfo.DATABASE_NAME,
                    tableName: DocumentDbCollectionInfo.TABLE_NAME,
                    documents: revisionDocuments);
            return insertRequest;
        }
    }
}
