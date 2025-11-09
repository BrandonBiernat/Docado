using System.Net;

namespace Docado.BusinessLogic.Interfaces;

public interface IServiceResult { 
    bool IsSuccess { get; }
    string Message { get; }
    object Value { get; }
}