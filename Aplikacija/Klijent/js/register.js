"use strict";
async function Registruj(){
    let email = document.getElementById('mejlreg');
    let emailstring = email.value;
    let password = document.getElementById('sifrareg');
    let passwordstring = password.value;
    let ime = document.getElementById('imereg').value;
    let prezime = document.getElementById('prezimereg').value;
    let potvrdi = document.getElementById('potvrdireg').value;
    document.getElementById('mojnalog').style.display="none"; //ne radi?
    console.log(emailstring);
    console.log(passwordstring);
    if(passwordstring!==potvrdi){
        alert("nije dobra potvrda sifre");
        return;
    }
    
    let headers = new Headers();
    headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
    await fetch(`http://localhost:5016/Korisnici/UpisiKorisnikaNovo/${ime}/${prezime}/${emailstring}/${passwordstring}`,
                                {method:"POST"});
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
        kor2.dogadjaji=user.dogadjaji;
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
        
        window.location.href="index.html";
        
        
    }
}