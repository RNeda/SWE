//document.addEventListener("load", vratiNaDefault());
//var odjavisebtn = document.getElementById("logoutclass");
//console.log(odjavisebtn);

function vratiNaDefault(){
    
    var novKorisnik = localStorage.getItem('Korisnik');
    console.log("vrati na default "+novKorisnik);
    var Korisnik = JSON.parse(novKorisnik);
    console.log("vracanje korisnika na default");


    var Korisnik ={
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
    console.log("default korisnik "+Korisnik);
    var kor = JSON.stringify(Korisnik);
    console.log(kor);

    localStorage.setItem('Korisnik', kor);
}