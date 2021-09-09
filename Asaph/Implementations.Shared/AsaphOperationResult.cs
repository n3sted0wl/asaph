using Asaph.InterfaceLibrary.Shared;

namespace Asaph.Implementations.Shared {
    public class GeneralAsaphOperationResult : AsaphOperationResult {
        public bool HasSuccessStatus() => Status == OperationStatus.Success;
        public bool HasFailureStatus() => !HasSuccessStatus();
        public OperationStatus Status { get; set; } = OperationStatus.Failure;
        public string Message { get; set; } = string.Empty;
    }

    public class GeneralAsaphOperationResult<Model> : AsaphOperationResult<Model> {
        public bool HasSuccessStatus() => Status == OperationStatus.Success;
        public bool HasFailureStatus() => !HasSuccessStatus();
        public OperationStatus Status { get; set; } = OperationStatus.Failure;
        public string Message { get; set; } = string.Empty;
        public Model Payload { get; set; }
    }
}
