//fleg, id, ime, prezime, tip, email, sifra, dogadjaji, orgkoncerti, org, filmovi, orgpredstave, orgutakmice, orgostali
"use strict";

var Korisnik //=['ne','','','','','','','','','','','',''];
 ={
    fleg:'ne',
    id: '',
    ime : '',
    prezime : '',
    tip: '',
    email:'',
    sifra:'',
    dogadjaji: '',
    orgkoncerti:'',
    orgfilmovi:'',
    orgpredstave:'',
    orgutakmice:'',
    orgostali:''
}
//ovako se cuva
console.log(Korisnik);
var kor = JSON.stringify(Korisnik);
console.log(kor);

localStorage.setItem('Korisnik', kor);

//ovako se pribavlja
/*const novKorisnik = localStorage.getItem('Korisnik');
console.log(novKorisnik);
/*
sessionStorage.setItem("Korisnik", JSON.stringify(Korisnik));
console.log("udjetu");


//Korisnik=JSON.parse(sessionStorage.getItem("Korisnik"));
//var Korisnik = localStorage.getItem("Korisnik");*/