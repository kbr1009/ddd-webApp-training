// 画面がロードした時設定
const workStatus = document.getElementById("workStatus");
const workBtn = document.getElementById("workBtn");
const majorItemSelectBox = document.getElementById("majorItemSelectBox");
const middleItemSelectBox = document.getElementById("middleItemSelectBox");
const minorItemSelectBox = document.getElementById("minorItemSelectBox");
window.onload = function () {
    //var dropDownList = document.getElementById("selsectWorkItem");
    //var text = dropDownList.value;

    // ワークボタンの活性・非活性を設定する
    // *ステータスがない場合（入力初期）、作業終了で終わっている場合、
    //  ユーザに作業項目を選択させる必要があるため、ワークボタンを非活性にする
    if (workStatus.value == "" || workStatus.value == "End") {
        workBtn.disabled = true;
    }

    // 中項目・小項目を隠す
    //document.getElementById("selectMiddleWorkItemdiv").style.display = 'none';
    middleItemSelectBox.style.display = 'none';
    minorItemSelectBox.style.display = 'none';
    // ボタンの上の空白を初期は非表示する
    document.getElementById("workBtnSpace").style.display = 'none';

    //document.getElementById("selectMinorWorkItemdiv").style.display = 'none';
    //var button = document.getElementById("workBtn");
    //var workStatus = document.getElementById("workStatus");

    //var selectWorkdiv = document.getElementById("selectWorkdiv");


    //if (text == "hidden") {
    //    button.disabled = "disabled";
    //} else {
    //    button.disabled = null;
    //}
}


// 大作業項目のドロップダウンが選択された際の動作
//let selectWorkItem = document.getElementById("selsectWorkItem");
//selectWorkItem.onchange = selectWorkItemEvent => {
//    const button = document.getElementById("workBtn");
//    button.disabled = false;

//    // 小作業項目のプルダウンを非表示にする
//    document.getElementById("selectMinorWorkItemdiv").style.display = 'none';

//    // プルダウンから選択された大作業項目のIDをTmpFormにセットする
//    document.getElementById("TmpFormMajorWorkItemId").value = selectWorkItem.value;
//    // 中作業項目と小作業項目のIDが格納されるTmpFormを空にする
//    document.getElementById("TmpFormMiddleworkItemId").value = '';
//    document.getElementById("TmpFormMinorworkItemId").value = '';

//    // 中作業項目のデータを取得してプルダウンにセットする
//    getAllMiddleWorkItem(selectWorkItem.value);
//    // 中作業項目のプルダウンを表示させる
//    document.getElementById("selectMiddleWorkItemdiv").style.display = '';
//}


//// 中作業項目のドロップダウンが選択された場合
//let selectMiddleWorkItem = document.getElementById("selectMiddleWorkItem");
//selectMiddleWorkItem.onchange = selectMiddleWorkItemEvent => {
//    // 選択された中作業項目のID
//    var middleworkItemId = selectMiddleWorkItem.value;

//    // プルダウンから選択された中作業項目のIDをTmpFormにセットする
//    document.getElementById("TmpFormMiddleworkItemId").value = middleworkItemId;
//    // 小作業項目のIDが格納されるTmpFormを空にする
//    document.getElementById("TmpFormMinorworkItemId").value = '';

//    // 小作業項目のデータを取得してプルダウンにセットする
//    this.getAllMinorWorkItem(middleworkItemId);
//    // 小作業項目のプルダウンを表示させる
//    document.getElementById("selectMinorWorkItemdiv").style.display = '';
//}


//// 小作業項目のドロップダウンが選択された場合
//let selectMinorWorkItem = document.getElementById("selectMinorWorkItem");
//selectMinorWorkItem.onchange = selectMinorWorkItemEvent => {
//    // 選択された小作業項目のID
//    var minorworkItemId = selectMinorWorkItem.value;
//    // プルダウンから選択された小作業項目のIDをTmpFormにセットする
//    document.getElementById("TmpFormMinorworkItemId").value = minorworkItemId;
//}


// 2銃クリック禁止機能の実装
// クリックがトリガーではなくpostをsubmitしたタイミングをトリガーにするのがキモ
var isSubmit = false;
function checkNijyuSubmit() {
    if (isSubmit) {
        // submit中の場合、ボタンをdisる
        const workBtn =  document.getElementById("workBtn");
        workBtn.disabled = true;
        return false;
    } else {
        //フラグがtrueでなければ、フラグをtrueにした上でsubmitする
        isSubmit = true;
        return true;
    }
}

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


// APIを叩いて中作業項目のドロップダウンを生成する
function getAllMiddleWorkItem(id) {
    fetch(`API/GetMiddleWorkItems/${id}`)
        .then(response => response.json())
        .then(data => _SetMiddleWorkItemsForSelectBox(data))
        .catch(error => alert('中作業項目の取得に失敗しました。', error));
}
function _SetMiddleWorkItemsForSelectBox(data) {
    var middleItemSelectList = document.getElementById("middleItemSelectList");
    // 既にある作業項目を全削除
    //var delItemTag = document.querySelectorAll('#selectMiddleItem div');
    //delItemTag.forEach(d => d.remove());
    // middleItemSelectListタグの子要素をすべてリセットする
    while (middleItemSelectList.lastChild) {
        middleItemSelectList.removeChild(middleItemSelectList.lastChild);
    }

    //// 初期値を設定 ==> 改修前
    //var hiddnOption = document.createElement("option");
    //hiddnOption.text = "中作業項目(自由選択)";
    //hiddnOption.value = "hidden";
    //hiddnOption.hidden = true;
    //middleWorkItemSelectBox.appendChild(hiddnOption);
    // httpリクエストで取得したデータをドロップダウン化
    //data.forEach(item => {
    //    var option = document.createElement("option");
    //    option.text = item.middleWorkItemName;
    //    option.value = item.middleWorkItemId;
    //    middleWorkItemSelectBox.appendChild(option);
    //});

    // 中項目一覧のタグを生成する
    data.forEach(item => {
        var div = document.createElement("div");
        // 要素を設定していく
        // テキストコンテンツ
        div.textContent = item.middleWorkItemName;
        // id
        div.setAttribute("id", "selectMiddleItem");
        // class
        div.setAttribute("class", "selectItem");
        // onclick
        div.setAttribute("onclick", "SelectedMiddleWorkItem('" + item.middleWorkItemId + "','" + item.middleWorkItemName +"')");
        // 追加
        middleItemSelectList.appendChild(div);
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
    // 改修前コード
    //var minorWorkItemSelectBox = document.getElementById("selectMinorWorkItem");
    //// 既にある作業項目を全削除
    //var options = document.querySelectorAll('#selectMinorWorkItem option');
    //options.forEach(o => o.remove());
    //// 初期値を設定
    //var hiddnOption = document.createElement("option");
    //hiddnOption.text = "小作業項目(自由選択)";
    //hiddnOption.value = "hidden";
    //hiddnOption.hidden = true;
    //minorWorkItemSelectBox.appendChild(hiddnOption);
    //// httpリクエストで取得したデータをドロップダウン化
    //data.forEach(item => {
    //    var option = document.createElement("option");
    //    option.text = item.minorWorkItemName;
    //    option.value = item.minorWorkItemId;
    //    minorWorkItemSelectBox.appendChild(option);
    //});

    // 改修後コード
    var minorItemSelectList = document.getElementById("minorItemSelectList");
    // 既にある作業項目を全削除
    while (minorItemSelectList.lastChild) {
        minorItemSelectList.removeChild(minorItemSelectList.lastChild);
    }

    // 小項目一覧のタグを生成する
    data.forEach(item => {
        var div = document.createElement("div");
        // 要素を設定していく
        // テキストコンテンツ
        div.textContent = item.minorWorkItemName;
        // id
        div.setAttribute("id", "selectMinorItem");
        // class
        div.setAttribute("class", "selectItem");
        // onclick
        div.setAttribute("onclick", "SelectedMinorWorkItem('" + item.minorWorkItemId + "','" + item.minorWorkItemName + "')");
        // 追加
        minorItemSelectList.appendChild(div);
    });

}



/* 
====================
モーダル（大項目用）
====================
*/

// セレクトボックスがクリックされたらユーザを選択させるモーダルを表示させる
// 大項目
majorItemSelectBox.onclick = event => {
    var modal = document.getElementById('modalMajorItemSelet');
    var mask = document.getElementById('maskMajroItemSelect');
    modal.classList.remove('hidden');
    mask.classList.remove('hidden');

    var closeBtn = document.getElementById('no');
    closeBtn.addEventListener('click', function () {
        modal.classList.add('hidden');
        mask.classList.add('hidden');
    });
}
// 中項目
middleItemSelectBox.onclick = event => {
    var modal = document.getElementById('modalMiddleItemSelet');
    var mask = document.getElementById('maskMiddleItemSelect');
    modal.classList.remove('hidden');
    mask.classList.remove('hidden');

    var closeBtn = document.getElementById('closeMiddleItemSelectBoxBtn');
    closeBtn.addEventListener('click', function () {
        modal.classList.add('hidden');
        mask.classList.add('hidden');
    });
}
// 小項目
minorItemSelectBox.onclick = event => {
    var modal = document.getElementById('modalMinorItemSelet');
    var mask = document.getElementById('maskMinorItemSelect');
    modal.classList.remove('hidden');
    mask.classList.remove('hidden');

    var closeBtn = document.getElementById('closeMinorItemSelectBoxBtn');
    closeBtn.addEventListener('click', function () {
        modal.classList.add('hidden');
        mask.classList.add('hidden');
    });
}

// 大項目が選定されたら
function SelectedMajorWorkItem(id, dipData) {

    //　選択された大作業項目のIDをpostTmpFormにセットする
    document.getElementById("TmpFormMajorWorkItemId").value = id;
    // 表示用divボックスにセットする
    document.getElementById("choiseMajorItemName").textContent = dipData + " (大項目)";

    // 大項目が選定されたら、ワークボタンを活性化する
    workBtn.disabled = false;

    // 小作業項目のプルダウンを非表示にする => ■レクトボックスではなく新しいDivの方に変更する
    //document.getElementById("selectMinorWorkItemdiv").style.display = 'none';
    minorItemSelectBox.style.display = 'none';
    // ボタンの上の空白を非表示にする
    document.getElementById("workBtnSpace").style.display = 'none';

    // 中作業項目と小作業項目のIDが格納されるTmpFormを空にする
    document.getElementById("TmpFormMiddleworkItemId").value = '';
    document.getElementById("TmpFormMinorworkItemId").value = '';

    // 中作業項目と小作業項目の表示をリセットする
    document.getElementById("choiseMiddleItemName").textContent = '中項目を選択';
    document.getElementById("choiseMinorItemName").textContent = '小項目を選択';

    // 中作業項目のデータを取得してセットする
    this.getAllMiddleWorkItem(id);

    // 中作業項目のプルダウンを表示させる
    document.getElementById("middleItemSelectBox").style.display = '';

    // モーダルの表示を終了する
    var modal = document.getElementById('modalMajorItemSelet');
    var mask = document.getElementById('maskMajroItemSelect');
    modal.classList.add('hidden');
    mask.classList.add('hidden');
}

/* 
====================
モーダル（中項目用）
====================
*/

// 中項目が選定されたら
function SelectedMiddleWorkItem(id, dipData) {
//    // 選択された中作業項目のID
//    var middleworkItemId = selectMiddleWorkItem.value;

//    // プルダウンから選択された中作業項目のIDをTmpFormにセットする
//    document.getElementById("TmpFormMiddleworkItemId").value = middleworkItemId;
//    // 小作業項目のIDが格納されるTmpFormを空にする
//    document.getElementById("TmpFormMinorworkItemId").value = '';

//    // 小作業項目のデータを取得してプルダウンにセットする
//    this.getAllMinorWorkItem(middleworkItemId);
//    // 小作業項目のプルダウンを表示させる
//    document.getElementById("selectMinorWorkItemdiv").style.display = '';

    // 選択された中作業項目のIDをpostTmpFormにセットする
    document.getElementById("TmpFormMiddleworkItemId").value = id;
    // 表示用divボックスにセットする
    document.getElementById("choiseMiddleItemName").textContent = dipData + " (中項目)";
    // 小作業項目のIDが格納されるTmpFormを空にする
    document.getElementById("TmpFormMinorworkItemId").value = '';
    // 小作業項目の表示をリセットする
    document.getElementById("choiseMinorItemName").textContent = '小項目を選択';
    // 小作業項目のデータを取得して選択リストへセットする
    this.getAllMinorWorkItem(id);
    // 小作業項目のプルダウンを表示させる
    document.getElementById("minorItemSelectBox").style.display = '';
    // ボタンの上の空白を表示させる
    document.getElementById("workBtnSpace").style.display ='';

    // モーダルの表示を終了する
    var modal = document.getElementById('modalMiddleItemSelet');
    var mask = document.getElementById('maskMiddleItemSelect');
    modal.classList.add('hidden');
    mask.classList.add('hidden');
}


/* 
====================
モーダル（小項目用）
====================
*/
// 小項目が選定されたら
function SelectedMinorWorkItem(id, dipData) {
    // ■選択された中作業項目のIDをpostTmpFormにセットする
    document.getElementById("TmpFormMinorworkItemId").value = id;
    // 表示用divボックスにセットする
    document.getElementById("choiseMinorItemName").textContent = dipData + " (小項目)";

    // モーダルの表示を終了する
    var modal = document.getElementById('modalMinorItemSelet');
    var mask = document.getElementById('maskMinorItemSelect');
    modal.classList.add('hidden');
    mask.classList.add('hidden');
}
