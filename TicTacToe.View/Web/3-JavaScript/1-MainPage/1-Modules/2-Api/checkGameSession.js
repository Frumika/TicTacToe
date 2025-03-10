"use strict"

import {URL} from "./url.js";

export async function checkGameSession(gameSessionId) {
    const url = URL.GAME_CONTROLLER_URL;

    const response = await fetch(`${url}/api/game/check`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(gameSessionId)
    })

    return response.ok;
}