using System;
using Asaph.InterfaceLibrary.RecordRevisions;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class RevisionsBuilder : AsaphRevisionBuilder {
        public RecordRevision<Model> Build<Model>(
            Guid tenantId, 
            string userId, 
            string type, 
            string referenceId, 
            DateTime dateTime, 
            Model recordData) where Model : class =>
            new RevisionRecordForPersistence<Model> {
                Guid = Guid.NewGuid(),
                TenantId = tenantId,
                Type = type,
                ReferenceId = referenceId,
                UserId = userId,
                RevisionDateTime = dateTime,
                RecordData = recordData
            };
    }
}
