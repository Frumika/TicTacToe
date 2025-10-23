"use strict"

import {URL} from "../../../0-Common/url.js";
import {IdentityStatusHelper} from "../../../0-Common/Helpers/IdentityStatusHelper.js";

export async function sendUpdate(login, isWin) {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            login: login,
            isWin: isWin
        })
    };

    const response = await fetch(`${url}/update`, requestData);
    const result = await response.json();

    if (IdentityStatusHelper.isFailure(result)) {
        throw new Error(result.message);
    }

    if (IdentityStatusHelper.isSuccess(result)) {
        console.log("Новые инструменты работают!");
    }
}