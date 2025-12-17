"use strict"


import {URL} from "../../../0-Common/url.js";
import {deleteSession, getSessionId} from "../../../0-Common/sessionId.js";
import {hideUserInfo, switchAccountOptions} from "../3-Ui/drawAuthorization.js";


export async function sendLogoutAllRequest() {
    let response = undefined;
    let result = undefined;

    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    const userSessionId = getSessionId("userSessionId")?.replace(/^"|"$/g, '');
    if (!userSessionId) {
        throw new Error("User session not found");
    }

    const getUserSessionInfoRequest = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            sessionId: userSessionId
        })
    };

    response = await fetch(`${url}/session_info`, getUserSessionInfoRequest);
    result = await response.json();

    if (!result.isSuccess) throw new Error(result.message);

    const logoutAllUserSessionsRequest = {
        method: "DELETE",
        headers: {"Content-Type": "application/json"},
    }

    response = await fetch(`${url}/logout_all/${result.data.id}`, logoutAllUserSessionsRequest);
    result = await response.json();

    if (result.isSuccess) {
        deleteSession("userSessionId");
        hideUserInfo();
        switchAccountOptions();
    } else {
        throw new Error(result.message);
    }
}