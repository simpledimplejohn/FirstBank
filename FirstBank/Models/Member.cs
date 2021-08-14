using System.Collections.Generic;

namespace FirstBank.Models
{
  public class Member
  {
    public Member()
    {
      this.JoinEntities = new HashSet<MemberBankAccount>();
    }

    public int MemberId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<MemberBankAccount> JoinEntities { get; set; }
  }
}