"use strict"

export function checkIsFormFillFull(){
    const login = document.getElementById("login").value;
    const passwordEntering = document.getElementById("password-entering").value;
    const passwordConfirmation = document.getElementById("password-confirmation").value;

    return login !== "" && passwordEntering !== "" && passwordConfirmation !== ""
}