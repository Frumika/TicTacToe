namespace Backend.Application.Enums;

public enum IdentityStatusCode
{
    Success,
    InvalidLogin,
    InvalidPassword,
    IncorrectData,
    UserAlreadyExists,
    UserNotFound,
    UnknownError
}