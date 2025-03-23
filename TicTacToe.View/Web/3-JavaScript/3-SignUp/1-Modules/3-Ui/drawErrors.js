"use strict"


export function drawSomethingWrong(key) {
    const container = document.querySelector(".sign_up-message");
    const image = document.querySelector(".sign_up-message__image");
    const text = document.querySelector(".sign_up-message__text");

    container.style.visibility = "visible";
    container.style.border = "4px solid red";

    image.src = "../../../Sources/Svg/error_circle.svg"

    text.style.color = "red";

    if (key === 409) {
        text.textContent = "User already exist";
    }
    else if(key === 500){
        text.textContent = "Something is broken on the server :(";
    }
    else{
        text.textContent = "Maybe you can try something else.";
    }
}


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