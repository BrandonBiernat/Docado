using Docado.BusinessLogic.Interfaces;

namespace Docado.BusinessLogic.ReturnTypes;

public class ServiceResult : IServiceResult {
    public ServiceResult(
        object value, 
        bool isSuccess = true) {
        IsSuccess = isSuccess;
        Value = value;
    }
    public ServiceResult(bool isSuccess = true) {
        IsSuccess = isSuccess;
    }

    public ServiceResult(string errorMessage) {
        IsSuccess = false;
        Message = errorMessage;
    }

    public bool IsSuccess { get; } = false;
    public string Message { get; } = string.Empty;
    public object? Value { get; } = null;
}