"use strict"

import {URL} from "../../../0-Common/url.js";
import {getSessionId} from "../../../0-Common/sessionId.js";
import {GameSettings} from "../1-Core/gameSettings.js";
import {getUserSessionId} from "../../../0-Common/getUserSessionId.js";


export async function sendResetRequest() {

    let userId = null;

    let response = undefined;
    let result = undefined;

    const gameControllerUrl = URL.GAME_CONTROLLER;
    const userControllerUrl = URL.IDENTITY_MANAGEMENT_CONTROLLER;


    const userSessionId = getUserSessionId();
    if (userSessionId !== null && userSessionId !== "") {
        const getUserDataRequest = {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({
                sessionId: userSessionId
            })
        };

        response = await fetch(`${userControllerUrl}/session_info`, getUserDataRequest);
        result = await response.json();

        if (result.isSuccess) userId = result.data.id;
    }


    const gameSessionId = getSessionId("gameSessionId");
    if (!gameSessionId) {
        throw new Error("Game session not found");
    }

    const requestData = {
        method: "PUT",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            userId: userId,
            sessionId: gameSessionId,
            gameMode: GameSettings.GameMode,
            botMode: GameSettings.BotMode
        })
    };


    response = await fetch(`${gameControllerUrl}/reset`, requestData);
    result = await response.json();

    if (!result.isSuccess) {
        throw new Error(result.message);
    }
}
