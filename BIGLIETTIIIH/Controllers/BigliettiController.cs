using Microsoft.AspNetCore.Mvc;
using BIGLIETTIIIH.Models;

namespace BIGLIETTIIIH.Controllers
{
    public class BigliettiController : Controller
    {
        private static List<Biglietto> biglietti = new List<Biglietto>();
        private static List<Sala> sale = new List<Sala>()
        {
            new Sala { Id = Guid.NewGuid(), Nome = "SALA NORD" },
            new Sala { Id = Guid.NewGuid(), Nome = "SALA EST" },
            new Sala { Id = Guid.NewGuid(), Nome = "SALA SUD" },
        };
        public IActionResult Index()
        {
            var biglietto = new BigliettoAddModel();
            ViewBag.Sale = sale;
            ViewBag.Biglietti = biglietti;
            return View(biglietto);
        }

        [HttpPost]
        public IActionResult Acquista(Biglietto biglietto)
        {
            var sala = sale.FirstOrDefault(s => s.Id == biglietto.Sala?.Id);

            if (sala != null && sala.PostiTotali > 0)
            {
                sala.PostiTotali--;

                if (!biglietto.isRidotto)
                {
                    sala.BigliettiVenduti++;
                }
                else
                {
                    sala.BigliettiRidotti++;
                }

                biglietti.Add(new Biglietto
                {
                    Id = Guid.NewGuid(),
                    Nome = biglietto.Nome,
                    Cognome = biglietto.Cognome,
                    Sala = sala,
                    isRidotto = biglietto.isRidotto
                });
            }

            return RedirectToAction("Index");
        }

        [HttpGet("biglietto/edit/{id:guid}")]
        public IActionResult Modifica(Guid id)
        {
            var biglietto = biglietti.FirstOrDefault(b => b.Id == id);

            if (biglietto == null)
            {
                TempData["Error"] = "Product not found!";
                return RedirectToAction("Index");
            }
            var model = new BigliettoEdit
            {
                Id = biglietto.Id,
                Nome = biglietto.Nome,
                Cognome = biglietto.Cognome,
                SalaId = biglietto.Sala.Id,
                IsRidotto = biglietto.isRidotto
            };

            ViewBag.Sale = sale;
            return View(model);
        }


        [HttpPost("biglietto/edit/save/{id:guid}")]
        public IActionResult SalvaModifiche(Guid id, BigliettoEdit model)
        {
            var biglietto = biglietti.FirstOrDefault(b => b.Id == id);
            if (biglietto == null)
            {
                TempData["Error"] = "Product not found!";
                return RedirectToAction("Index");
            }
            var sala = sale.FirstOrDefault(s => s.Id == model.SalaId);
            if (sala == null)
            {
                TempData["Error"] = "Sala not found!";
                return RedirectToAction("Index");
            }
            biglietto.Nome = model.Nome;
            biglietto.Cognome = model.Cognome;
            biglietto.Sala = sala;
            biglietto.isRidotto = model.IsRidotto;
            return RedirectToAction("Index");
        }

        [HttpGet("biglietto/delete/{id:guid}")]

        public IActionResult Elimina(Guid id)
        {
            var biglietto = biglietti.FirstOrDefault(b => b.Id == id);
            if (biglietto == null)
            {
                TempData["Error"] = "Product not found!";
                return RedirectToAction("Index");
            }
            var succesfullyRemoved = biglietti.Remove(biglietto);

            if (!succesfullyRemoved)
            {
                TempData["Error"] = "Error while removing the product!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
