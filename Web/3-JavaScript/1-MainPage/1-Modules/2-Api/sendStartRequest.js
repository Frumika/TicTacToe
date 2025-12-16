"use strict"

import {URL} from "../../../0-Common/url.js";
import {addSessionId} from "../../../0-Common/sessionId.js";
import {GameSettings} from "../1-Core/gameSettings.js";
import {APP_PARAMS} from "../../../0-Common/Helpers/appParameters.js";
import {getUserSessionId} from "../../../0-Common/getUserSessionId.js";


export async function sendStartRequest() {

    let userId = null;

    let response = undefined;
    let result = undefined;


    const gameUrl = URL.GAME_CONTROLLER;
    const userUrl = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const userSessionId = getUserSessionId();

    if (userSessionId !== null && userSessionId !== "") {
        const getUserDataRequest = {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({
                sessionId: userSessionId
            })
        };

        response = await fetch(`${userUrl}/session_info`, getUserDataRequest);
        result = await response.json();

        if (result.isSuccess) userId = result.data.id;
    }

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            userId: userId,
            gameMode: GameSettings.GameMode,
            botMode: GameSettings.BotMode
        })
    };

    response = await fetch(`${gameUrl}/start`, requestData);
    result = await response.json();

    if (!result.isSuccess) {
        throw new Error(result.message);
    }

    const sessionId = result.data.sessionId;
    addSessionId("gameSessionId", sessionId, APP_PARAMS.GAME_SESSIONS_LIFE_TIME);

}