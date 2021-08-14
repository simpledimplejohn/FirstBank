using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FirstBank.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FirstBank.Controllers
{
  [Authorize]
  public class BankAccountsController : Controller
  {
    private readonly FirstBankContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public BankAccountsController(UserManager<ApplicationUser> userManager, FirstBankContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userBankAccounts = _db.BankAccounts.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userBankAccounts);
    }

    public ActionResult Create()
    {
      ViewBag.MemberId = new SelectList(_db.Members, "MemberId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(BankAccount bankaccount, int MemberId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      bankaccount.User = currentUser;
      _db.BankAccounts.Add(bankaccount);
      _db.SaveChanges();
      if (MemberId != 0)
      {
          _db.MemberBankAccount.Add(new MemberBankAccount() { MemberId = MemberId, BankAccountId = bankaccount.BankAccountId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBankAccount = _db.BankAccounts
          .Include(bankaccount => bankaccount.JoinEntities)
          .ThenInclude(join => join.Member)
          .FirstOrDefault(bankaccount => bankaccount.BankAccountId == id);
      return View(thisBankAccount);
    }

    public ActionResult Edit(int id)
    {
      var thisBankAccount = _db.BankAccounts.FirstOrDefault(bankaccount => bankaccount.BankAccountId == id);
      ViewBag.MemberId = new SelectList(_db.Members, "MemberId", "Name");
      return View(thisBankAccount);
    }

    [HttpPost]
    public ActionResult Edit(BankAccount bankaccount, int MemberId)
    {
      if (MemberId != 0)
      {
        _db.MemberBankAccount.Add(new MemberBankAccount() { MemberId = MemberId, BankAccountId = bankaccount.BankAccountId });
      }
      _db.Entry(bankaccount).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMember(int id)
    {
      var thisBankAccount = _db.BankAccounts.FirstOrDefault(bankaccount => bankaccount.BankAccountId == id);
      ViewBag.MemberId = new SelectList(_db.Members, "MemberId", "Name");
      return View(thisBankAccount);
    }

    [HttpPost]
    public ActionResult AddMember(BankAccount bankaccount, int MemberId)
    {
      if (MemberId != 0)
      {
      _db.MemberBankAccount.Add(new MemberBankAccount() { MemberId = MemberId, BankAccountId = bankaccount.BankAccountId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBankAccount = _db.BankAccounts.FirstOrDefault(bankaccount => bankaccount.BankAccountId == id);
      return View(thisBankAccount);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBankAccount = _db.BankAccounts.FirstOrDefault(bankaccount => bankaccount.BankAccountId == id);
      _db.BankAccounts.Remove(thisBankAccount);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteMember(int joinId)
    {
      var joinEntry = _db.MemberBankAccount.FirstOrDefault(entry => entry.MemberBankAccountId == joinId);
      _db.MemberBankAccount.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}