// ユーザログイン画面
window.onload = function () {
    const dropDownList = document.getElementById("selectUser");
    const button = document.getElementById("loginBtn");
    const text = dropDownList.value;
    if (text == "hidden") {
        button.disabled = "disabled";
    } else {
        button.disabled = null;
        document.getElementById("UserId").value = text;
    }
}

// ユーザログイン画面
const selectUserName = document.getElementById("selectUser");
selectUserName.onchange = event => {
    const loginButton = document.getElementById("loginBtn");
    loginButton.disabled = null;
    document.getElementById("UserId").value = selectUserName.value;
}
