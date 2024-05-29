"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment").build();

connection.on("RecieveMessage", function (userId, imgUrl, message,date) {
    console.log("Recieving is working")
    addToMessageList(userId, imgUrl, message, date);

    const myDiv = document.getElementById('messageBox');
    setTimeout(function () {
        myDiv.scrollTop = myDiv.scrollHeight;
    }, 10);
    console.log(`${userId}  ${imgUrl}   ${message}  ${date}`)
});

connection.start().then(function () {
    var itemId = document.getElementById("itemId").textContent
    console.log(itemId);
    connection.invoke("JoinToGroup",itemId, itemId)
}).catch(function (error) {
    console.log(error)
});

//<div class="d-flex p-2 border-bottom position-relative">
//    <div class="position-absolute d-flex gap-1" style="top:3px;left:68px">
//        <a class="text-white userInput card-link p-0 bg-transparent" asp-controller="Account" asp-action="Profile" asp-route-userName="@Model.User.UserName">@Model.User.UserName</a>
//        <p> • @DateTime.Now.ToString("dd-mm-yyyy")</p>
//    </div>
//    <img src="/Images/github.jpg" class="rounded-circle" style="height:45px;width:45px" />
//    <p class="text-start p-3 mt-2"></p>
//</div>

function addToMessageList(userName, imgPath, message, date) {
    console.log(`username: ${userName}`)
    console.log(`imgPath: ${imgPath}`)
    console.log(`message: ${message}`)
    console.log(`date: ${date}`)
    var html = ''
    html += `<div class="d-flex p-2 border-bottom position-relative">`;
    html += `<div class="position-absolute d-flex gap-1" style="top:3px;left:68px">`;
    html += `<a class="text-white userInput card-link p-0 bg-transparent" href="https://localhost:7280/account/profile?userName=${userName}" asp-controller="Account" asp-action="Profile" asp-route-userName="${userName}">${userName}</a>`;
    html += `<p> • ${date}</p>` + `</div>`;
    html += `<img src="${imgPath}" class="rounded-circle" style="height:45px;width:45px" />`;
    html += `<p class="text-start p-3 mt-2">${message}</p>` + `</div>`;

    var messageBox = document.getElementById("messageBox")

    messageBox.innerHTML += html;
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("message").value;
    var userName = document.getElementById("userUserName").textContent;
    var img = document.getElementById("userImgPath");
    var imgPath = "/Images/github.jpg";
    var date = new Date().toLocaleDateString()
    if (img != null) {
        imgPath = img.getAttribute("src");
    }
    //addToMessageList(userName, imgPath, message, date)
    var itemId = document.getElementById("itemId").textContent

    connection.invoke("SendComment", userName, imgPath, itemId, message,date).catch((ex) => {
        console.log(ex)
    })
})