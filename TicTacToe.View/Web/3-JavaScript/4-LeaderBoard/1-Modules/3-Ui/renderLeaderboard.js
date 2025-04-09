"use strict"

export function renderLeaderboard(data) {


    const tbody = document.querySelector('.table-body');
    tbody.innerHTML = ''; // очищаем

    data.forEach(player => {
        const row = document.createElement('tr');

        row.innerHTML = `
      <td class="table-body__text">${player.login}</td>
      <td class="table-body__text">${player.matches}</td>
      <td class="table-body__text">${player.wins}</td>
      <td class="table-body__text">${player.losses}</td>
    `;
        tbody.appendChild(row);
    });
}