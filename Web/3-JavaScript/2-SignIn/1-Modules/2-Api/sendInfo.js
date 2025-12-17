"use strict"

import {URL} from "../../../0-Common/url.js";
import {addSessionId} from "../../../0-Common/sessionId.js";
import {drawWasAuthorized} from "../3-Ui/drawSuccesses.js";
import {drawSomethingWrong} from "../3-Ui/drawErrors.js";
import {IdentityStatusCodeHelper} from "../../../0-Common/Helpers/IdentityStatusCodeHelper.js";
import {APP_PARAMS} from "../../../0-Common/Helpers/appParameters.js";


export async function sendInfo() {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

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

    const response = await fetch(`${url}/signin`, requestData);
    const result = await response.json();

    console.log(result);
    console.log(result.data.sessionId);

    if (!result.isSuccess) {
        drawSomethingWrong(IdentityStatusCodeHelper.getCode(result));
        throw new Error(result.message);
    } else {
        const userSessionId = JSON.stringify(result.data.sessionId);
        addSessionId("userSessionId", userSessionId, APP_PARAMS.USER_SESSIONS_LIFE_TIME);

        drawWasAuthorized();
        setTimeout(() => {
            window.location.href = "/Web/1-HTML/index.html";
        }, 2000);
    }
}