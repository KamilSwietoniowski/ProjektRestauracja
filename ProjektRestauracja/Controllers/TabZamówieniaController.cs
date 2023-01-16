using ProjektRestauracja.DBC;
using ProjektRestauracja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektRestauracja.Controllers
{
    public class TabZamówieniaController : Controller
    {
        TabZamówieniaDBC _tabzamównienaiDBC = new TabZamówieniaDBC();
        // GET: TabZamówienia
        public ActionResult Index()
        {
            var tabzamównieniaList = _tabzamównienaiDBC.GetAllTabZamówienia();
            if (tabzamównieniaList.Count == 0)
            {
                TempData["InfoMessage"] = "Aktualnie baza danych nie zawiera żadnych zamówień.";
            }
            return View(tabzamównieniaList);
        }

        // GET: TabZamówienia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TabZamówienia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TabZamówienia/Create
        [HttpPost]
        public ActionResult Create(TabZamówienia tabZamówienia)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _tabzamównienaiDBC.insertZamówienia(tabZamówienia);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Zmiany zapisano poprawinie ! ";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Nie udało się zapisać zmian ! Wpisano ID nie występujące w bazie danych !  ";
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

        // GET: TabZamówienia/Edit/5
        public ActionResult Edit(int id)
        {
            var  tabZamówienia= _tabzamównienaiDBC.EditZamówienia(id).FirstOrDefault();
            if (tabZamówienia == null)
            {
                TempData["InfoMessage"] = "Potrawa o takim ID nie istnieje" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(tabZamówienia);
        }

        // POST: TabZamówienia/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateZamówienia(TabZamówienia tabZamówienia)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _tabzamównienaiDBC.UpdateZamówienia(tabZamówienia);

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

        // GET: TabZamówienia/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var tabZamówienia = _tabzamównienaiDBC.EditZamówienia(id).FirstOrDefault();
                if (tabZamówienia == null)
                {
                    TempData["InfoMessage"] = "Potrawa o takim ID nie istnieje" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(tabZamówienia);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: TabZamówienia/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _tabzamównienaiDBC.DeleteZamówienia(id);
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
