using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Asaph.InterfaceLibrary.RecordRevisions;
using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class RevisionsReader : AsaphRevisionsReader {
        private readonly DatabaseServicesFactory databaseServicesFactory;

        public RevisionsReader(DatabaseServicesFactory databaseServicesFactory) {
            this.databaseServicesFactory = databaseServicesFactory;
        }

        public Task<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>> Revisions<Model>(Guid tenantId, string typeId, string referenceId) where Model : class {
            throw new NotImplementedException();
        }

        public Task<AsaphOperationResult<IEnumerable<RecordRevision<Model>>>> Revisions<Model>(Guid tenantId, string typeId, IEnumerable<string> referenceIds) where Model : class {
            throw new NotImplementedException();
        }
    }
}
