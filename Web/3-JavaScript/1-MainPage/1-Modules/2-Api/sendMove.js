"use strict"

import {getSessionId} from "../../../0-Common/sessionId.js";
import {URL} from "../../../0-Common/url.js";
import {sendStartRequest} from "./sendStartRequest.js";
import {checkGameSession} from "./checkGameSession.js";

export async function sendMove(row, column) {
    // Определяем URL контроллера
    let url = URL.GAME_CONTROLLER;

    // Находим id нужной нам игровой сессии
    let gameSessionId = getSessionId("gameSessionId");

    if (!gameSessionId) { // Если Сессии с Id не существует, то
        try {
            await sendStartRequest(); // Создаем сессию и отправляем запрос на создание сессии на сервере
        } catch (error) {
            throw new Error(error.message);
        }
        gameSessionId = getSessionId("gameSessionId"); // Получаем id этой сессии
    } else { // Если Сессия уже существует, то
        const success = await checkGameSession(gameSessionId); // Проверяем её на сервере
        if (!success) await sendStartRequest(); // И создаем новую, если её нет
    }


    // По нужному адресу отправляем запрос и ждём ответ от сервера
    const response = await fetch(`${url}/move`, {
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