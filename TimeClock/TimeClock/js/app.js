﻿var clock = document.getElementById('clock');
var hexColor = document.getElementById('hex-color');


function hexClock() {
    var time = new Date();
    var hours = (time.getHours() % 12).toString();
    var minutes = time.getMinutes().toString();
    var seconds = time.getSeconds().toString();

    if (hours.length < 2) {
        hours = '0' + hours;
    }

    if (minutes.length < 2) {
        minutes = '0' + minutes;
    }

    if (seconds.length < 2) {
        seconds = '0' + seconds;
    }

    var clockStr = hours + ' : ' + minutes + ' : ' + seconds;
    var hexColorStr = '#' + hours + minutes + seconds;

    if (clock)
        clock.textContent = clockStr;
    if (hexColor)
        hexColor.textContent = hexColorStr;
    if (document.body)
        document.body.style.backgroundColor = hexColorStr;
}

hexClock();
setInterval(hexClock, 1000);