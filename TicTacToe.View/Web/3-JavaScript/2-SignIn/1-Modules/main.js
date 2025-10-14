"use strict"


import {checkIsFormFillFull} from "./1-Core/checkIsFormFillFull.js";
import {addHead} from "../../0-Common/addHead.js";

import {sendInfo} from "./2-Api/sendInfo.js";

import {togglePassword} from "./3-Ui/togglePassword.js";
import {drawFieldsIsNotFill} from "./3-Ui/drawWarnings.js";

import {listenSendButton} from "./4-Events/listenSendButton.js";
import {listenTogglePassword} from "./4-Events/listenTogglePassword.js";


document.addEventListener("DOMContentLoaded", async () => {
    await addHead({
        title: "Sign In",
        styles: ["../2-CSS/signIn.css"]
    });

    listenSendButton()
    listenTogglePassword();
});


document.addEventListener("send", async () => {
    if (checkIsFormFillFull()) {
        try {
            await sendInfo();
        } catch (error) {
            console.error(`Sign In Error: ${error.error}`)
        }
    } else {
        drawFieldsIsNotFill();
    }
});


document.addEventListener("toggle", () => {
    togglePassword();
});
