async function GetEmailByName(name) {
  let output;
  var settings = {
    url: "https://localhost:7069/api/Account/" + name + "",
    method: "GET",
    timeout: 0,
  };
  await $.ajax(settings).done(function (response) {
    output = response;
  });
  return output;
}

function Hide(id) {
  document.getElementById(id).style.display = "none";
}
function Show(id) {
  document.getElementById(id).style.display = "block";
}

function DisplayMessageSender(msg) {
  var li = document.createElement("li");
  var span = document.createElement("p");
  span.textContent = msg;
  span.style.color = "white";
  span.style.backgroundColor = "#1E90FF";
  span.style.borderRadius = "30px";
  li.append(span);
  li.style.width = "100%";
  li.style.display = "flex";
  li.style.justifyContent = "flex-end";
  return li;
}

async function DisplayMessageReceiver(msg) {
  var arr = msg.split(":");
  var li = document.createElement("li");
  var img = document.createElement("img");
  var span = document.createElement("p");
  var src = await GetImgByEmail(arr[0]);
  if (src != null) img.src = "http://127.0.0.1:11111/wwwroot/images/" + src;
  else img.src = "http://127.0.0.1:11111/wwwroot/images/avatarEmpty.webp";
  span.textContent = arr[1];
  span.style.backgroundColor = "#DCDCDC";
  span.style.borderRadius = "30px";
  span.style.marginLeft = "8px";
  img.style.width = "25px";
  img.style.height = "25px";
  img.style.borderRadius = "50%";
  li.style.width = "100%";
  li.style.display = "flex";
  li.append(img);
  li.append(span);
  return li;
}

function CheckShow(id) {
  if (document.getElementById(id).style.display == "block") return true;
  else return false;
}

function SetStatus() {
  var i = document.createElement("i");
  i.className = "fa fa-circle";
  i.style.color = "green";
  return i;
}

function SetPositionScrollBar(id) {
  var messageClient = document.querySelector(id);
  messageClient.scrollTop =
    messageClient.scrollHeight - messageClient.clientHeight;
}

async function GetAllGroup() {
  var arr;
  var settings = {
    url: "https://localhost:7069/api/Group/",
    method: "GET",
    timeout: 0,
  };

  await $.ajax(settings).done(function (response) {
    arr = response;
  });
  return arr;
}

async function SearchUserAndGroup(event, listDivGroup) {
  var PATTERN = event.target.value;
  if (PATTERN) {
    var newArr = listDivGroup.filter(function (el) {
      return el
        .getElementsByTagName("p")[0]
        .innerHTML.toString()
        .toLowerCase()
        .includes(PATTERN);
    });
    listDivGroup.forEach((element) => {
      newArr.forEach((item) => {
        if (item != element) element.style.display = "none";
      });
    });
  } else {
    listDivGroup.forEach((elem) => {
      elem.style.display = "block";
    });
  }
}

let x = 0;

async function handleScrollMessage(id, name) {
  x += 0.4;
  console.log(x.toFixed());
  var arr = await GetMessageByGroupID(id, x.toFixed());
  await showMessageGroup(arr, name, "listMessageGroup");
  //return arr;
}

function ClickSearchUserByEmail(myEmail) {
    var divInsert = document.createElement("div");
    divInsert.id = "search-area";
    divInsert.style.display = "inline-block";
    var label = document.createElement("label");
    label.textContent = "To: ";
    label.style.marginRight = "5px";
    var input = document.createElement("input");
    document.getElementById("titleOfGroup").style.display = "none";
    var chatGroup = document.getElementById("ChatGroup");
    input.placeholder = "Enter name user";
    input.style.border = "none";
    input.id = "inputSearchUserByEmail";
    var div = document.createElement("div");
    div.id = "searchContent";
    //input.setAttribute('onchange', 'CaptureSearchUserByEmail(event, " ' + myEmail + ' ")');
    input.addEventListener("focusout", function (event) {
        document.getElementById('searchContent').innerHTML='';
    });

    /*input.addEventListener("focusin", function (event) {
        CaptureSearchUserByEmail(event, myEmail);
    });*/

    input.addEventListener("input", function (event) {
        event.preventDefault();
        CaptureSearchUserByEmail(event, myEmail);
    });

  divInsert.appendChild(label);
  divInsert.appendChild(input);
  divInsert.appendChild(div);
  if (chatGroup.firstChild.id != "titleOfGroup") {
    chatGroup.firstChild.remove();
  }

  chatGroup.insertBefore(divInsert, chatGroup.firstChild);
}

async function CaptureSearchUserByEmail(event, email) {
  var body = document.getElementById("ChatGroup");
  //var tempBody = document.getElementById("tempBody");
  //if (tempBody) {
  //  body.removeChild(tempBody);
  //}
  var arrAccount = await GetAllAcc();
  console.log(arrAccount);
  var PATTERN = event.target.value;
  var arrName = arrAccount.filter(function (el) {
    return el.fullName.toLowerCase().includes(PATTERN);
  });
  var tempBody = document.getElementById("searchContent");
  tempBody.innerHTML = "";

  for (let i = 0; i < arrName.length; i++) {
    var div = await CreateTempGroup(
      arrName[i].fullName,
      arrName[i].email,
      arrName[i].avatar,
      arrName[i].email,
      email
    );
    div.style.marginTop = "5px";
    tempBody.style.overflowY = "scroll";
    tempBody.append(div);
  }
  body.style.position = "relative";
  /*for (var i = body.children.length; i >= 2; i--) {
        body.removeChild(body.children[i]-1);
    }
    body.removeChild("")*/
  tempBody.style.position = "absolute";
  body.insertBefore(tempBody, body.children[2]);
}

function RemoveDivNullId() {
  document.getElementById("bottomOfGroup").style.display = "flex";

  var arrayGroup = document.getElementById("selectListGroup");
  var arrayDivGroup = Array.from(arrayGroup.children);
  console.log(arrayDivGroup);
  for (let i = 0; i < arrayDivGroup.length; i++) {
    if (arrayDivGroup[i].id == "null") arrayDivGroup[i].remove();
    console.log(arrayDivGroup[i].id);
  }
}

function RemoveAllTempSelect() {
  if (document.getElementById("tempBody")) {
    document.getElementById("tempBody").style.display = "none";
    document.getElementById("tempBody").innerHTML = "";
  }
}
