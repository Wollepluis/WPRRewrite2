using Microsoft.EntityFrameworkCore;
using WPRRewrite2.Modellen;
using WPRRewrite2.Modellen.Abbo;
using WPRRewrite2.Modellen.Account;
using WPRRewrite2.Modellen.Kar;

namespace WPRRewrite2;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }


    public DbSet<Account> Accounts { get; set; }
    public DbSet<Voertuig> Voertuigen { get; set; }
    public DbSet<Bedrijf> Bedrijven { get; set; }
    public DbSet<Adres> Adressen { get; set; }
    public DbSet<Abonnement> Abonnementen { get; set; }
    public DbSet<Reservering> Reserveringen { get; set; }
    //public DbSet<Reparatie> Reparaties { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Account>()
            .HasDiscriminator<string>("AccountType")
            .HasValue<AccountParticulier>("Particulier")
            .HasValue<AccountMedewerkerFrontoffice>("Frontoffice")
            .HasValue<AccountMedewerkerBackoffice>("Backoffice")
            .HasValue<AccountZakelijkBeheerder>("ZakelijkBeheerder")
            .HasValue<AccountZakelijkHuurder>("ZakelijkHuurder");

        builder.Entity<Voertuig>()
            .HasDiscriminator<string>("VoertuigType")
            .HasValue<Auto>("Auto")
            .HasValue<Camper>("Camper")
            .HasValue<Caravan>("Caravan");

        builder.Entity<Abonnement>()
            .HasDiscriminator<string>("AbonnementType")
            .HasValue<PayAsYouGo>("PayAsYouGo")
            .HasValue<UpFront>("UpFront");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        if (!builder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
        }
    }
}