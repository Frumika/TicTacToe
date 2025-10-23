"use strict"

import {URL} from "../../../0-Common/url.js";
import {deleteSession, getSessionId} from "../../../0-Common/sessionId.js";
import {hideUserInfo, switchAccountOptions} from "../3-Ui/drawAuthorization.js";


export async function sendSignOutRequest() {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const userSessionId = getSessionId("userSessionId")?.replace(/^"|"$/g, '');
    if (!userSessionId) {
        throw new Error("User session not found");
    }

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            sessionId: userSessionId
        })
    };

    const response = await fetch(`${url}/signout`, requestData);
    const result = await response.json();

    if (!result.isSuccess) {
        throw new Error(result.message);
    } else {
        deleteSession("userSessionId");
        hideUserInfo();
        switchAccountOptions();
    }
}