"use strict"


import {isPasswordsEquals} from "./1-Core/checkPasswords.js";

import {sendSignUpInfo} from "./2-Api/sendSignUpInfo.js";

import {togglePassword} from "./3-Ui/togglePassword.js";
import {clearPasswordError, drawPasswordsEnterError} from "./3-Ui/drawPasswordsEnterError.js";

import {listenSignUpSendButton} from "./4-Events/listenSignUpSendButton.js";
import {listenTogglePassword} from "./4-Events/listenTogglePasswordButton.js";
import {listenPasswordInputField} from "./4-Events/listenPasswordsInputField.js";


// Прослушивание загрузки страницы
document.addEventListener("DOMContentLoaded", async () => {
    listenSignUpSendButton();
    listenTogglePassword();
    listenPasswordInputField();
});


// Прослушивание кнопки отправки на сервер
document.addEventListener("send", async () => {
    if (isPasswordsEquals()) {
        try {
            await sendSignUpInfo();

        } catch (error) {
            console.error(`SignUp ERROR: ${error.message}`);
        }
    } else {
        drawPasswordsEnterError();
    }
});


// Прослушивание кнопки переключения показа пароля
document.addEventListener("toggle", () => {togglePassword();});

document.addEventListener("custom-focus", () => {clearPasswordError();})



