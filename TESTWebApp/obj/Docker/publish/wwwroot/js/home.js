// リアルタイムクロック
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


// 画面がロードした時設定
window.onload = function () {
    var dropDownList = document.getElementById("selsectWorkItem");
    document.getElementById("selectMiddleWorkItemdiv").style.display = 'none';
    document.getElementById("selectMinorWorkItemdiv").style.display = 'none';
    var button = document.getElementById("workBtn");
    var workStatus = document.getElementById("workStatus");
    var selectWorkdiv = document.getElementById("selectWorkdiv");
    var text = dropDownList.value;
    if (text == "hidden") {
        button.disabled = "disabled";
    } else {
        button.disabled = null;
        //document.getElementById("TmpFormMajorWorkItemId").value = text;
    }
    if (workStatus.value == "Start") {
        selectWorkdiv.setAttribute("hidden", true);
        dropDownList.setAttribute("hidden", true);
    }
}


// 大作業項目のドロップダウンが選択された際の動作
let selectWorkItem = document.getElementById("selsectWorkItem");
selectWorkItem.onchange = selectWorkItemEvent => {
    const button = document.getElementById("workBtn");
    button.disabled = false;

    // 小作業項目のプルダウンを非表示にする
    document.getElementById("selectMinorWorkItemdiv").style.display = 'none';

    // プルダウンから選択された大作業項目のIDをTmpFormにセットする
    document.getElementById("TmpFormMajorWorkItemId").value = selectWorkItem.value;
    // 中作業項目と小作業項目のIDが格納されるTmpFormを空にする
    document.getElementById("TmpFormMiddleworkItemId").value = '';
    document.getElementById("TmpFormMinorworkItemId").value = '';

    // 中作業項目のデータを取得してプルダウンにセットする
    getAllMiddleWorkItem(selectWorkItem.value);
    // 中作業項目のプルダウンを表示させる
    document.getElementById("selectMiddleWorkItemdiv").style.display = '';
}


// 中作業項目のドロップダウンが選択された場合
let selectMiddleWorkItem = document.getElementById("selectMiddleWorkItem");
selectMiddleWorkItem.onchange = selectMiddleWorkItemEvent => {
    // 選択された中作業項目のID
    var middleworkItemId = selectMiddleWorkItem.value;

    // プルダウンから選択された中作業項目のIDをTmpFormにセットする
    document.getElementById("TmpFormMiddleworkItemId").value = middleworkItemId;
    // 小作業項目のIDが格納されるTmpFormを空にする
    document.getElementById("TmpFormMinorworkItemId").value = '';

    // 小作業項目のデータを取得してプルダウンにセットする
    this.getAllMinorWorkItem(middleworkItemId);
    // 小作業項目のプルダウンを表示させる
    document.getElementById("selectMinorWorkItemdiv").style.display = '';
}


// 小作業項目のドロップダウンが選択された場合
let selectMinorWorkItem = document.getElementById("selectMinorWorkItem");
selectMinorWorkItem.onchange = selectMinorWorkItemEvent => {
    // 選択された小作業項目のID
    var minorworkItemId = selectMinorWorkItem.value;
    // プルダウンから選択された小作業項目のIDをTmpFormにセットする
    document.getElementById("TmpFormMinorworkItemId").value = minorworkItemId;
}


// 2銃クリック禁止機能の実装
// クリックがトリガーではなくpostをsubmitしたタイミングをトリガーにするのがキモ
var isSubmit = false;
function checkNijyuSubmit() {
    if (isSubmit) {
        // submit中の場合、ボタンをdisる
        const workBtn = document.getElementById("workBtn");
        workBtn.disabled = true;
        return false;
    } else {
        //フラグがtrueでなければ、フラグをtrueにした上でsubmitする
        isSubmit = true;
        return true;
    }
}


// APIを叩いて中作業項目のドロップダウンを生成する
function getAllMiddleWorkItem(id) {
    fetch(`API/GetMiddleWorkItems/${id}`)
        .then(response => response.json())
        .then(data => _SetMiddleWorkItemsForSelectBox(data))
        .catch(error => alert('中作業項目の取得に失敗しました。', error));
}
function _SetMiddleWorkItemsForSelectBox(data) {
    var middleWorkItemSelectBox = document.getElementById("selectMiddleWorkItem");
    // 既にある作業項目を全削除
    var options = document.querySelectorAll('#selectMiddleWorkItem option');
    options.forEach(o => o.remove());
    // 初期値を設定
    var hiddnOption = document.createElement("option");
    hiddnOption.text = "中作業項目(自由選択)";
    hiddnOption.value = "hidden";
    hiddnOption.hidden = true;
    middleWorkItemSelectBox.appendChild(hiddnOption);
    // httpリクエストで取得したデータをドロップダウン化
    data.forEach(item => {
        var option = document.createElement("option");
        option.text = item.middleWorkItemName;
        option.value = item.middleWorkItemId;
        middleWorkItemSelectBox.appendChild(option);
    });
}


// APIを叩いて小作業項目のドロップダウンを生成する
function getAllMinorWorkItem(id) {
    fetch(`API/GetMinorWorkItems/${id}`)
        .then(response => response.json())
        .then(data => _SetMinorWorkItemsForSelectBox(data))
        .catch(error => console.error('小作業項目の取得に失敗しました。', error));
}
function _SetMinorWorkItemsForSelectBox(data) {
    var minorWorkItemSelectBox = document.getElementById("selectMinorWorkItem");
    // 既にある作業項目を全削除
    var options = document.querySelectorAll('#selectMinorWorkItem option');
    options.forEach(o => o.remove());
    // 初期値を設定
    var hiddnOption = document.createElement("option");
    hiddnOption.text = "小作業項目(自由選択)";
    hiddnOption.value = "hidden";
    hiddnOption.hidden = true;
    minorWorkItemSelectBox.appendChild(hiddnOption);
    // httpリクエストで取得したデータをドロップダウン化
    data.forEach(item => {
        var option = document.createElement("option");
        option.text = item.minorWorkItemName;
        option.value = item.minorWorkItemId;
        minorWorkItemSelectBox.appendChild(option);
    });
}
