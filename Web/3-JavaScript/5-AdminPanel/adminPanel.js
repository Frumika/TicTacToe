document.addEventListener('DOMContentLoaded', () => {
    loadUsers();
});

async function loadUsers() {
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

        if (!result.isSuccess) {
            throw new Error(result.message || 'Ошибка API');
        }

        renderUserList(result.data.users);

    } catch (error) {
        console.error('Ошибка загрузки пользователей:', error);
    }
}

function renderUserList(users) {
    const userList = document.querySelector('.user-list');

    // очищаем статические li из HTML
    userList.innerHTML = '';

    users.forEach(user => {
        const li = document.createElement('li');
        li.classList.add('user-item');

        const button = document.createElement('button');
        button.classList.add('user-item__button');
        button.textContent = user.login;

        // если нужно — можно повесить обработчик
        button.addEventListener('click', () => {
            console.log('Выбран пользователь:', user);
        });

        li.appendChild(button);
        userList.appendChild(li);
    });
}
