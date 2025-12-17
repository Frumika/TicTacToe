"use strict"

import {URL} from "../../../0-Common/url.js";
import {IdentityStatusCodeHelper} from "../../../0-Common/Helpers/IdentityStatusCodeHelper.js";

export async function sendUpdate(login, endGameType) {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const requestData = {
        method: "PATCH",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            login: login,
            type: endGameType
        })
    };

    const response = await fetch(`${url}/update_stats`, requestData);
    const result = await response.json();

    if (IdentityStatusCodeHelper.isFailure(result)) {
        throw new Error(result.message);
    }

    if (IdentityStatusCodeHelper.isSuccess(result)) {
        console.log("Новые инструменты работают!");
    }
}