"use strict"


import {IdentityStatus} from "../../../0-Common/Enums/IdentityStatus.js";

export function drawSomethingWrong(key) {
    const container = document.querySelector(".sign_in-message");
    const image = document.querySelector(".sign_in-message__image");
    const text = document.querySelector(".sign_in-message__text");

    container.style.visibility = "visible";
    container.style.border = "4px solid red";

    image.src = "../../../4-Sources/Svg/error_circle.svg"

    text.style.color = "red";

    if (key === IdentityStatus.IncorrectData) {
        text.textContent = "The user does not exist";
    }
    else if(key === IdentityStatus.UnknownError){
        text.textContent = "Something is broken on the server :(";
    }
    else{
        text.textContent = "Maybe you can try something else.";
    }
}