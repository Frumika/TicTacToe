"use strict"

import {getSessionId} from "../../../0-Common/sessionId.js";
import {URL} from "../../../0-Common/url.js";


// Получение информации об игре
export async function getGameState() {

    const url = URL.GAME_CONTROLLER

    const gameSessionId = getSessionId("gameSessionId");
    if (!gameSessionId) {
        throw new Error("The game session was not received");
    }

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            sessionId: gameSessionId
        })
    };

    const response = await fetch(`${url}/state`, requestData);
    const result = await response.json();

    if (!result.isSuccess) {
        throw new Error(result.message);
    }

    console.log(result);
    console.log(result.data);

    return result.data;
}