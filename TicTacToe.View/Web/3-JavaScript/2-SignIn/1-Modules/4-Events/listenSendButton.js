"use strict"


export function listenSendButton() {
    const button = document.getElementById("send-button");

    if (!button) {
        throw new Error("Send button is inactive");
    }

    button.addEventListener("click", () => {
        const sendEvent = new CustomEvent("send");
        document.dispatchEvent(sendEvent);
    });
}