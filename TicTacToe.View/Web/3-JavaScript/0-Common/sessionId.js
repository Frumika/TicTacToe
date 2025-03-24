"use strict"

export {getSessionId, createSessionId, addSessionId, deleteSession, getOrCreateSessionId};


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

function deleteCookie(name) {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/`;
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

function createSessionId(key, minutes) {
    let sessionId;

    try {
        sessionId = crypto.randomUUID();
        setCookie(key, sessionId, minutes)
    } catch (error) {
        console.error(`Error generating sessionId: ${error.message}`);
    }

    return sessionId;
}

function addSessionId(key, sessionId, minutes) {
    setCookie(key, sessionId, minutes);
}

function deleteSession(key) {
    deleteCookie(key);
}

function getOrCreateSessionId(key, minutes) {
    let sessionId = getSessionId(key);
    if (sessionId == null) sessionId = createSessionId(key, minutes);
    return sessionId;
}