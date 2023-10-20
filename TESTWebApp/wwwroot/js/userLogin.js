/*
 ユーザログイン画面.js
*/

//ユーザIDpostdata
const postDataEl = document.getElementById("UserId");

// ログインボタン
const loginButtonEl = document.getElementById("loginBtn");

// ユーザ情報選択
// ＊テキストボックスだが、クリックするとモーダルが表示されユーザ情報を選ぶことができる
const userSelectBox = document.getElementById("userSelectBox");
//  ユーザ選択の表示用aタグ
const choiseLoginUserName = document.getElementById("choiseLoginUserName");


window.onload = function () {
    // ログインボタンは非活性にする
    loginButtonEl.disabled = "disabled";
}


// セレクトボックスがクリックされたらユーザを選択させるモーダルを表示させる
userSelectBox.onclick = event => {
    SelectUserEvent();
}

// ユーザが選択されたら
function SelectOnClick(id, dipData) {
    postDataEl.value = id;
    choiseLoginUserName.textContent = dipData;
    loginButtonEl.disabled = null;
    modal.classList.add('hidden');
    mask.classList.add('hidden');
}


const close = document.getElementById('no');
function SelectUserEvent() {
    var modal = document.getElementById('modal');
    var mask = document.getElementById('mask');
    modal.classList.remove('hidden');
    mask.classList.remove('hidden');

    close.addEventListener('click', function () {
        modal.classList.add('hidden');
        mask.classList.add('hidden');
    });
}
