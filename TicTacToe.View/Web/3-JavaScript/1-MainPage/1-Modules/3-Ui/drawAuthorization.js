"use strict"

export {showUserInfo, hideUserInfo, switchAccountOptions, hideAccountOptions}

function showUserInfo(userLogin) {
    const container = document.querySelector(".aside-container__authorized");
    const buttons = document.querySelectorAll(".authorization-button");
    const login = document.querySelector(".account-info__login");

    buttons.forEach(button => {
        button.style.visibility = "hidden";
    });

    login.textContent = userLogin;

    container.style.visibility = "visible";
}

function hideUserInfo() {
    const container = document.querySelector(".aside-container__authorized");
    const buttons = document.querySelectorAll(".authorization-button");

    buttons.forEach(button => {
        button.style.visibility = "visible";
    });

    container.style.visibility = "hidden";
}

function switchAccountOptions() {
    const container = document.querySelector(".authorized__account-options");

    let visibility = container.style.visibility;

    container.style.visibility = visibility === "visible" ? "hidden" : "visible";
}


function hideAccountOptions() {
    const container = document.querySelector(".authorized__account-options");

    container.style.visibility = "hidden";
}