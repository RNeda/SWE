namespace Models;
[Table("Koncerti")]
public class Koncerti : Dogadjaji
{
    
    public  string? Izvodjac {get; set;}

    public  int BrMesta {get; set;}

    public List<Gosti>? Gosti {get; set;}
    
    
}