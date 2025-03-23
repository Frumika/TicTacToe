"use strict"

import {URL} from "../../../0-Common/url.js";

export async function sendSignUpInfo() {
    const url = URL.SIGN_UP_CONTROLLER;

    const loginInput = document.getElementById("login").value;
    const passwordInput = document.getElementById("password-entering").value;

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            login: loginInput,
            password: passwordInput
        })
    };

    const response = await fetch(`${url}/registration`, requestData);

    if (!response.ok) {
        const error = await response.json();
        throw new Error(JSON.stringify(error.errors));
    } else {
        window.location.href = "/Web/1-HTML/index.html";
    }
}