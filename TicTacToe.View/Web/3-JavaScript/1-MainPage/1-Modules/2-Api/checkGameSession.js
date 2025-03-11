"use strict"

import {URL} from "../../../0-Common/url.js";

export async function checkGameSession(gameSessionId) {
    const url = URL.GAME_CONTROLLER;

    const response = await fetch(`${url}/check`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(gameSessionId)
    })

    return response.ok;
}