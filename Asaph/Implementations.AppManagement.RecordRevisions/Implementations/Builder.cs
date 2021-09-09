using System;
using Asaph.InterfaceLibrary.RecordRevisions;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class RevisionsBuilder : AsaphRevisionBuilder {
        public RecordRevision<Model> Build<Model>(Guid tenantId, string userId, string type, string referenceId, DateTime dateTime, Model model) where Model : class {
            throw new NotImplementedException();
        }
    }
}
