"use strict"

import {URL} from "./url.js";
import {getSessionId} from "../1-Core/sessionId.js";


export async function sendResetRequest() {
    // URL игрового контроллера
    const url = URL.GAME_CONTROLLER_URL;

    // Id игровой сессии
    const gameSessionId = getSessionId("gameSessionId");
    if (!gameSessionId) {
        throw new Error("Game session not found");
    }

    // Данные, которые будут отправлены на сервер в запросе
    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(gameSessionId)
    }

    // Отправляем запрос на обновление доски на сервер
    const resetResponse = await fetch(`${url}/api/game/reset`, requestData);
    if (!resetResponse.ok) {
        const error = await resetResponse.json();
        throw new Error(error.message || "The board has not been updated");
    }
}
