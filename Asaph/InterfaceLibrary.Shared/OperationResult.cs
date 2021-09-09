namespace Asaph.InterfaceLibrary.Shared {
    public enum OperationStatus {
        Success,
        Failure,
        BadRequest
    }

    public interface AsaphOperationResult {
        bool HasSuccessStatus();
        bool HasFailureStatus();

        OperationStatus Status { get; }
        string Message { get; }
    }

    public interface AsaphOperationResult<PayloadType> : AsaphOperationResult { 
        PayloadType Payload { get; }
    }
}
