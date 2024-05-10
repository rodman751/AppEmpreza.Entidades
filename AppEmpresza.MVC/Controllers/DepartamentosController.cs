using AppEmpreza.ConsumeAPI;
using AppEmpreza.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace AppEmpresza.MVC.Controllers
{
    public class DepartamentosController : Controller
    {

        public INotyfService _notifyService { get; }
        
        private  string urlApi;
        public DepartamentosController(IConfiguration configuration, INotyfService notyfService)
        {
            urlApi = configuration.GetValue("ApiUrlBase", "").ToString() + "/Departamento";
            _notifyService = notyfService;
        }
        // GET: DepartamentosController
        public ActionResult Index()
        {
            try
            {
                var data = AppEmpreza.ConsumeAPI.CRUD<Departamento>.Read(urlApi);
                //_notifyService.Success("Bienvenido a la página de departamentos");
                return View(data);
            }
            catch (Exception ex)
            {
                if (ex.Message == "La API está cargando o ha ocurrido un error")
                {
                    _notifyService.Error("Error al cargar los datos, La API está cargando o ha ocurrido un error, vuelva a intentarlo");
                    ModelState.AddModelError("", ex.Message);
                    return RedirectToAction("Index", "ErrorAPI");
                }
                throw;
            }
        }


        // GET: DepartamentosController/Details/5
        public ActionResult Details(int id)
        {
            _notifyService.Success("Bienbenido a Details");
            try
            {
                var data = CRUD<Departamento>.Read_ById(urlApi, id);
                return View(data);
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al cargar los datos");
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: DepartamentosController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: DepartamentosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Departamento data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newData = CRUD<Departamento>.Created(urlApi, data);
                    _notifyService.Success("Departamento creado con éxito!");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notifyService.Error("Error al crear el departamento");
                    return PartialView("Index", data);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("Index", data);
            }
        }

        // GET: DepartamentosController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = CRUD<Departamento>.Read_ById(urlApi, id);
            return View(data);
        }

        // POST: DepartamentosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Departamento data)
        {
            try
            {
                CRUD<Departamento>.Update(urlApi, id, data);
                _notifyService.Success("Departamento actualizado con éxito!");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _notifyService.Error("Error al actualizar el departamento");
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: DepartamentosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = CRUD<Departamento>.Read_ById(urlApi, id);
            _notifyService.Warning("Esta apundo de borrar este Departameno");
            return View(data);
        }

        // POST: DepartamentosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Departamento data)
        {
            try
            {
                CRUD<Departamento>.Delete(urlApi, id);
                _notifyService.Information("Departamento eliminado con éxito!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al eliminar el departamento");
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }
    }
}
