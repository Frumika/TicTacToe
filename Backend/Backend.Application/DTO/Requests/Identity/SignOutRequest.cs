﻿using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Identity;

public class SignOutRequest : IValidatableRequest
{
    public string SessionId { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        string? message = null;
        bool isSessionIdValid = !string.IsNullOrWhiteSpace(SessionId);
        
        if (!isSessionIdValid) message += "Session id is required";
        
        return isSessionIdValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}