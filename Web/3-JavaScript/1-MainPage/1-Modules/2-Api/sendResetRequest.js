"use strict"

import {URL} from "../../../0-Common/url.js";
import {getSessionId} from "../../../0-Common/sessionId.js";
import {GameSettings} from "../1-Core/gameSettings.js";


export async function sendResetRequest() {
    // URL игрового контроллера
    const url = URL.GAME_CONTROLLER;

    // Id игровой сессии
    const gameSessionId = getSessionId("gameSessionId");
    if (!gameSessionId) {
        throw new Error("Game session not found");
    }

    // Данные, которые будут отправлены на сервер в запросе
    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            sessionId: gameSessionId,
            gameMode: GameSettings.GameMode,
            botMode: GameSettings.BotMode
        })
    };

    // Отправляем запрос на обновление доски на сервер
    const resetResponse = await fetch(`${url}/reset`, requestData);
    const result = await resetResponse.json();

    if (!result.isSuccess) {
        throw new Error(result.message);
    }
}
