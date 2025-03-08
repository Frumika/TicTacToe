"use strict"

export {getSessionId, createSessionId, getOrCreateSessionId};

function getSessionId(key) {
    let sessionId;

    try {
        sessionId = sessionStorage.getItem(key);
        if (!sessionId) console.warn(`Session ID with key '${key}' not found.`);
    } catch (error) {
        console.error(`Error reading from sessionStorage: ${error.message}`);
    }

    return sessionId;
}


function createSessionId(key) {
    let sessionId;

    try {
        sessionId = crypto.randomUUID();
        sessionStorage.setItem(key, sessionId);
    } catch (error) {
        console.error(`Error generating sessionId: ${error.message}`);
    }

    return sessionId;
}

function getOrCreateSessionId(key) {
    let sessionId = getSessionId(key);
    if (sessionId == null) sessionId = createSessionId(key);
    return sessionId;
}