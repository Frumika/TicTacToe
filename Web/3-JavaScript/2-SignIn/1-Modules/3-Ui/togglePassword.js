"use strict"


export function togglePassword() {
    let passwordInput = document.getElementById("password");
    let eyeIcon = document.getElementById("image-eye-closed");

    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        eyeIcon.src = "/Web/4-Sources/Svg/eye_open.svg";
        eyeIcon.alt = "Hide";
    } else {
        passwordInput.type = "password";
        eyeIcon.src = "/Web/4-Sources/Svg/eye_closed.svg";
        eyeIcon.alt = "Show";
    }
}