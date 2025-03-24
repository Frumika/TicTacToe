"use strict"

export {listenAccountInfo, listenSignOutButton};


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