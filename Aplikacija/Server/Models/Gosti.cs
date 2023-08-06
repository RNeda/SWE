namespace Models;

[Table("Gosti")]
public class Gosti 
{
    [Key]

    public int ID {get; set;}

    public string? Ime {get; set;}

    public string? Prezume {get; set;}
}