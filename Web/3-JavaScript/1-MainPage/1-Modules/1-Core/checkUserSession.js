"use strict"


import {getSessionId} from "../../../0-Common/sessionId.js";

export function checkUserSessionId() {
    const userSessionId = getSessionId("userSessionId");


    if (userSessionId !== "" && userSessionId !== null) {
        const showEvent = new CustomEvent("authorized-show");
        document.dispatchEvent(showEvent);
    }

    else{
        const hideEvent = new CustomEvent("authorized-hide");
        document.dispatchEvent(hideEvent);
    }
}