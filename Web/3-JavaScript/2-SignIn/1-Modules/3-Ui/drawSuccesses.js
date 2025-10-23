"use strict"


export function drawWasAuthorized() {
    const container = document.querySelector(".sign_in-message");
    const image = document.querySelector(".sign_in-message__image");
    const text = document.querySelector(".sign_in-message__text");

    container.style.visibility = "visible";
    container.style.border = "4px solid green";

    image.src = "../../../4-Sources/Svg/check_circle.svg"

    text.style.color = "green";
    text.textContent = "You have successfully authorized!";
}
