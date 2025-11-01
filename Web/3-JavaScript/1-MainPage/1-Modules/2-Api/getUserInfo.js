"use strict"

import {URL} from "../../../0-Common/url.js";
import {getSessionId} from "../../../0-Common/sessionId.js";
import {IdentityStatusCodeHelper} from "../../../0-Common/Helpers/IdentityStatusCodeHelper.js";


export async function getUserLogin() {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const userSessionId = getSessionId("userSessionId")?.replace(/^"|"$/g, '');
    if (!userSessionId) {
        throw new Error("The user's session does not exist");
    }

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            sessionId: userSessionId
        })
    }

    const response = await fetch(`${url}/info`, requestData);
    const result = await response.json();

    if (!result.isSuccess) {
        throw new Error(result.message);
    }
    
    return result.data.login;
}