namespace TicTacToe.Application.DTO.Requests.Base;

public interface IValidatableRequest
{
    ValidationResult Validate();
}