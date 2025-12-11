"use strict"

import {URL} from "../../../0-Common/url.js";
import {addSessionId} from "../../../0-Common/sessionId.js";
import {GameSettings} from "../1-Core/gameSettings.js";
import {APP_PARAMS} from "../../../0-Common/Helpers/appParameters.js";


export async function sendStartRequest() {

    const url = URL.GAME_CONTROLLER;


    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            gameMode: GameSettings.GameMode,
            botMode: GameSettings.BotMode
        })
    }

    const response = await fetch(`${url}/start`, requestData);
    const result = await response.json();

    if (!result.isSuccess) {
        throw new Error(result.message);
    }

    const sessionId = result.data.sessionId;
    addSessionId("gameSessionId", sessionId, APP_PARAMS.GAME_SESSIONS_LIFE_TIME);

}