namespace InterfaceLibrary.Shared {
    public enum OperationStatus {
        Success,
        Failure,
        BadRequest
    }

    public interface AsaphOperationResult {
        bool HasSuccessStatus { get; }
        bool HasFailureStatus { get; }

        OperationStatus Status { get; }
        string Message { get; }
    }

    public interface AsaphOperationResult<PayloadType> : AsaphOperationResult { 
        PayloadType Payload { get; }
    }
}
