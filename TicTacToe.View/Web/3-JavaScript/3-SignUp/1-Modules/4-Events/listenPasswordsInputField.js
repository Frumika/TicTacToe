"use strict"

export function listenPasswordInputField(){
    const passwordInput = document.getElementById("password-entering");
    const confirmPasswordInput = document.getElementById("password-confirmation");

    const focusEvent = new CustomEvent("password-click");

    passwordInput.addEventListener("click", () =>{
        document.dispatchEvent(focusEvent);
    });

    confirmPasswordInput.addEventListener("click", () => {
        document.dispatchEvent(focusEvent);
    });
}