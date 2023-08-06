"use strict";
var novKorisnik = localStorage.getItem('Korisnik');
console.log("prikazi na index "+novKorisnik);
var kor2 = JSON.parse(novKorisnik);
console.log(kor2);
//var Korisnik = sessionStorage.getItem('Korisnik');
/*console.log("udje i tu");
Korisnik = JSON.parse(sessionStorage.getItem("Korisnik"));
//console.log(Korsnik + "treba");*/
if(kor2.fleg==='ne'){
    //nije prijavljen korisnik
    //document.getElementById('dodajdogadjajbtn').style.display="none";
    document.getElementById('mojnalog').style.display="none";
}
else{
    if(kor2.fleg==='da'){
        //korinsik je prijavljen, zavisno od tipa prikazujemo elemente
        if(kor2.tip==='Korisnik'||kor2.tip==='korisnik'){
            //ako je obican korisnik
            //window.location.href="index.html";  //kad se uloguje vodi ga na index.html, poziva se iz login.js, odavde ne radi kako treba

            //sklanja dodaj dogadjaj dugme, uloguj se i registruj se--valjda samo ovo
            //document.getElementById('dodajdogadjajbtn').style.display="none";
            document.getElementById('loginclass').style.display="none"; 
            document.getElementById('registerclass').style.display="none"; 

        }
        else{
            if(kor2.tip==='Organizator'||kor2.tip==='organizator'||kor2.tip==='Administrator'||kor2.tip==='administrator'){
                //ako je organizator ili admin
                document.getElementById('loginclass').style.display="none"; 
                document.getElementById('registerclass').style.display="none"; 
                //document.getElementById('dodajdogadjajbtn').style.display="initial"; 
            }
        }
    }
}
/*
function PrikaziNaIndex(){
    var el = document.getElementById('loginclass');
    console.log(el);
}*/