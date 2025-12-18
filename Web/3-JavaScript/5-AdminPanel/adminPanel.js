document.addEventListener('DOMContentLoaded', async () => {
    await fetchAllUsers();
    renderCurrentPage();
});


let selectedUser = null;
let currentPage = 1;
const USERS_PER_PAGE = 6;
let allUsers = [];

// Получение пользователя
async function fetchAllUsers() {
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
                    usersCount: 999
                })
            }
        );

        const result = await response.json();
        console.log('Ответ сервера:', result);

        allUsers = result?.data?.users ?? [];

    } catch (e) {
        console.error('Ошибка загрузки пользователей', e);
    }
}

function renderCurrentPage() {
    const start = (currentPage - 1) * USERS_PER_PAGE;
    const end = start + USERS_PER_PAGE;

    const pageUsers = allUsers.slice(start, end);

    console.log(
        'Page:', currentPage,
        'start:', start,
        'end:', end,
        'pageUsers:', pageUsers
    );

    renderUserList(pageUsers);
    updatePageLabel();
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

        button.addEventListener('click', () => {
            selectUser(user);
        });

        li.appendChild(button);
        userList.appendChild(li);
    });
}

function updatePageLabel(currentCount, totalCount) {
    document.querySelector('.page-name').textContent = `Page_${currentPage}`;

    document.querySelector('.button-prev').disabled = currentPage === 1;
    document.querySelector('.button-next').disabled =
        currentPage * USERS_PER_PAGE >= totalCount;
}

document.querySelector('.button-next').addEventListener('click', () => {
    currentPage++;
    renderCurrentPage();
});
document.querySelector('.button-prev').addEventListener('click', () => {
    if (currentPage === 1) return;

    currentPage--;
    renderCurrentPage();
});


// Обновление пользователя
document.getElementById('confirm').addEventListener('click', async () => {
    if (!selectedUser) {
        alert('Пользователь не выбран');
        return;
    }

    const payload = {
        userId: selectedUser.id,
        userLogin: document.getElementById('login').value,
        wins: Number(document.getElementById('wins').value),
        losses: Number(document.getElementById('losses').value),
        draws: Number(document.getElementById('draws').value)
    };

    try {
        const response = await fetch(
            'http://localhost:5026/api/admin/update',
            {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': '*/*'
                },
                body: JSON.stringify(payload)
            }
        );

        const result = await response.json();

        if (!response.ok || result.code !== 'Success') {
            throw new Error(result.message || 'Ошибка обновления');
        }

        alert('Статистика пользователя обновлена');

        // локально обновляем данные
        selectedUser.wins = payload.wins;
        selectedUser.losses = payload.losses;
        selectedUser.draws = payload.draws;
        selectedUser.matches =
            payload.wins + payload.losses + payload.draws;

        renderCurrentPage();

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

