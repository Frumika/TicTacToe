"use strict"


export function isPasswordsEquals() {
    const passEntering = document.getElementById("password-entering");
    const passConfirm = document.getElementById("password-confirmation");
    return passEntering.value === passConfirm.value;
}