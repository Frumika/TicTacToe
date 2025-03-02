"use strict"

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
                console.log("Password not confirm")

                passwordInput.value = "";
                confirmPasswordInput.value = "";

                passwordFields.forEach(field => field.style.border = "3px solid red");

                passwordInput.placeholder = "Passwords do not match!";
                confirmPasswordInput.placeholder = "Passwords do not match!";

                return;
            }


            passwordInput.placeholder = "Password";
            confirmPasswordInput.placeholder = "Password confirmation";

            const userData = {
                login: login,
                password: password
            }

            try {
                const response = await fetch(`${API_BASE_URL}/api/register`, {
                    method: "POST",
                    headers: {"Content-Type": "application/json"},
                    body: JSON.stringify(userData)
                });

                if (response.ok) {
                    window.location.href = "../HTML/index.html"
                } else {
                    alert("Registration failed!");
                }

            } catch (error) {

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