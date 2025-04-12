"use strict"

export function drawFieldsIsNotFill() {
    const container = document.querySelector(".sign_in-message");
    const image = document.querySelector(".sign_in-message__image");
    const text = document.querySelector(".sign_in-message__text");

    container.style.visibility = "visible";
    container.style.border = "4px solid yellow";

    image.src = "../../../4-Sources/Svg/warning_circle.svg"

    text.style.color = "yellow";
    text.textContent = "Not all fields were filled in";
}

export function hiddenFieldsIsNotFill() {
    const container = document.querySelector(".sign_in-message");
    container.style.visibility = "hidden";
}