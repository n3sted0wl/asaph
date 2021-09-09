using System.Collections.Generic;

using Asaph.InterfaceLibrary.ServiceCallers.Databases;
using Asaph.InterfaceLibrary.Shared;

namespace Asaph.Implementations.ServiceCallers.Database.Implementations {
    internal class AsaphDbQueryResultForDapper<Model> : AsaphDbQueryResult<Model> {
        public AsaphDbQueryResultForDapper(
            OperationStatus status, 
            string message, 
            IEnumerable<Model> records = null) {
            Status = status;
            Message = message;
            if (records != null)
                Records = records;
        }

        public IEnumerable<Model> Records { get; set; } = new List<Model>();
        public bool HasSuccessStatus() => Status == OperationStatus.Success;
        public bool HasFailureStatus() => !HasSuccessStatus();
        public OperationStatus Status { get; set; } = OperationStatus.Failure;
        public string Message { get; set; } = string.Empty;
    }
}
