async function getUsers() {
    try {
        const response = await fetch(
            'http://localhost:5026/api/admin/get/list',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': '*/*'
                },
                body: JSON.stringify({
                    skipModifier: 0,
                    usersCount: 15
                })
            }
        );

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const result = await response.json();

        console.log('Ответ сервера:', result);
        return result.data.users;

    } catch (error) {
        console.error('Ошибка при получении пользователей:', error);
    }
}

getUsers();
