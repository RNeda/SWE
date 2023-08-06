//fleg je ne kad korisnik nije prijavljen, a da kad jeste
/*var Korisnik ={
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
}*/
//const fs = require("fs");
//const stringToWrite = "HELLO I AM WRITTEN TO THE FILE";
"use strict";

async function Uloguj(){
    let email = document.getElementById('loginmejl');
    let emailstring = email.value;
    let password = document.getElementById('loginsifra');
    let passwordstring = password.value;
    var elem = document.getElementById('mojnalog').style.display="none";  //ne radi
    console.log(elem);
    console.log(emailstring);
    console.log(passwordstring);
    let headers = new Headers();
    headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
    const korisnik = await fetch(`http://localhost:5016/Korisnici/VratiKorisnika/${emailstring}/${passwordstring}`,
                                {method:"GET"});
    const user = await korisnik.json();
    if(!korisnik.ok){
        alert(user);
        //console.log(korisnik);
        //alert(user);
    }
    else{
        //vrati kako treba
        console.log(user);

        //preuyimamo dogadjaje za koje je zainteresovan
        let headers = new Headers();
        headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
        var dogodinteresa = await fetch(`http://localhost:5016/Korisnici/PreuzmiDogadjajeZaKojeJeKorisnikZainteresovan/${emailstring}`,
                                        {method:"GET"});
        const dogodinteresa2 = await dogodinteresa.json();
        //console.log("dogadjaji korisnika iz baze:");
        console.log(dogodinteresa2);
    
        var novKorisnik = localStorage.getItem('Korisnik');
        console.log("vrati iz local storage" +novKorisnik);
        var kor2 = JSON.parse(novKorisnik);
        console.log(kor2);

        kor2.fleg='da';
        kor2.ime=user.ime;
        kor2.prezime=user.prezime;
        kor2.tip=user.tipKorisnika;
        kor2.email=user.email;
        kor2.sifra=user.sifra;
        kor2.id=user.id;
        kor2.dogadjaji=dogodinteresa2;
        //kor2.dogadjaji=user.dogadjaji;
        kor2.orgfilmovi=user.organizovaniFilmovi;
        kor2.orgkoncerti=user.organizovaniKoncerti;
        kor2.orgostali=user.organizovaniOstaliDogadjaji;
        kor2.orgpredstave=user.organizovanePredstave;
        kor2.orgutakmice=user.organizovaneUtakmice;
        
        console.log(kor2);

        localStorage.removeItem('Korisnik');

        var kor = JSON.stringify(kor2);
        console.log(kor);

        localStorage.setItem('Korisnik', kor);
        
        window.location.href="index.html";  //kad se uloguje vodi ga na index.html

        //var data = JSON.stringify(user);
    //console.log(data);
    //const data = 'This is the content that will be written to the file.';
    //const filename = 'infokorisnik.txt';

    //writeFile(data, filename);
    /*requestFileSystemPermission();
    writeFile(data);
/*
        fs.writeFile("test.txt", user, (err) => {
            if (err) {
                console.error(err);
            return;
              }
            });*/
        /*
        Korisnik.fleg='da';
        Korisnik.ime=user.ime;
        Korisnik.prezime=user.prezime;
        Korisnik.tip=user.tipKorisnika;
        Korisnik.email=user.email;
        Korisnik.sifra=user.sifra;
        Korisnik.id=user.id;
        Korisnik.dogadjaji=user.dogadjaji;
        Korisnik.orgfilmovi=user.organizovaniFilmovi;
        Korisnik.orgkoncerti=user.organizovaniKoncerti;
        Korisnik.orgostali=user.organizovaniOstaliDogadjaji;
        Korisnik.orgpredstave=user.organizovanePredstave;
        Korisnik.orgutakmice=user.organizovaneUtakmice;
        //localStorage.setItem("Korisnik", Korisnik);
        console.log(Korisnik);
        */
            // process DOM elements here
            //let logg = document.getElementsByClassName('loginclass');
            //logg.style.display="none";
            //let regg = document.getElementsByClassName('registerclass');
            //regg.style.display="none";
            
            //let imee = document.getElementById('imeprezime').innerHTML="novoime";
            //console.log(imee);
        /*PrikaziNaIndex();/*
        let regg = document.getElementById('loginclass');
        console.log(regg);*/
        //window.location.href="index.html";  //kad se uloguje vodi ga na index.html
        //treba da se sklone uoguj se i prijavi se dugme - nesto nece :(
        
        //document.getElementById("menu-menu").children[4].style.display="none";
        
        //treba da se na mojnalog pokazu podaci - i ovo nece
       
        //var ime = await document.querySelector(".imeprezime");
        //ime.textContent = user.ime;//document.getElementById('imeprezime'); //ne stigne da ga uhvati jer se skripta izvrsi pre nego se ucitaju sve stranice
        //let imee = user.ime; //dobro
        //console.log(ime);
        //alert(imee);
        //ime.innerHTML=imee;  //ime je null tj ne uhvati element pa ne moze da se postavi innerhtml kad ne uhvati element
        //ime.innerHTML(imee);
        //treba da se u zavisnosti od tipa korisnika prikazu odredjeni delovi
    }
}

