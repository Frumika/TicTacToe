"use strict"

export {showUserInfo, hideUserInfo, showAccountOptions}

function showUserInfo(){
    const container = document.querySelector(".aside-container__authorized");
    const buttons = document.querySelectorAll(".authorization-button");

    buttons.forEach(button =>{
        button.style.visibility = "hidden";
    });

    container.style.visibility = "visible";
}

function hideUserInfo(){
    const container = document.querySelector(".aside-container__authorized");
    const buttons = document.querySelectorAll(".authorization-button");

    buttons.forEach(button =>{
        button.style.visibility = "visible";
    });

    container.style.visibility = "hidden";
}

function showAccountOptions(){
    const container = document.querySelector(".authorized__account-options");

    let visibility = container.style.visibility;

    container.style.visibility = visibility === "visible" ? "hidden" : "visible";
}