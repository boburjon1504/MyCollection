"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/comment").build();

connection.on("ReceiveMessage", function () {
    
});

connection.start().then(function () {
    
}).catch(function (error) {
    console.log(error)
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    
})