"use strict"

export {listenAccountInfo, listenSignOutButton, listenModalWindow};


function listenAccountInfo() {
    const accountInfo = document.querySelector(".authorized__account-info");

    accountInfo.addEventListener("click", () => {
        const accountEvent = new CustomEvent("account-info");
        document.dispatchEvent(accountEvent);
    });
}


function listenSignOutButton() {
    const image = document.getElementById("sign-out-image");
    const text = document.getElementById("sign-out-text");

    const signOutEvent = new CustomEvent("sign-out");

    image.addEventListener("click", () => {
        document.dispatchEvent(signOutEvent);
    });

    text.addEventListener("click", () => {
        document.dispatchEvent(signOutEvent);
    });
}

function listenModalWindow() {
    const modalWindow = document.querySelector(".authorized__account-options");
    const accountInfo = document.querySelector(".authorized__account-info");

    document.addEventListener("click", (event) => {
        if (!modalWindow.contains(event.target) &&
            !accountInfo.contains(event.target) &&
            // modalWindow.style.visibility === "visible") {
            modalWindow.style.display === "flex") {

            const hideModalEvent = new CustomEvent("hide-modal");
            document.dispatchEvent(hideModalEvent);
        }
    });
}