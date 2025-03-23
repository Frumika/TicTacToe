"use strict"

export function checkIsFormFillFull() {
    const login = document.getElementById("login").value;
    const password = document.getElementById("password").value;

    return login !== "" && password !== "";
}