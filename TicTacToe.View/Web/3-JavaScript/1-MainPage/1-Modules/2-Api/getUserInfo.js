"use strict"

import {URL} from "../../../0-Common/url.js";
import {getSessionId} from "../../../0-Common/sessionId.js";


export async function getUserLogin() {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;

    let login;

    const userSessionId = getSessionId("userSessionId")?.replace(/^"|"$/g, '');
    if (!userSessionId) {
        throw new Error("The user's session does not exist");
    }

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(userSessionId)
    }

    const response = await fetch(`${url}/info`, requestData);

    if (!response.ok) {
        const error = await response.json();
        throw new Error(JSON.stringify(error.message));
    } else {
        const userInfo = await response.json();
        console.log(JSON.stringify(userInfo));

        login = userInfo.user.login;
        console.log(login);
    }

    return login;
}