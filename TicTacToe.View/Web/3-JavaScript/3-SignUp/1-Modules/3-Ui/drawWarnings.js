"use strict"

export function drawFieldsIsNotFill() {
    const container = document.querySelector(".sign_up-message");
    const image = document.querySelector(".sign_up-message__image");
    const text = document.querySelector(".sign_up-message__text");

    container.style.visibility = "visible";
    container.style.border = "4px solid yellow";

    image.src = "../../../Sources/Svg/warning_circle.svg"

    text.style.color = "yellow";
    text.textContent = "Not all fields were filled in";
}

export function hiddenFieldsIsNotFill() {
    const container = document.querySelector(".sign_up-message");
    container.style.visibility = "hidden";
}