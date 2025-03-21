"use strict"

import {URL} from "../../../0-Common/url.js";
import {getSessionId} from "../../../0-Common/sessionId.js";


export async function sendSignUpInfo() {
    const url = URL.SIGN_UP_CONTROLLER;

    let playerSessionId = "1234";

    try {
        playerSessionId = getSessionId("playerSessionId");
    } catch (error) {
        throw new Error(`Player session Id ERROR: ${error.message}`);
    }

    /* Добавить начало обработки сессий !!! */
    const loginInput = document.getElementById("login").value;
    const passwordInput = document.getElementById("password-entering").value;

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({playerSessionId, loginInput, passwordInput})
    };

    console.log(`request: ${playerSessionId}, ${loginInput}, ${passwordInput}`);

    const response = await fetch(`${url}/register`, requestData);

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message);
    }
}