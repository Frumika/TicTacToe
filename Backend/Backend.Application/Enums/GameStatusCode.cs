namespace Backend.Application.Enums;

public enum GameStatusCode
{
    Success,
    IncorrectData,
    SessionNotFound,
    SessionAlreadyExists,
    InvalidMove,
    SessionResetFailed,
    UnknownError
}