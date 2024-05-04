using AppEmpreza.ConsumeAPI;
using AppEmpreza.Entidades;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppEmpresza.MVC.Controllers
{
    public class CargoController : Controller
    {
        public INotyfService _notifyService { get; }

        private string urlApi;
        public CargoController(IConfiguration configuration, INotyfService notyfService)
        {
            urlApi = configuration.GetValue("ApiUrlBase", "").ToString() + "/Cargos";
            _notifyService = notyfService;
        }
        // GET: CargoController
        public ActionResult Index()
        {
            var data = AppEmpreza.ConsumeAPI.CRUD<Cargo>.Read(urlApi);
            return View(data);
        }

        // GET: CargoController/Details/5
        public ActionResult Details(int id)
        {
            _notifyService.Success("Bienbenido a Details");
            try
            {
                var data = CRUD<Cargo>.Read_ById(urlApi, id);
                return View(data);
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al cargar los datos");
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: CargoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cargo data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newData = CRUD<Cargo>.Created(urlApi, data);
                    _notifyService.Success("Cargo creado con éxito!");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notifyService.Error("Error al crear el Cargo");
                    return PartialView("Index", data);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("Index", data);
            }
        }

        // GET: CargoController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = CRUD<Cargo>.Read_ById(urlApi, id);
            return View(data);
        }

        // POST: CargoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cargo data)
        {
            try
            {
                CRUD<Cargo>.Update(urlApi, id, data);
                _notifyService.Success("Cargo actualizado con éxito!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al actualizar el Cargo");
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: CargoController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = CRUD<Cargo>.Read_ById(urlApi, id);
            _notifyService.Warning("Esta apundo de borrar este Cargo");
            return View(data);
        }

        // POST: CargoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Cargo data)
        {
            try
            {
                CRUD<Cargo>.Delete(urlApi, id);
                _notifyService.Information("cargo eliminado con éxito!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al eliminar el cargo");
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }
    }
}
