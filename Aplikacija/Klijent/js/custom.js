// const button1=document.getElementById("reset")
// button2.onclick=function() {
// }

function clearAll() {
    document.getElementById("filmoviInputs").style.display="none";
    document.getElementById("predstaveInputs").style.display="none";
    document.getElementById("koncertiInputs").style.display="none";
    document.getElementById("sportInputs").style.display="none";
    document.getElementById("ostaloInputs").style.display="none";
}


function filmoviInputs() {
     clearAll();
     document.getElementById("filmoviInputs").style.display="block";
 }

function predstaveInputs() {
    clearAll();
    document.getElementById("predstaveInputs").style.display="block";
}

function koncertiInputs() {
    clearAll();
    document.getElementById("koncertiInputs").style.display="block";
}

function sportInputs() {
    clearAll();
    document.getElementById("sportInputs").style.display="block";
}

function ostaloInputs() {
    clearAll();
    document.getElementById("ostaloInputs").style.display="block";
}


function update() {
    var select = document.getElementById('tip');
    var option = select.options[select.selectedIndex];

    // document.getElementById('value').value = option.value;
    
    switch (option.value) {
        case 'Filmovi':
            filmoviInputs();
            break;
         case 'Predstave':
            predstaveInputs();
            break;
        case 'Koncerti':
            koncertiInputs();
            break;
        case 'Sport':
            sportInputs();
            break;
        case 'Ostalo':
            ostaloInputs();
            break;
    
        default:
            break;
    }



    //  alert(option.text);
}

update();