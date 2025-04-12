"use strict"


export function drawAllSuccessful() {
    const container = document.querySelector(".sign_up-message");
    const image = document.querySelector(".sign_up-message__image");
    const text = document.querySelector(".sign_up-message__text");

    container.style.visibility = "visible";
    container.style.border = "4px solid green";

    image.src = "../../../4-Sources/Svg/check_circle.svg"

    text.style.color = "green";
    text.textContent = "You have successfully registered!";
}