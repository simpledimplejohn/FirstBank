using System.Collections.Generic;

namespace FirstBank.Models
{
  public class BankAccount
  {
    public BankAccount()
    {
      this.JoinEntities = new HashSet<MemberBankAccount>();
    }

    public int BankAccountId { get; set; }
    public string Description { get; set; }
    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<MemberBankAccount> JoinEntities { get;}
  }
}