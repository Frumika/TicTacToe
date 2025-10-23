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
        gameSessionId: gameSessionId,
        GameMode: GameSettings.GameMode,
        BotMode: GameSettings.BotMode
    }

    // Отправляем запрос на обновление доски на сервер
    const resetResponse = await fetch(`${url}/reset`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(requestData)
    });

    if (!resetResponse.ok) {
        const error = await resetResponse.json();
        throw new Error(error.message || "The board has not been updated");
    }
}
