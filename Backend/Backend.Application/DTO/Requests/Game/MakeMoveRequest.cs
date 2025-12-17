using Backend.Application.DTO.Requests.Base;

namespace Backend.Application.DTO.Requests.Game;

public class MakeMoveRequest : IValidatableRequest
{
    public string SessionId { get; set; } = string.Empty;
    public int Row { get; set; }
    public int Column { get; set; }

    public ValidationResult Validate()
    {
        string? message = null;

        bool isSessionIdValid = !string.IsNullOrWhiteSpace(SessionId);
        bool isRowValueValid = Row >= 0;
        bool isColumnValueValid = Column >= 0;

        bool isValid = isSessionIdValid && isRowValueValid && isColumnValueValid;

        if (!isSessionIdValid) message += "Session Id is required \n";
        if (!isRowValueValid) message += "Row value must be in the range from 0 to 2_147_483_647 \n";
        if (!isColumnValueValid) message += "Column value must be in the range from 0 to 2_147_483_647";

        return isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}