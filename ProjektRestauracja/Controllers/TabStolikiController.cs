using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektRestauracja.DBC;
using ProjektRestauracja.Models;

namespace ProjektRestauracja.Controllers
{
    public class TabStolikiController : Controller
    {

        TabStolikiDBC _TabStolikiDBC = new TabStolikiDBC();

        // GET: TabStoliki
        public ActionResult Index()
        {
            var TabStolikiList = _TabStolikiDBC.GetAllTabStoliki();
            if (TabStolikiList.Count == 0)
            {
                TempData["InfoMessage"] = "Aktualnie baza danych nie zawiera żadnego stolika.";
            }

            return View(TabStolikiList);
        }

        // GET: TabStoliki/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TabStoliki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TabStoliki/Create
        [HttpPost]
        public ActionResult Create(TabStoliki tabStoliki)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _TabStolikiDBC.InsertTabStoliki(tabStoliki);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Zmainy zapisano poprawnie.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Nie udało się wprowadzić zmian.";
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: TabStoliki/Edit/5
        public ActionResult Edit(int id)
        {
            var tabStoliki = _TabStolikiDBC.EditTabStoliki(id).FirstOrDefault();
            if (tabStoliki == null)
            {
                TempData["InfoMessage"] = "Taki Stolik nie istnieje" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(tabStoliki);
        }

        // POST: TabStoliki/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateTabStoliki(TabStoliki tabStoliki)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _TabStolikiDBC.UpdateTabStoliki(tabStoliki);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Zmainy zapisano poprawnie.";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Nie udało się wprowadzić zmian.";
                    }
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        // GET: TabStoliki/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var tabStoliki = _TabStolikiDBC.EditTabStoliki(id).FirstOrDefault();
                if (tabStoliki == null)
                {
                    TempData["InfoMessage"] = "Stolik o takim ID nie istnieje" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(tabStoliki);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        // POST: TabStoliki/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _TabStolikiDBC.DeleteTabStoliki(id);
                if (result.Contains("usunięto"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
