"use strict"

import {URL} from "../../../0-Common/url.js";
import {getOrCreateSessionId} from "../../../0-Common/sessionId.js";
import {GameSettings} from "../1-Core/gameSettings.js";


export async function sendStartRequest() {

    const url = URL.GAME_CONTROLLER;

    const gameSessionId = getOrCreateSessionId("gameSessionId", 5);

    const gameInfoRequest = {
        gameSessionId: gameSessionId,
        gameMode: GameSettings.GameMode,
        botMode: GameSettings.BotMode
    }

    const response = await fetch(`${url}/start`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(gameInfoRequest)
    });

    if (!response.ok) {
        const errorMessage = await response.json();
        throw new Error(`The game has not been started. Reason - ${errorMessage}`);
    }
}