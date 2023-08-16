setInterval(() => {
    DD = new Date();
    Hours = DD.getHours();
    Minutes = DD.getMinutes();
    Seconds = DD.getSeconds();

    Hours = ('0' + Hours).slice(-2);
    Minutes = ('0' + Minutes).slice(-2);
    Seconds = ('0' + Seconds).slice(-2);

    document.querySelector('#now').textContent = "現在時刻 " + Hours + ":" + Minutes + ":" + Seconds;
}, 100);

window.onload = function () {
    const dropDownList = document.getElementById("selsectWorkItem");
    const button = document.getElementById("workBtn");
    const workStatus = document.getElementById("workStatus");
    const selectWorkdiv = document.getElementById("selectWorkdiv");
    const text = dropDownList.value;
    if (text == "hidden") {
        button.disabled = "disabled";
    } else {
        button.disabled = null;
        document.getElementById("workItemName").value = text;
    }
    if (workStatus.value == "Start") {
        selectWorkdiv.setAttribute("hidden", true);
        dropDownList.setAttribute("hidden", true);
    }
}

const select = document.getElementById("selsectWorkItem");
select.onchange = event => {
    const button = document.getElementById("workBtn");
    button.disabled = null;
    document.getElementById("workItemName").value = select.value;
}
