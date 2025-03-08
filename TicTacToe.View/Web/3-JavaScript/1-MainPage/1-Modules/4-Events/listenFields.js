"use strict"

export function listenFields() {
    // Собираем все игровые поля с доски
    const fields = document.querySelectorAll(".board__field");

    // Для каждого поля ставим "слушатель" на нажатие
    fields.forEach((field) => {
        field.addEventListener("click", async () => {
            try {
                // Если нажатие произошло, собираем координаты этого поля
                const row = parseInt(field.dataset.row);
                const column = parseInt(field.dataset.column);

                // Создаем пользовательское событие
                const moveEvent = new CustomEvent("move", {detail: {row, column}})

                // Отправляем событие
                document.dispatchEvent(moveEvent);
            } catch (error) {
                console.error("Move failed: ", error.message);
            }
        });
    });
}