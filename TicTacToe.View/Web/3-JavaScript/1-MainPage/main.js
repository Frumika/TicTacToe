"use strict"

/*------------------> Import <------------------*/

// API: Отправка данных на сервер
import {sendMove} from "./1-Modules/2-Api/sendMove.js"; // Запрос о совершенном ходе
import {sendResetRequest} from "./1-Modules/2-Api/sendResetRequest.js"; // Запрос о перезагрузки игровой доски
import {sendSignOutRequest} from "./1-Modules/2-Api/sendSignOutRequest.js";

// UI: Работа с визуальным представлением
import {updateGameState, UpdateMode} from "./1-Modules/3-Ui/updateGameInfo.js"; // Обновление доски с сервера
import {drawClearBoard} from "./1-Modules/3-Ui/drawClearBoard.js"; // Очистка доски
import {hiddenWinnerMessage} from "./1-Modules/3-Ui/drawWinnerMessage.js"; // Вывод сообщения о победителе
import {setBaseGameMode, setBaseBotMode, GameSettings} from "./1-Modules/1-Core/gameSettings.js";
import {drawGameMode, drawBotMode} from "./1-Modules/3-Ui/gameSettings.js";
import {
    hideAccountOptions,
    hideUserInfo,
    showAccountOptions,
    showUserInfo
} from "./1-Modules/3-Ui/drawAuthorization.js";

// EVENTS: Прослушивание элементов страницы
import {listenFields} from "./1-Modules/4-Events/listenFields.js"; // Полей игровой доски
import {listenResetButton} from "./1-Modules/4-Events/listenResetButton.js"; // Кнопки сброса
import {listenGameModeSelection, listenBotModeSelection} from "./1-Modules/4-Events/listenGameSettings.js"; // Выбор игрового режима
import {
    listenAccountInfo,
    listenModalWindow,
    listenSignOutButton
} from "./1-Modules/4-Events/listenIAuthorizeButtons.js";

// Всё то, что... то
import {checkUserSessionId} from "./1-Modules/1-Core/checkUserSession.js";


document.addEventListener("DOMContentLoaded", async () => {
    listenFields();
    listenResetButton();

    setBaseGameMode();
    listenGameModeSelection();

    setBaseBotMode();
    listenBotModeSelection();

    checkUserSessionId();

    listenAccountInfo();
    listenSignOutButton();

    listenModalWindow();

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
        hiddenWinnerMessage();
    } catch (error) {
        console.error(error.message);
    }
});


// Отслеживания выбора режима игры
document.addEventListener("mode-select", (event) => {
    const mode = event.detail.mode;
    GameSettings.GameMode = mode;
    drawGameMode(mode);
});


// Отслеживание выбора сложности бота
document.addEventListener("bot-select", (event) => {
    const mode = event.detail.mode;
    GameSettings.BotMode = mode;
    drawBotMode(mode);
});


// Отображение данных пользователя после авторизации
document.addEventListener("authorized-show", () => {
    showUserInfo();
});

// Сокрытие данные пользователя если тот не авторизован
document.addEventListener("authorized-hide", () => {
    hideUserInfo();
});

// Обработка показа опций пользователя
document.addEventListener("account-info", () => {
    showAccountOptions();
});


// Обработка выхода из сессии
document.addEventListener("sign-out", async () => {
    try {
        await sendSignOutRequest();
    } catch (error) {
        console.error(`Error while deleting user: ${error.message}`);
    }
});


// Сокрытие модального окна
document.addEventListener("hide-modal", () => {
    hideAccountOptions();
});
