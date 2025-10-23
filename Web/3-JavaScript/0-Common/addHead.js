"use strict"

export async function addHead({title, styles = []} = {}) {
    try {
        const response = await fetch("../1-HTML/Shared/head.html");
        const headHTML = await response.text();

        document.head.insertAdjacentHTML("afterbegin", headHTML);

        const titleElement = document.createElement("title");
        titleElement.textContent = title;
        document.head.appendChild(titleElement);

        for (const href of styles) {
            const link = document.createElement("link");
            link.rel = "stylesheet";
            link.href = href;
            document.head.appendChild(link);
        }

    } catch (error) {
        console.error("Ошибка при добавлении head:", error);
    }
}