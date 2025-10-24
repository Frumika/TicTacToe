"use strict"


export function togglePassword() {
    let passwordFields = [
        document.getElementById("password-entering"),
        document.getElementById("password-confirmation")
    ];

    let eyeIcons = document.querySelectorAll(".toggle-password img");

    let isHidden = passwordFields[0].type === "password";

    passwordFields.forEach(field => {
        field.type = isHidden ? "text" : "password";
    });

    eyeIcons.forEach(icon => {
        icon.src = isHidden ? "/Web/4-Sources/Svg/eye_open.svg" : "/Web/4-Sources/Svg/eye_closed.svg";
        icon.alt = isHidden ? "Hide" : "Show";
    });
}
