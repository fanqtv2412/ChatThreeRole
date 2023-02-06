async function getNameByEmail(email) {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Account/GetNameByEmail/" + email,
        "method": "Get",
        "timeout": 0
    };
    await $.ajax(settings).done(function (response) {
        res = response;
    });
    return res;
}

async function GetAllAcc() {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Account/",
        "method": "GET",
        "timeout": 0
    };
    await $.ajax(settings).done(function (response) {
        res = response;
    });
    return res;
}

async function GetAllGroupRegistered(email) {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Account/GetAccountByEmail?email=" + email,
        "method": "GET",
        "timeout": 0
    };
    await $.ajax(settings).done(function (response) {
        res = response;
    });
    //let arr = new Array();
    //res.groups.forEach(element => {
    //    arr.push(element.groupName);
    //});
    return res;
}

async function GetImgByEmail(email) {
    let res;
    var settings = {
        "url": "https://localhost:7069/api/Account/GetAccountByEmail?email=" + email.trim(),
        "method": "GET",
        "timeout": 0
    };
    await $.ajax(settings).done(function (response) {
        if (response)
            res = response.avatar;
        else res = null;
    });

    return res;
}

async function AddGroupToAccount(groupID, email) {
    var settings = {
        "url": "https://localhost:7069/api/Account/addgrouptoaccount",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "GroupId": groupID,
            "UserEmail": email
        }),
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
    });
}

