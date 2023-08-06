namespace Models;

public class Context: DbContext
{
    public required DbSet<Dogadjaji> Dogadjaji {get; set;}
    public required DbSet<Filmovi> Filmovi {get; set;}
    public required DbSet<Glumci> Glumci {get; set;}
    public required DbSet<Gosti> Gosti {get; set;}
    public required DbSet<Koncerti> Koncerti {get; set;}
    public required DbSet<Korisnici> Korisnici {get; set;}
    public required DbSet<OstaliDogadjaji> OstaliDogadjaji {get; set;}
    public required DbSet<Predstave> Predstave{get; set;}
    public required DbSet<Utakmice> Utakmice {get; set;}
    public Context(DbContextOptions options) : base(options)
    {

    }

 

}