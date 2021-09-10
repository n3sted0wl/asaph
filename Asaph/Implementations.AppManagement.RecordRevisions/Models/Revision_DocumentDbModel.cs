using System;

using Asaph.InterfaceLibrary.RecordRevisions;

using MongoDB.Bson.Serialization.Attributes;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Models {
    internal class Revision_DocumentDbModel<Model> where Model : class {
        public Revision_DocumentDbModel(RecordRevision<Model> revision) {
            Guid = revision.Guid;
            TenantId = revision.TenantId;
            Type = revision.Type;
            ReferenceId = revision.ReferenceId;
            UserId = revision.UserId;
            RevisionDateTime = revision.RevisionDateTime;
            RecordData = revision.RecordData;
        }

        [BsonId]
        public Guid Guid { get; set; }
        public Guid TenantId { get; set; }
        public string Type { get; set; }
        public string ReferenceId { get; set; }
        public string UserId { get; set; }
        public DateTime RevisionDateTime { get; set; }
        public Model RecordData { get; set; }
    }
}
