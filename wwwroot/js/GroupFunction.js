async function CreateGroup(groupName, id = null, avatar, msg, hoursLater = null, email = null) {
    var titleName;
    if(groupName.indexOf(':') > -1){
        var handleName = groupName.split(':');
        if (handleName[0].trim() == email.trim()) {
            titleName = await getNameByEmail(handleName[1].trim());
        }
        else if (handleName[1].trim() == email.trim()) {
            titleName = await getNameByEmail(handleName[0].trim());
        }
        else titleName = groupName;
        //console.log(groupName + ":" + handleName[0] + ":" + handleName[1]);
        //connection.invoke("AddToGroup", groupName, handleName[0]);
        //connection.invoke("AddToGroup", groupName, handleName[1]);
    }
    else 
        titleName = groupName;
    var div        = document.createElement("div");
    var p          = document.createElement('button');
    var img        = document.createElement('img');
    var divBao     = document.createElement('div');
    var name       = document.createElement('p');
    var divTimeAndContent = document.createElement('div');
    var tempMsg    = document.createElement('span');
    var hour       = document.createElement('span');
    if(avatar){
        if(avatar.indexOf('/') > -1){
            img.src = avatar;
        }
        else
            img.src ="http://127.0.0.1:11111/wwwroot/images/" + avatar;
    }
    else img.src = "http://127.0.0.1:11111/wwwroot/images/avatarEmpty.webp";
    img.style.width        = "40px";
    img.style.height       = "40px";
    img.style.borderRadius = "50%";
    titleName = titleName.length > 25 ?titleName.substring(0,23) + "...": titleName;
    name.textContent = titleName;
    if(hoursLater != null){
        hour.textContent = moment(hoursLater).fromNow();
        hour.style.fontSize = "12px";
    }
    name.style.marginBottom = "inherit";
    if(msg.indexOf(":") > -1){
        var handleEmail_Msg = msg.split(":");
        if(handleEmail_Msg[0].trim() == email.trim()) msg = "You: " + handleEmail_Msg[1];
        else{
            msg = await getNameByEmail(handleEmail_Msg[0].trim()) + ":" + handleEmail_Msg[1];
            msg = HandleMsgGroup(msg);
        }
        if(msg.length + moment(hoursLater).fromNow().length > 28) msg = msg.substring(0,20) + "...";
        else
            msg = msg.length>30? msg.substring(0,27) + "..." : msg;
    }
    tempMsg.textContent        = msg + ".";
    tempMsg.style.marginBottom = "10px";
    tempMsg.style.fontSize     = "12px";

    var timeInterval = setInterval(function(){
        hour.innerText = moment(hoursLater).fromNow();
        if(msg.length + moment(hoursLater).fromNow().length > 28) msg = msg.substring(0,20) + "...";
        tempMsg.innerText = msg + ".";
    }, 60000);
    divTimeAndContent.append(tempMsg);
    divTimeAndContent.append(hour);
    divTimeAndContent.style.display = "flex";
    divTimeAndContent.style.justifyContent = "space-between";
    divTimeAndContent.style.width = "100%";
    divBao.append(name);
    divBao.append(divTimeAndContent);
    divBao.style.marginLeft = "10px";
    divBao.style.width = "100%";
    p.append(img);
    p.append(divBao);
    p.className = "GroupOnline";    
    p.setAttribute('onclick', 'SelectGroup( " '+groupName+' "," '+titleName+' ", "' + id +'","' + img.src + '")');
    p.style.width ="100%";
    p.style.height = "100%";
    p.style.display = "flex";
    div.appendChild(p);
    p.title = id;
    div.style.width = "100%";
    div.style.height = "60px";
    div.id = id;
    return div;
}

async function CreateNewTempGroup(groupName, avatar, msg){

}
async function PostGroup(name) {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Group/",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "groupName": name
        }),
    };
    await $.ajax(settings).done(function (response) {
        res = response;
    });
    return res;
}

async function GetAllGroupNameThroughHub() {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Group/GetThroughHub",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        }
    };
    await $.ajax(settings).done(function (response) {
        res = response;
    });
    return res;
}

function HandleGroupName(nameGroup, name){
    var arr = nameGroup.split(':');
    var res = name==arr[0]?arr[1]:arr[0]; 
    return res;
}
async function GetGroupNameByID(id){
    var res;
    var settings = {
        "url": "https://localhost:7069/api/Group/GetNameByID?id=" + id,
        "method": "GET",
        "timeout": 0
    };
    await $.ajax(settings).done(function(response){
        res = response;
    });
    return res;
}

function countTime(time){
    setInterval(function () {
        return moment(time).fromNow();
    }, 60000);
}

async function CreateTempGroup(nameAccount, id, avatar, email,  myEmail){
    var div    = document.createElement('div');
    var button = document.createElement('button');
    var img    = document.createElement('img');
    var name   = document.createElement('b');
    if(avatar != null){
        if(avatar.indexOf('/') > -1){
            img.src = avatar;
        }
        else
            img.src ="http://127.0.0.1:11111/wwwroot/images/" + avatar;
    }
    else img.src = "http://127.0.0.1:11111/wwwroot/images/avatarEmpty.webp";

    img.style.width        = "30px";
    img.style.height       = '30px';
    img.style.borderRadius = "50%";
    name.textContent       = nameAccount;
    name.style.marginLeft  = "10px";
    button.append(img);
    button.append(name);
    button.style.width     = "100%";
    button.style.borderStyle = "none";
    button.className       = "GroupOnline";
    div.append(button);
    div.style.display      = "flex";
    div.id                 = id;
    div.setAttribute('onclick', 'SelectTempGroup( " ' + nameAccount + ' ", "' + id + '","' + img.src + '"," '+ email + '","' + myEmail + '")'); 
    return div;
}

async function GetImageOfGroupByID(id){
    var res;
    var settings = {
        "url"    : "https://localhost:7069/api/Group/GetImage?id=" + id,
        "method" : "GET",
        "timeout": 0,
      };
      
    await $.ajax(settings).done(function (response) {
        res = response;
    });
    return res;
}
async function GetAllGroup() {
    var res;
    var settings = {
        "url": "https://localhost:7069/api/Group/",
        "method": "GET",
        "timeout": 0,
    };

    await $.ajax(settings).done(function (response) {
        res = response;
    });
    return res;
}
async function CheckExistGroup(nameGroup){
    var res;
    var settings = {
        "url"    : "https://localhost:7069/api/Group/" + nameGroup,
        "method" : "GET",
        "timeout": 0,
      };
      
    await $.ajax(settings).done(function (response) {
        res = response;
    });
    return res;
}


