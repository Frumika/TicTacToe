"use strict"


export function listenTogglePassword() {
    const button = document.getElementById("toggle-password");

    if (!button) {
        throw new Error("Toggle button is inactive");
    }

    button.addEventListener("click", () => {
        const toggleEvent = new CustomEvent("toggle");
        document.dispatchEvent(toggleEvent);
    });
}