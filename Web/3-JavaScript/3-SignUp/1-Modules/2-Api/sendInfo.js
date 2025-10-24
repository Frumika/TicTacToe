"use strict"

import {URL} from "../../../0-Common/url.js";
import {drawAllSuccessful} from "../3-Ui/drawSuccesses.js";
import {drawSomethingWrong} from "../3-Ui/drawErrors.js";
import {IdentityStatusHelper} from "../../../0-Common/Helpers/IdentityStatusHelper.js";

export async function sendInfo() {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

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

    const response = await fetch(`${url}/signup`, requestData);
    const result = await response.json();

    if (!result.isSuccess) {
        drawSomethingWrong(IdentityStatusHelper.getCode(result));
        throw new Error(result.message);
    } else {
        drawAllSuccessful();
        setTimeout(() => {
            window.location.href = "/Web/1-HTML/index.html";
        }, 2000);
    }
}