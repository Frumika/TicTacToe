"use strict"

import {getOrCreateSessionId} from "../1-Core/sessionId.js";
import {URL} from "./url.js";

export async function sendMove(row, column) {
    // Определяем URL контроллера
    let url = URL.GAME_CONTROLLER_URL;

    // Находим id нужной нам игровой сессии
    let gameSessionId = getOrCreateSessionId("gameSessionId");
    if (!gameSessionId) {
        throw new Error("The game session was not received")
    }

    // По нужному адресу отправляем запрос и ждём ответ от сервера
    const response = await fetch(`${url}/api/game/move`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({gameSessionId, row, column})
    });

    // Если что-то пошло не так - выбрасываем ошибку
    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message);
    }
}