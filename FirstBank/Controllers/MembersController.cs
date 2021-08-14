using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FirstBank.Models;
using System.Collections.Generic;
using System.Linq;

namespace FirstBank.Controllers
{
  [Authorize]
  public class MembersController : Controller
  {
    private readonly FirstBankContext _db;

    public MembersController(FirstBankContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Member> model = _db.Members.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Member member)
    {
      _db.Members.Add(member);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMember = _db.Members
          .Include(member => member.JoinEntities)
          .ThenInclude(join => join.BankAccount)
          .FirstOrDefault(member => member.MemberId == id);
      return View(thisMember);
    }
    public ActionResult Edit(int id)
    {
      var thisMember = _db.Members.FirstOrDefault(member => member.MemberId == id);
      return View(thisMember);
    }

    [HttpPost]
    public ActionResult Edit(Member member)
    {
      _db.Entry(member).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisMember = _db.Members.FirstOrDefault(member => member.MemberId == id);
      return View(thisMember);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMember = _db.Members.FirstOrDefault(member => member.MemberId == id);
      _db.Members.Remove(thisMember);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}