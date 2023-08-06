
async function DodajDogadjaj(){

    let nazivd = document.getElementById('nazivd');
    let naziv= nazivd.value;
    let datumvremed = document.getElementById('datumvremed');
    let datumvreme= datumvremed.value;
    let mestod = document.getElementById('mestod');
    let mesto =mestod.value;
    let cenau=document.getElementById('cenaulazniced');
    let cena=cenau.value;
    let tipp= document.getElementById('tip');
    let tip=tipp.value;
    console.log(datumvreme);
    if(naziv!="" && datumvreme!="" && mesto!="" && cena!="" && tip!=""){
    if(!isNaN(cena)){
    if(tip=="Filmovi"){
        let opiss=document.getElementById('kratakopisfilmd');
        let opis=opiss.value;
        let glumcii=document.getElementById('glumcifilmd');
        let glumci=glumcii.value;
        let trajanjee=document.getElementById('trajanjeumind');
        let trajanje=trajanjee.value;
        let zanrr=document.getElementById('zanrd');
        let zanr=zanrr.value;
        let brm=document.getElementById('brmestafilmd');
        let br=brm.value;

        let korisnik=localStorage.getItem('Korisnik')
        var kor2 = JSON.parse(korisnik);
        let headers = new Headers();
        headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
        
        if(opis!=""&& glumci!="" && trajanje!="" && !isNaN(trajanje) && br!=" "&& !isNaN(br))
        {
         await fetch(`http://localhost:5016/Filmovi/UpisiFilm/${kor2.id}/${naziv}/${datumvreme}/${mesto}/${opis}/${glumci}/${trajanje}/${zanr}/${br}/${cena}`,
                                  {method:"POST"});

         alert("Uspesno dodat film");
                                  
        window.location.href="events.html";
        }
        else{
            alert("Sva polja moraju biti popunjena i trajanje i broj mesta moraju biti brojevi ");
        }

    }
    if(tip=="Predstave"){
        let opiss=document.getElementById('kratakopispredstavad');
        let opis=opiss.value;
        let glumcii=document.getElementById('glumcipredstavad');
        let glumci=glumcii.value;
        let brm=document.getElementById('brmestapredstavad');
        let br=brm.value;

        let korisnik=localStorage.getItem('Korisnik')
        var kor2 = JSON.parse(korisnik);
        let headers = new Headers();
        if(naziv){
        headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
        if(opis!=""&& glumci!="" && br!="" &&!isNaN(br))
        {
        await fetch(`http://localhost:5016/Predstave/UpisiPredstavu/${kor2.id}/${naziv}/${datumvreme}/${mesto}/${opis}/${glumci}/${br}/${cena}`,
                                  {method:"POST"});
                                
                                  alert("Uspesno dodata predstava");
                                  
                                  window.location.href="events.html";
       }
     else{
            alert("Sva polja moraju biti popunjena i  broj mesta mora biti broj ");
        }
    }
    }
    if(tip=="Koncerti"){
        let izvodjacc=document.getElementById('izvodjacd');
        let izvodjac=izvodjacc.value;
        let gostii=document.getElementById('gostikoncertd');
        let gosti=gostii.value;
        let brm=document.getElementById('brmestakoncertd');
        let br=brm.value;

        let korisnik=localStorage.getItem('Korisnik')
        var kor2 = JSON.parse(korisnik);
        let headers = new Headers();
        headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
       
        if(izvodjac!=""&& gosti!="" && br!=" "  && !isNaN(br))
        {
        await fetch(`http://localhost:5016/Koncerti/UpisiKoncert/${kor2.id}/${naziv}/${datumvreme}/${mesto}/${izvodjac}/${gosti}/${br}/${cena}`,
                                  {method:"POST"});
             alert("Uspesno dodat koncert");
                                  
             window.location.href="events.html";
        }
     else{
            alert("Sva polja moraju biti popunjena i broj mesta mora biti broj ");
     }

    }
    if(tip=="Sport"){
        let sportt=document.getElementById('sportd');
        let sport=sportt.value;
        let klubb1=document.getElementById('klub1d');
        let klub1=klubb1.value;
        let klubb2=document.getElementById('klub2d');
        let klub2=klubb2.value;
        let brm=document.getElementById('brmestasportd');
        let br=brm.value;

        let korisnik=localStorage.getItem('Korisnik')
        var kor2 = JSON.parse(korisnik);
        let headers = new Headers();
        headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
        if(sport!=""&& klub1!="" && klub2!=""&& br!="" && !isNaN(br))
        {
        await fetch(`http://localhost:5016/Utakmice/UpisiUtakmicu/${kor2.id}/${naziv}/${datumvreme}/${mesto}/${sport}/${klub1}/${klub2}/${br}/${cena}`,
                                  {method:"POST"});
                                  alert("Uspesno dodato");
                                  window.location.href="events.html";
     }
     else{
           alert("Sva polja moraju biti broj mesta mora biti broj ");
        }
    }
    if(tip=="Ostalo"){
        let opiss=document.getElementById('kratakopisostalod');
        let opis=opiss.value;
        let trajedoo=document.getElementById('trajedod');
        let trajedo=trajedoo.value;
        

        let korisnik=localStorage.getItem('Korisnik')
        var kor2 = JSON.parse(korisnik);
        let headers = new Headers();
        headers.append('Access-Control-Allow-Origin', 'http://localhost:5016');
        if(opis!="")
        {
        await fetch(`http://localhost:5016/OstaliDogadjaji/UpisiOstalidogadjaj/${kor2.id}/${naziv}/${datumvreme}/${mesto}/${opis}/${trajedo}/${cena}`,
                                  {method:"POST"});
                 alert("Uspesno dodato");
                                  
                  window.location.href="events.html";
         }
        else{
                 alert("Sva polja moraju biti popunjena i broj mesta mora biti broj ");
         }

    }  
    }
    else
    {
        alert("Cena mora da bude broj");
    }
    }
    else{
        alert("Popunite sva polja!");
    }
}