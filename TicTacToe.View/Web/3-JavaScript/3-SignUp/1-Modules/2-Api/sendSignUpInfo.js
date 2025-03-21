"use strict"

import {URL} from "../../../0-Common/url.js";
import {getSessionId} from "../../../0-Common/sessionId.js";


export async function sendSignUpInfo() {
    const url = URL.SIGN_UP_CONTROLLER;

    const playerSessionId = getSessionId("playerSessionId");

    /* Добавить начало обработки сессий !!! */
    const loginInput = document.getElementById("login");
    const passwordInput = document.getElementById("password-entering");

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({playerSessionId, loginInput, passwordInput})
    };

    const response = await fetch(`${url}/register`, requestData);

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message);
    }
}