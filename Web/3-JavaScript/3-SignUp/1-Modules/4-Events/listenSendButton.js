"use strict"


export function listenSendButton() {
    const sendButton = document.querySelector(".form__submit-button");

    if (!sendButton) {
        throw new Error("Send Button is inactive");
    }

    sendButton.addEventListener("click", () => {
        const sendEvent = new CustomEvent("send");
        document.dispatchEvent(sendEvent);
    });
}