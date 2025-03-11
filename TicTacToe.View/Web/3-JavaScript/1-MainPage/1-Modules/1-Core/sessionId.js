"use strict"

export {getSessionId, createSessionId, getOrCreateSessionId};


function getCookie(name) {
    let value = `; ${document.cookie}`;
    let parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}


function setCookie(name, value, minutes) {
    const expires = new Date();
    expires.setTime(expires.getTime() + (minutes * 60 * 1000));
    document.cookie = `${name}=${value}; expires=${expires.toUTCString()}; path=/`;
}

function getSessionId(key) {
    let sessionId;

    try {
        sessionId = getCookie(key);
    } catch (error) {
        console.error(`Error reading from sessionStorage: ${error.message}`);
    }

    return sessionId;
}


function createSessionId(key) {
    let sessionId;

    try {
        sessionId = crypto.randomUUID();
        setCookie(key, sessionId,5)
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