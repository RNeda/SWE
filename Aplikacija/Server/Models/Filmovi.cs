namespace Models;

 [Table("Filmovi")]
public class Filmovi : Dogadjaji
{
  

    public  List<Glumci>? Glumci {get; set;}

    public string? KratakOpis {get; set;}

    public  int VremeTrajanjaUMin {get; set;}

    public  int BrMesta {get; set;}

    public  string? Zanr {get; set;}
    
    
    
}