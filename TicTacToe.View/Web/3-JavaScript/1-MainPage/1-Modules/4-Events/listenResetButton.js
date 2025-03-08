"use strict"


export function listenResetButton() {
    const resetButton = document.querySelector(".board-container__game-board-button");

    if (!resetButton) {
        throw new Error("The reset button is inactive");
    }

    resetButton.addEventListener("click", async () => {
        const resetEvent = new CustomEvent("reset");
        document.dispatchEvent(resetEvent);
    });
}