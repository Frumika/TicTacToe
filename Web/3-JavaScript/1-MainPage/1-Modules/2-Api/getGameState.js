"use strict"

import { getSessionId} from "../../../0-Common/sessionId.js";
import {URL} from "../../../0-Common/url.js";


// Получение информации об игре
export async function getGameState() {

    const url = URL.GAME_CONTROLLER

    const gameSessionId = getSessionId("gameSessionId");
    if (!gameSessionId) {
        throw new Error("The game session was not received");
    }

    const response = await fetch(`${url}/state`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(gameSessionId)
    });

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message);
    }

    return await response.json();
}