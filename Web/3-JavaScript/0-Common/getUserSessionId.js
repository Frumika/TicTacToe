"use strict"


import {getSessionId} from "./sessionId.js";

export function getUserSessionId() {
    return getSessionId("userSessionId")?.replace(/^"|"$/g, '');
}