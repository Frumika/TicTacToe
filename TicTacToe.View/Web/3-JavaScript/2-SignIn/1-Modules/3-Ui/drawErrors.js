"use strict"


export function drawSomethingWrong(key) {
    const container = document.querySelector(".sign_in-message");
    const image = document.querySelector(".sign_in-message__image");
    const text = document.querySelector(".sign_in-message__text");

    container.style.visibility = "visible";
    container.style.border = "4px solid red";

    image.src = "../../../Sources/Svg/error_circle.svg"

    text.style.color = "red";

    if (key === 401) {
        text.textContent = "The user does not exist";
    }
    else if(key === 500){
        text.textContent = "Something is broken on the server :(";
    }
    else{
        text.textContent = "Maybe you can try something else.";
    }
}