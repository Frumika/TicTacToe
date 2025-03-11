"use strict"

import {URL} from "../0-Common/url.js";



document.addEventListener("DOMContentLoaded", () => {
    const url = URL.SIGN_UP_CONTROLLER;

    const form = document.querySelector(".sign_up-container__form");
    const loginInput = document.getElementById("login");

    const passwordFields = document.querySelectorAll(".form__password");

    const passwordInput = document.getElementById("password-entering");
    const confirmPasswordInput = document.getElementById("password-confirmation");


    if (form && passwordFields) {
        form.addEventListener("submit", async (event) => {
            event.preventDefault();

            const login = loginInput.value;
            const password = passwordInput.value;
            const confirmPassword = confirmPasswordInput.value;

            if (password !== confirmPassword) {
                passwordInput.value = "";
                confirmPasswordInput.value = "";

                passwordFields.forEach(field => field.style.border = "3px solid red");

                passwordInput.placeholder = "Passwords do not match!";
                confirmPasswordInput.placeholder = "Passwords do not match!";

                return;
            }


            passwordInput.placeholder = "Password";
            confirmPasswordInput.placeholder = "Password confirmation";


            try {
                const response = await fetch(`${url}/register`, {
                    method: "POST",
                    headers: {"Content-Type": "application/json"},
                    body: JSON.stringify({login, password})
                });

                if (response.ok) {
                    window.location.href = "../1-HTML/index.html";
                } else {
                    alert("Registration failed!");
                }

            } catch (error) {
                console.log(error.message);
            }
        });
    }

    [passwordInput, confirmPasswordInput].forEach(input => {
        input.addEventListener("focus", () => {
            passwordFields.forEach(field => {
                field.style.border = "3px solid white";
            });

            passwordInput.placeholder = "Password";
            confirmPasswordInput.placeholder = "Password confirmation";
        });
    });

});