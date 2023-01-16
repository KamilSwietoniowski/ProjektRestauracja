using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektRestauracja.DBC;
using ProjektRestauracja.Models;

namespace ProjektRestauracja.Controllers
{
    public class PotrawyController : Controller
    {
        PotrawyDBC _potrawyDBC = new PotrawyDBC();

        // GET: Potrawy
        public ActionResult Index()
        {
            var potrawyList = _potrawyDBC.GetAllPotrawy();
            if (potrawyList.Count == 0)
            {
                TempData["InfoMessage"] = "Aktualnie baza danych nie zawiera żadnych potraw.";
            }
            return View(potrawyList);
        }

        // GET: Potrawy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Potrawy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Potrawy/Create
        [HttpPost]
        public ActionResult Create(Potrawy potrawy)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _potrawyDBC.InsertPotrawy(potrawy);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Zmiany zapisano poprawinie ! ";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Nie udało się zapisać zmian ! ";
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


        // GET: Potrawy/Edit/5
        public ActionResult Edit(int id)
        {
            var potrawy = _potrawyDBC.EditPotrawy(id).FirstOrDefault();
            if (potrawy == null)
            {
                TempData["InfoMessage"] = "Potrawa o takim ID nie istnieje" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(potrawy);
        }

        // POST: Potrawy/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdatePotrawy(Potrawy potrawy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _potrawyDBC.UpdatePotrawy(potrawy);

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

        // GET: Potrawy/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var potrawy = _potrawyDBC.EditPotrawy(id).FirstOrDefault();
                if (potrawy == null)
                {
                    TempData["InfoMessage"] = "Potrawa o takim ID nie istnieje" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(potrawy);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Potrawy/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _potrawyDBC.DeletePotrawy(id);
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
