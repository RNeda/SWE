namespace Models;

[Table("Predstave")]
public class Predstave : Dogadjaji
{
    
    public  List<Glumci>? Glumci {get; set;}


    public  int BrMesta {get; set;}

    public string? KratakOpis {get; set;}
}
    
 