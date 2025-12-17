namespace Backend.Application.Enums;

public enum UserStatusCode
{
    Success,
    InvalidLogin,
    InvalidPassword,
    IncorrectData,
    UserAlreadyExists,
    UserNotFound,
    UnknownError
}