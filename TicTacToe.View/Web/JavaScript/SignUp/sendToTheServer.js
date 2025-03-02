"use strict"

const API_BASE_URL = "http://localhost:5026";

document.addEventListener("DOMContentLoaded", () => {
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
                const response = await fetch(`${API_BASE_URL}/api/signup/register`, {
                    method: "POST",
                    headers: {"Content-Type": "application/json"},
                    body: JSON.stringify({login, password})
                });

                if (response.ok) {
                    window.location.href = "../HTML/index.html"
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