using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstBank.Models
{
  public class FirstBankContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Member> Members { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<MemberBankAccount> MemberBankAccount { get; set; }

    public FirstBankContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}
