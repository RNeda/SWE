namespace Models;

[Table("Utakmice")]
public class Utakmice : Dogadjaji
{
    
    public  string? Sport {get; set;}

    public  string? Klub1 {get; set;}

    public  string? Klub2 {get; set;}

    public  int BrMesta {get; set;}
    
  
}