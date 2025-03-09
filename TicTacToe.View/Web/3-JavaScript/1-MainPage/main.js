"use strict"

/*------------------> Import <------------------*/

// API: Отправка данных на сервер
import {sendMove} from "./1-Modules/2-Api/sendMove.js"; // Запрос о совершенном ходе
import {sendResetRequest} from "./1-Modules/2-Api/sendResetRequest.js"; // Запрос о перезагрузки игровой доски

// UI: Работа с визуальным представлением
import {updateGameState, UpdateMode} from "./1-Modules/3-Ui/updateGameInfo.js"; // Обновление доски с сервера
import {drawClearBoard} from "./1-Modules/3-Ui/drawClearBoard.js"; // Очистка доски
import {hiddenWinner} from "./1-Modules/3-Ui/drawWinnerMessage.js"; // Вывод сообщения о победителе

// EVENTS: Прослушивание элементов страницы
import {listenFields} from "./1-Modules/4-Events/listenFields.js"; // Полей игровой доски
import {listenResetButton} from "./1-Modules/4-Events/listenResetButton.js"; // Кнопки сброса



/*------------------> Scripts <------------------*/

/*
* Скрипт срабатывает при запуске страницы:
*
* Выполняет:
* 1. Прослушивает поля игровой доски;
* 2. Прослушивает кнопку Reset;
* 3. Обновляет все поля игровой доски;
*  */
document.addEventListener("DOMContentLoaded", async () => {
    listenFields();
    listenResetButton();

    try {
        await updateGameState(UpdateMode.UpdateAllFields);
    } catch (warn) {
        console.warn(warn.message)
    }
});


// Отслеживаем ивент move
document.addEventListener("move", async (event) => {
    try {
        const {row, column} = event.detail; // Получаем данные

        await sendMove(row, column); // Отправляем их на сервер
        await updateGameState(UpdateMode.MakeMove); // Обновляем игровые данные

    } catch (error) {
        console.error("Error during move or board update: ", error.message);
    }
});


// Отслеживаем нажатие кнопки Reset
document.addEventListener("reset", async () => {
    try {
        await sendResetRequest();
        drawClearBoard();
        hiddenWinner();
    } catch (error) {
        console.error(error.message);
    }
});
