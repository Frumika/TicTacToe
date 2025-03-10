"use strict"

import {getOrCreateSessionId, getSessionId} from "../1-Core/sessionId.js";
import {URL} from "./url.js";


// Получение информации об игре
export async function getGameState() {

    const url = URL.GAME_CONTROLLER_URL

    const gameSessionId = getSessionId("gameSessionId");
    if (!gameSessionId) {
        throw new Error("The game session was not received");
    }

    const response = await fetch(`${url}/api/game/state`, {
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