document.addEventListener('DOMContentLoaded', () => {
    loadUsers();
});

let selectedUser = null;

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

function selectUser(user) {
    selectedUser = user;

    document.getElementById('login').value  = user.login ?? '';
    document.getElementById('wins').value   = user.wins ?? 0;
    document.getElementById('losses').value = user.losses ?? 0;
    document.getElementById('draws').value  = user.draws ?? 0;

    console.log('Выбран пользователь:', user);
}


function renderUserList(users) {
    const userList = document.querySelector('.user-list');

    userList.innerHTML = '';

    users.forEach(user => {
        const li = document.createElement('li');
        li.classList.add('user-item');

        const button = document.createElement('button');
        button.classList.add('user-item__button');
        button.textContent = user.login;

        // если нужно — можно повесить обработчик
        button.addEventListener('click', () => {
            selectUser(user);
        });

        li.appendChild(button);
        userList.appendChild(li);
    });
}



document.getElementById('confirm').addEventListener('click', async () => {
    if (!selectedUser) {
        alert('Пользователь не выбран');
        return;
    }

    const updatedUser = {
        id: selectedUser.id,
        login: document.getElementById('login').value,
        wins: Number(document.getElementById('wins').value),
        losses: Number(document.getElementById('losses').value),
        draws: Number(document.getElementById('draws').value),

        matches: selectedUser.matches,
        isAdmin: selectedUser.isAdmin
    };

    try {
        const response = await fetch(
            'http://localhost:5026/api/admin/update',
            {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': '*/*'
                },
                body: JSON.stringify(updatedUser)
            }
        );

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const result = await response.json();

        if (!result.isSuccess) {
            throw new Error(result.message || 'Ошибка обновления');
        }

        alert('Данные пользователя обновлены');

        // обновляем список пользователей
        loadUsers();

    } catch (error) {
        console.error('Ошибка обновления пользователя:', error);
        alert('Ошибка при обновлении пользователя');
    }
});


// Удаление пользователя
document.getElementById('delete').addEventListener('click', async () => {
    if (!selectedUser) {
        alert('Пользователь не выбран');
        return;
    }

    const confirmed = confirm(
        `Вы уверены, что хотите удалить пользователя "${selectedUser.login}"?`
    );

    if (!confirmed) return;

    try {
        const response = await fetch(
            `http://localhost:5026/api/admin/delete/${selectedUser.id}`,
            {
                method: 'DELETE',
                headers: {
                    'Accept': '*/*'
                }
            }
        );

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`);
        }

        const result = await response.json();

        if (!result.isSuccess) {
            throw new Error(result.message || 'Ошибка удаления');
        }

        alert(result.message || 'Пользователь удалён');

        // сбрасываем форму
        clearForm();
        selectedUser = null;

        // обновляем список
        loadUsers();

    } catch (error) {
        console.error('Ошибка удаления пользователя:', error);
        alert('Ошибка при удалении пользователя');
    }
});
function clearForm() {
    document.getElementById('login').value = '';
    document.getElementById('wins').value = '';
    document.getElementById('losses').value = '';
    document.getElementById('draws').value = '';
}

