using ErrorOr;

namespace RentAMotto.Common.Api.Presentation;

/// <summary>
/// Contrato de retorno de dados 
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiBaseResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public List<Error>? Errors { get; set; }
}
