//Create a websocket allows real time connection between front end and backend
const token = document.querySelector("#FinnhubToken").value;
var stockSymbol = document.getElementById("StockSymbol").value;
const socket = new WebSocket('wss://ws.finnhub.io/api/v1/quote?symbol='+stockSymbol+"&"+token);


//connection opened.Subscribe to a symbol
socket.addEventListener('open', function (event) {
    socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stockSymbol }))
});

//Listen(ready to recieve) for messages
socket.addEventListener('message', function (event) {

    //if error message is recieed from server
    if (event.data.type == "error"){
        $(".price").text(event.data.msg);
        return; //exit the function
    }

    var eventData = JSON.parse(event.data);
    if (eventData) {
        if (eventData.data) {
            var updatedPrice = JSON.parse(event.data).data[0].p;

            $(".price").text(updatedPrice.toFixed(2));
        }
    }

});

//unsubscribe 
var unsubscribe = function (symbol) {
    //disconnect from server
    socket.send(JSON.stringify({'type': 'unsubscribe', 'symbol':symbol}))

}

window.onunload = function () {
    unsubscribe(stockSymbol);
};
