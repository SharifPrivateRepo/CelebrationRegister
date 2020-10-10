var main = document.getElementById("main");
var userPanel = document.getElementById("user-panel");

window.onresize = () => {
    if (document.body.clientWidth < 992) {
        userPanel.style.display = "block";
        main.style.display = "block";
    } else {
        userPanel.style.display = "flex";
        main.style.display = "flex";
    }
}

window.onload = () => {
    if (document.body.clientWidth < 992) {
        userPanel.style.display = "block";
        main.style.display = "block";
    } else {
        userPanel.style.display = "flex";
        main.style.display = "flex";
    }
}