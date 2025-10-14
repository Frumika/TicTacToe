"use strict"

import {addHead} from "../../0-Common/addHead.js";

import {isPasswordsEquals} from "./1-Core/checkPasswords.js";
import {checkIsFormFillFull} from "./1-Core/checkIsFormFillFull.js";

import {sendInfo} from "./2-Api/sendInfo.js";

import {togglePassword} from "./3-Ui/togglePassword.js";
import {drawFieldsIsNotFill} from "./3-Ui/drawWarnings.js";
import {clearPasswordError, drawPasswordsEnterError} from "./3-Ui/drawErrors.js";

import {listenSendButton} from "./4-Events/listenSendButton.js";
import {listenTogglePassword} from "./4-Events/listenTogglePasswordButton.js";
import {listenPasswordInputField} from "./4-Events/listenPasswordsInputField.js";


// Прослушивание загрузки страницы
document.addEventListener("DOMContentLoaded", async () => {
    await addHead({
        title: "Sign Up",
        styles: ["../2-CSS/signUp.css"]
    });

    listenSendButton();
    listenTogglePassword();
    listenPasswordInputField();
});


// Прослушивание кнопки отправки на сервер
document.addEventListener("send", async () => {
    if (checkIsFormFillFull()) {
        if (isPasswordsEquals()) {
            try {
                await sendInfo();
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



