namespace Models;

[Table("Korisnici")]
public class Korisnici
{
    [Key]

    public int ID {get; set;}

    [MaxLength(50)]
    public  string? TipKorisnika {get; set;}

    [MaxLength(50)]
    public  string? Ime {get; set;}

     [MaxLength(50)]
    public  string? Prezime {get; set;}

     [MaxLength(100)]
     [RegularExpression("[a-z0-9]+@[a-z]+\\.[a-z]{2,3}")]
    public  string? Email {get; set;}

     [MinLength(6)]
    public  string? Sifra {get; set;}
   [JsonIgnore]
    public List<Dogadjaji>? Dogadjaji {get; set;}
[JsonIgnore]
    public List<Koncerti>? OrganizovaniKoncerti {get; set;}
    [JsonIgnore]
    public List<Filmovi>? OrganizovaniFilmovi {get; set;}
    [JsonIgnore]
    public List<Predstave>? OrganizovanePredstave {get; set;}
    [JsonIgnore]
    public List<Utakmice>? OrganizovaneUtakmice {get; set;}
    [JsonIgnore]
    public List<OstaliDogadjaji>? OrganizovaniOstaliDogadjaji {get; set;}



}