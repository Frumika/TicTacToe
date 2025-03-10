"use strict"

export let GameMode = {
    PvP: "PvP",
    PvE: "PvE"
}

export let BotMode = {
    Easy: "Easy",
    Medium: "Medium",
    Hard: "Hard"
}

export function listenGameModeSelection() {
    const modeElements = document.querySelectorAll(".params__game-mode .variants__elem");

    modeElements.forEach((elem) => {

        const circle = elem.querySelector(".elem__circle");
        const text = elem.querySelector(".elem__text");

        const gameMode = text.textContent === "PvP" ? GameMode.PvP : GameMode.PvE;

        const modeSelectionEvent = () => {
            const event = new CustomEvent("mode-select", {detail: {mode: gameMode}});
            document.dispatchEvent(event);
        };

        circle.addEventListener("click", modeSelectionEvent);

        text.addEventListener("click", modeSelectionEvent);
    });
}

export function listenBotModeSelection() {
    const modeElements = document.querySelectorAll(".params__bot-mode .variants__elem")

    modeElements.forEach((elem) => {

        const circle = elem.querySelector(".elem__circle");
        const text = elem.querySelector(".elem__text");

        let botMode;
        if (text.textContent === "Easy") {
            botMode = BotMode.Easy;
        } else if (text.textContent === "Medium") {
            botMode = BotMode.Medium;
        } else {
            botMode = BotMode.Hard
        }

        const modeSelectionEvent = () => {
            const event = new CustomEvent("bot-select", {detail: {mode: botMode}});
            document.dispatchEvent(event);
        };

        circle.addEventListener("click", modeSelectionEvent);
        text.addEventListener("click", modeSelectionEvent);
    });
}