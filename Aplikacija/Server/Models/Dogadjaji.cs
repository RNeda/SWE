namespace Models;

[Table("Dogadjaji")]
public abstract class Dogadjaji
{
   [Key]
    public int ID {get; set;}

    public  string? Naziv {get; set;}

    public  DateTime DatumVreme {get; set;}

    public  string? Mesto {get; set;}

    public  Korisnici? Organizator{get; set;}
    
    public  double CenaKarte {get; set;}

}