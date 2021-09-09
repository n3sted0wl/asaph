using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class RevisionsWriter : AsaphRevisionsWriter {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public RevisionsWriter(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public Task<AsaphOperationResult> Save<Model>(RecordRevision<Model> revision) where Model : class =>
            Save(revisions: new List<RecordRevision<Model>> { revision });

        public Task<AsaphOperationResult> Save<Model>(IEnumerable<RecordRevision<Model>> revisions) where Model : class {
            throw new NotImplementedException();
        }
    }
}
