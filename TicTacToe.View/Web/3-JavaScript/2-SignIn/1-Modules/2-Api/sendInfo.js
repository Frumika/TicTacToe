"use strict"

import {URL} from "../../../0-Common/url.js";
import {drawWasAuthorized} from "../3-Ui/drawSuccesses.js";
import {drawSomethingWrong} from "../3-Ui/drawErrors.js";

export async function sendInfo() {
    const url = URL.SIGN_IN_CONTROLLER;

    const login = document.getElementById("login").value;
    const password = document.getElementById("password").value;

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            login: login,
            password: password
        })
    };

    const response = await fetch(`${url}/authorization`, requestData);

    if (!response.ok) {
        drawSomethingWrong(response.status);
        const error = await response.json();
        throw new Error(JSON.stringify(error.errors));
    } else {
        drawWasAuthorized();
        setTimeout(() => {
            window.location.href = "/Web/1-HTML/index.html";
        }, 2000);
    }
}