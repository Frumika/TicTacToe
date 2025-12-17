const url = `https://api/admin/get`

fetch(url)
    .then(response => {
        // Проверяем, успешен ли ответ
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        // Преобразуем ответ в JSON
        return response.json();
    })
    .then(users => {
        // 'users' - это массив объектов пользователей
        console.log('Список пользователей:', users);
        // Здесь вы можете обновить UI, например, отобразить список
        // users.forEach(user => { console.log(user.name); });
    })
    .catch(error => {
        console.error('Произошла ошибка при получении пользователей:', error);
    });







