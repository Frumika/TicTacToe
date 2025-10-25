"use strict"

import {URL} from "../../../0-Common/url.js";
import {IdentityStatusCodeHelper} from "../../../0-Common/Helpers/IdentityStatusCodeHelper.js";

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

    if (IdentityStatusCodeHelper.isFailure(result)) {
        throw new Error(result.message);
    }

    if (IdentityStatusCodeHelper.isSuccess(result)) {
        console.log("Новые инструменты работают!");
    }
}