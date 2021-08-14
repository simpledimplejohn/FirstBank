namespace FirstBank.Models
{
  public class MemberBankAccount
  {       
    public int MemberBankAccountId { get; set; }
    public int BankAccountId { get; set; }
    public int MemberId { get; set; }
    public virtual BankAccount BankAccount { get; set; }
    public virtual Member Member { get; set; }
  }
}