using System;

using Asaph.Implementations.AppManagement.RecordRevisions.Models;
using Asaph.InterfaceLibrary.RecordRevisions;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Implementations {
    internal class RevisionRecordForPersistence<Model> : RecordRevision<Model> where Model : class {
        public RevisionRecordForPersistence() { }
        public RevisionRecordForPersistence(RecordRevisionDbModel dbModel) {
            Guid = dbModel.Guid;
            TenantId = dbModel.TenantId;
            Type = dbModel.Type;
            ReferenceId = dbModel.ReferenceId;
            UserId = dbModel.UserId;
            RevisionDateTime = dbModel.RevisionDateTime;
            RecordData = RevisionDataEncoder.Decode<Model>(dbModel.EncodedData);
        }

        public RevisionRecordForPersistence(Revision_DocumentDbModel<Model> document) {
            Guid = document.Guid;
            TenantId = document.TenantId;
            Type = document.Type;
            ReferenceId = document.ReferenceId;
            UserId = document.UserId;
            RevisionDateTime = document.RevisionDateTime;
            RecordData = document.RecordData;
        }

        public Guid Guid { get; set; }
        public Guid TenantId { get; set; }

        public string Type { get; set; }
        public string ReferenceId { get; set; }
        public string UserId { get; set; }

        public DateTime RevisionDateTime { get; set; }
        public Model RecordData { get; set; }
    }
}
