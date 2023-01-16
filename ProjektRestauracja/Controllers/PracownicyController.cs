using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektRestauracja.DBC;
using ProjektRestauracja.Models;

namespace ProjektRestauracja.Controllers
{
    public class PracownicyController : Controller
    {
        PracownicyDBC _pracownicyDBC = new PracownicyDBC();

        // GET: Pracownicy
        public ActionResult Index()
        {
            var pracownicyList = _pracownicyDBC.GetAllPracownicy();
            if(pracownicyList.Count == 0)
            {
                TempData["InfoMessage"] = "Aktualnie baza danych nie zawiera żadnego pracownika.";
            }

            return View(pracownicyList);
        }

        // GET: Pracownicy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pracownicy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pracownicy/Create
        [HttpPost]
        public ActionResult Create(Pracownicy pracownicy)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _pracownicyDBC.insertPracownicy(pracownicy);

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

        // GET: Pracownicy/Edit/5
        public ActionResult Edit(int id)
        {
            var pracownicy = _pracownicyDBC.EditPracownicy(id).FirstOrDefault();
            if(pracownicy == null)
            {
                TempData["InfoMessage"] = "Pracownik o takim ID nie istnieje" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(pracownicy);
        }

        // POST: Pracownicy/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdatePracownicy(Pracownicy pracownicy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _pracownicyDBC.UpdatePracownicy(pracownicy);

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

        // GET: Pracownicy/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var pracownicy = _pracownicyDBC.EditPracownicy(id).FirstOrDefault();

                if (pracownicy == null)
                {
                    TempData["InfoMessage"] = "Pracownik o takim ID nie istnieje" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(pracownicy);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Pracownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _pracownicyDBC.DeletePracownicy(id);
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
