namespace Models;

[Table("Glumci")]
public class Glumci
{
    [Key]

    public int ID {get; set;}

    public  string? Ime {get; set;}

    public  string? Prezume {get; set;}
    
}