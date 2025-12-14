"use strict"


import {deleteSession, getSessionId} from "../../../0-Common/sessionId.js";
import {URL} from "../../../0-Common/url.js";

export async function checkUserSessionId() {
    const url = URL.IDENTITY_MANAGEMENT_CONTROLLER;
    const userSessionId = getSessionId("userSessionId")?.replace(/^"|"$/g, '');

    const requestData = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            sessionId: userSessionId
        })
    }

    const response = await fetch(`${url}/session_info`, requestData);
    const result = await response.json();

    if (result.isSuccess) {
        const showEvent = new CustomEvent("authorized-show");
        document.dispatchEvent(showEvent);

    } else {
        if (userSessionId !== null && userSessionId !== "") deleteSession("userSessionId");

        const hideEvent = new CustomEvent("authorized-hide");
        document.dispatchEvent(hideEvent);
    }
}