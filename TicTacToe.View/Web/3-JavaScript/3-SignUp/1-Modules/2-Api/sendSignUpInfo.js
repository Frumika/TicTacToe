"use strict"

import {URL} from "../../../0-Common/url.js";
import {drawAllSuccessful} from "../3-Ui/drawAllSuccessful.js";
import {drawSomethingWrong} from "../3-Ui/drawSomethingWrong.js";

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
        drawSomethingWrong(response.status);
        const error = await response.json();
        throw new Error(JSON.stringify(error.errors));
    } else {
        drawAllSuccessful();
        setTimeout(() => {
            window.location.href = "/Web/1-HTML/index.html"; // Перенаправление через 1.5 сек
        }, 2000);
    }
}