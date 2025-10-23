"use strict"


export function listenTogglePassword() {
    const toggleButtons = document.querySelectorAll(".toggle-password img");

    if (!toggleButtons) {
        throw new Error("The toggle password buttons is inactive");
    }

    toggleButtons.forEach(button => {
        button.addEventListener("click", () => {
            const toggleEvent = new CustomEvent("toggle");
            document.dispatchEvent(toggleEvent);
        });
    });
}