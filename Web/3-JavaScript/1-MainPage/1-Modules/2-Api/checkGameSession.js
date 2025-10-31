"use strict"

import {URL} from "../../../0-Common/url.js";

export async function checkGameSession(gameSessionId) {
    const url = URL.GAME_CONTROLLER;

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            sessionId: gameSessionId
        })
    };

    const response = await fetch(`${url}/check`, requestData);
    const result = await response.json();

    return result.isSuccess;
}