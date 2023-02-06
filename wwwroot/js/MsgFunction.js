async function showMessageGroup(arr, name, idElem) {
    for(let i =0; i<arr.length; i++){
        var msg = arr[i].messsage.split(':');
        if (msg[0] == name) {
            var li = DisplayMessageSender(msg[1]);
            document.getElementById(idElem).appendChild(li);
        }
        else {
            var li = await DisplayMessageReceiver(arr[i].messsage);
            document.getElementById(idElem).appendChild(li);
        }
    }
}

async function SendClientFunction(Name) {
    constEmail = await GetEmailByName(document.getElementById("NameClient").innerText);
    var msg = $("#messageToClient").val();
    if (msg && constEmail) {
        console.log(msg + constEmail);
        connection.invoke("SendPrivateMessage", constEmail, Name + ":" + msg);
        console.log(msg);
        var li = DisplayMessageSender(msg);
        document.getElementById("listMessageClient").appendChild(li);
        document.getElementById("messageToClient").value = "";
    }
}

async function SendMessageToGroupFunc(constGroupName, Name, constGroupID, email) {
    if(document.getElementById(constGroupID) == null){
        var selectList = document.getElementById('selectListGroup');
        var firstChild = selectList.getElementsByTagName('div')[0];
        firstChild.id = constGroupID;
        console.log(firstChild);

    }
    var mess = $("#messageToGroup").val();
    if (mess) {
        console.log(constGroupName + constGroupID + email + mess);
        var tempDiv = document.getElementById(constGroupID);
        var tempImg = tempDiv.getElementsByTagName('img')[0].src;
        await connection.invoke("SendMessageToGroup", constGroupName.trim(), email + ": " + mess, constGroupID);
        var li = DisplayMessageSender(mess);
        document.getElementById("listMessageGroup").appendChild(li);
        document.getElementById("messageToGroup").value = "";
        SaveMesageGroup(constGroupID, email + ":" + mess, parseDate());
        document.getElementById(constGroupID).remove();
        var div = await CreateGroup(constGroupName, constGroupID, tempImg, email + ": " + mess, Date.now(), email);
        var body = document.getElementById("selectListGroup");
        body.insertBefore(div, body.firstChild);
        SetPositionScrollBar('#listMessageGroup');
    }
}

function parseDate() {
    const thisTime = new Date();
    const yyyy = thisTime.getFullYear();
    let mm = thisTime.getMonth() + 1;
    let dd = thisTime.getDate();
    let hh = thisTime.getHours();
    let m = thisTime.getMinutes();
    let ss = thisTime.getSeconds();
    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;
    if (hh < 10) hh = '0' + hh;
    if (m < 10) m = '0' + m;
    if (ss < 10) ss = '0' + ss;
    return yyyy + "-" + mm + "-" + dd + "T" + hh + ":" + m + ":" + ss;
}

async function GetMessageByGroupID(id, scroll = 0) {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Message/" + id + "/" + scroll,
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        }
    };
    await $.ajax(settings).done(async function (response) {
        res = response;
    });
    return res;
}

function SaveMesageGroup(groupid, message, dateOfMsg) {
    var settings = {
        "url": "https://localhost:7069/api/Message",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "messsage": message,
            "groupID": groupid,
            "dateOfMsg": dateOfMsg
        }),
    };
    $.ajax(settings).done(function (response) {
    });
}

async function GetLastMessage(groupID) {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Message/lastmessage?id=" + groupID,
        "method": "GET",
        "timeout": 0,
    };
    await $.ajax(settings).done(function (response) {
        if(response == null) res = {
            "id": null,
            "messsage": "Start new conversation",
            "groupID": groupID,
            "group": null,
            "dateOfMsg": null
        };
        else
            res = response;
    });
    return res;
}
async function GetAllLastMessage(listGroup){
    var arr = new Array();
    for(const item of listGroup){
        const msg = await GetLastMessage(item.id);
        if(msg)
            arr.push(msg);
        else{
            //msg.messsage = "Let start conversation";
            arr.push(msg);
        } 
    }
    return arr;
}

function HandleMsgGroup(msg){
    if(msg.indexOf(':') > -1){
        var arr = msg.split(":");
        var name = arr[0].split(' ').pop();
        return name + ":" + arr[1];
    }
    else return msg;
   
}