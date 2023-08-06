"use strict";


var novKorisnik = localStorage.getItem('Korisnik');
//console.log("prikazi na index "+novKorisnik);
var kor2 = JSON.parse(novKorisnik);
console.log( kor2);
//var Korisnik = sessionStorage.getItem('Korisnik');
/*console.log("udje i tu");
Korisnik = JSON.parse(sessionStorage.getItem("Korisnik"));
//console.log(Korsnik + "treba");*/
if(kor2.fleg==='ne'){
    //nije prijavljen korisnik
    document.getElementById('logoutclass').style.display="none"; //odjavi se dugme se ne vidi kad korisnik nije prijavljen
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
            var imeprez = kor2.ime + " " +kor2.prezime;
            document.getElementById('imeprezime').innerHTML=imeprez;
            document.getElementById('emailkor').innerHTML=kor2.email;
            var svidogadjaji = "<p>Dogadjaji za koje je korisnik oznacio da je zainteresovan: </p>" //+ kor2.dogadjaji;
            kor2.dogadjaji.forEach(d => {
                var datumvreme=d.day+"."+d.month+"."+d.year+" "+d.hours+":"+d.minutes;
                svidogadjaji+="\n<p>Naziv: " + d.naziv + ", mesto: " + d.mesto + ", cena karte: " + d.cenaKarte + ",datum i vreme: "+datumvreme+ ";</p>"+"\n";//JSON.stringify(d) + " \n";
            });
            console.log(svidogadjaji);
            
            document.getElementById('podacikor').innerHTML=svidogadjaji;
            console.log(kor2.dogadjaji);

        }
        else{
            if(kor2.tip==='Organizator'||kor2.tip==='organizator'||kor2.tip==='Administrator'||kor2.tip==='administrator'){
                //ako je organizator ili admin
                document.getElementById('loginclass').style.display="none"; 
                document.getElementById('registerclass').style.display="none"; 
                //document.getElementById('dodajdogadjajbtn').style.display="initial"; 
                var imeprez = kor2.ime + " " +kor2.prezime;
                document.getElementById('imeprezime').innerHTML=imeprez;
                document.getElementById('emailkor').innerHTML=kor2.email;
                var svidogadjaji = "<p>Dogadjaji za koje je korisnik oznacio da je zainteresovan: </p>" //+ kor2.dogadjaji;
                kor2.dogadjaji.forEach(d => {
                    var datumvreme=d.day+"."+d.month+"."+d.year+" "+d.hours+":"+d.minutes;
                    svidogadjaji+="\n<p>Naziv: " + d.naziv + ", mesto: " + d.mesto + ", cena karte: " + d.cenaKarte + ", datum i vreme: "+datumvreme+ ";</p>"+"\n";//JSON.stringify(d) + " \n";
                
                });
                console.log(svidogadjaji);
                document.getElementById('podacikor').innerHTML=svidogadjaji;
                console.log(kor2.dogadjaji);
               /* var svidogadjajiorg = "<p>Ja sam organizator, ovo su moji organizovani dogadjaji: </p>"+
                "<p>Filmovi:</p>";//+ kor2.orgkoncerti + " "+kor2.orgfilmovi+" "+kor2.orgpredstave+" "+ kor2.orgutakmice+" "+kor2.orgostali;
                kor2.orgfilmovi.forEach(d => {
                    //var datumvreme=d.day+"."+d.month+"."+d.year+" "+d.hours+":"+d.minutes;
                    svidogadjajiorg+="\n<p>Naziv: " + d.naziv + ", mesto: " + d.mesto + ", cena karte: " + d.cenaKarte + ";</p>"+"\n";//JSON.stringify(d) + " \n";
                
                });
                svidogadjajiorg+="<p>Koncerti:</p>";//+ kor2.orgkoncerti + " "+kor2.orgfilmovi+" "+kor2.orgpredstave+" "+ kor2.orgutakmice+" "+kor2.orgostali;
                kor2.orgkoncerti.forEach(d => {
                    //var datumvreme=d.day+"."+d.month+"."+d.year+" "+d.hours+":"+d.minutes;
                    svidogadjajiorg+="\n<p>Naziv: " + d.naziv + ", mesto: " + d.mesto + ", cena karte: " + d.cenaKarte + ";</p>"+"\n";//JSON.stringify(d) + " \n";
                
                });
                "<p>Predstave:</p>";//+ kor2.orgkoncerti + " "+kor2.orgfilmovi+" "+kor2.orgpredstave+" "+ kor2.orgutakmice+" "+kor2.orgostali;
                kor2.orgpredstave.forEach(d => {
                    //var datumvreme=d.day+"."+d.month+"."+d.year+" "+d.hours+":"+d.minutes;
                    svidogadjajiorg+="\n<p>Naziv: " + d.naziv + ", mesto: " + d.mesto + ", cena karte: " + d.cenaKarte + ";</p>"+"\n";//JSON.stringify(d) + " \n";
                
                });
                "<p>Utakmice:</p>";//+ kor2.orgkoncerti + " "+kor2.orgfilmovi+" "+kor2.orgpredstave+" "+ kor2.orgutakmice+" "+kor2.orgostali;
                kor2.orgutakmice.forEach(d => {
                    //var datumvreme=d.day+"."+d.month+"."+d.year+" "+d.hours+":"+d.minutes;
                    svidogadjajiorg+="\n<p>Naziv: " + d.naziv + ", mesto: " + d.mesto + ", cena karte: " + d.cenaKarte + ";</p>"+"\n";//JSON.stringify(d) + " \n";
                
                });
                "<p>Ostali:</p>";//+ kor2.orgkoncerti + " "+kor2.orgfilmovi+" "+kor2.orgpredstave+" "+ kor2.orgutakmice+" "+kor2.orgostali;
                kor2.orgostali.forEach(d => {
                    //var datumvreme=d.day+"."+d.month+"."+d.year+" "+d.hours+":"+d.minutes;
                    svidogadjajiorg+="\n<p>Naziv: " + d.naziv + ", mesto: " + d.mesto + ", cena karte: " + d.cenaKarte +  ";</p>"+"\n";//JSON.stringify(d) + " \n";
                
                });
                document.getElementById('organizovani').innerHTML=svidogadjajiorg;
                console.log(kor2.dogadjajiorg);*/
            }

        }
        
    }
}

document.getElementById("logoutclass").addEventListener("click", vratiNaDefault());


