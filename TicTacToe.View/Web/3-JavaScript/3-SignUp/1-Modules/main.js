"use strict"


import {isPasswordsEquals} from "./1-Core/checkPasswords.js";
import {checkIsFormFillFull} from "./1-Core/checkIsFormFillFull.js";

import {sendSignUpInfo} from "./2-Api/sendSignUpInfo.js";

import {togglePassword} from "./3-Ui/togglePassword.js";
import {clearPasswordError, drawPasswordsEnterError} from "./3-Ui/drawPasswordsEnterError.js";
import {drawFieldsIsNotFill, hiddenFieldsIsNotFill} from "./3-Ui/drawFieldsIsNotFill.js";
import {drawSomethingWrong} from "./3-Ui/drawSomethingWrong.js";

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
    if (checkIsFormFillFull()) {
        if (isPasswordsEquals()) {
            try {
                await sendSignUpInfo();
            } catch (error) {
                console.error(`SignUp ERROR: ${error.message}`);
            }
        } else {
            drawPasswordsEnterError();
        }
    } else {
        drawFieldsIsNotFill()
    }
});


// Прослушивание кнопки переключения показа пароля
document.addEventListener("toggle", () => {
    togglePassword();
});

// Прослушивание полей ввода пароля
document.addEventListener("password-click", () => {
    clearPasswordError();
});



