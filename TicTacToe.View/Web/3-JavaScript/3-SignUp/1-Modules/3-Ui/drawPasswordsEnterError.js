"use strict"


export function drawPasswordsEnterError(){
    const passwordFields = document.querySelectorAll(".form__password");

    const passwordInput = document.getElementById("password-entering");
    const confirmPasswordInput = document.getElementById("password-confirmation");

    passwordInput.value = "";
    confirmPasswordInput.value = "";

    passwordFields.forEach(field => field.style.border = "3px solid red");

    passwordInput.placeholder = "Passwords do not match!";
    confirmPasswordInput.placeholder = "Passwords do not match!";
}




export function clearPasswordError() {
    const passwordFields = document.querySelectorAll(".form__password");
    passwordFields.forEach(field => field.style.border = "3px solid white");

    document.getElementById("password-entering").placeholder = "Password";
    document.getElementById("password-confirmation").placeholder = "Password confirmation";
}