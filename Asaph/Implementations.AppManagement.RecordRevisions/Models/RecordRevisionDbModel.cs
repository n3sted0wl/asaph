using System;

namespace Asaph.Implementations.AppManagement.RecordRevisions.Models {
    public class RecordRevisionDbModel {
        public RecordRevisionDbModel() { }

        public Guid Guid { get; set; }
        public Guid TenantId { get; set; }

        public string Type { get; set; } = string.Empty;
        public string ReferenceId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public DateTime RevisionDateTime { get; set; }

        public string EncodedData { get; set; } = string.Empty;
    }
}
