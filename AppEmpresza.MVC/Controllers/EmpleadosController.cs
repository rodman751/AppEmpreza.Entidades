using AppEmpreza.ConsumeAPI;
using AppEmpreza.Entidades;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppEmpresza.MVC.Controllers
{
    public class EmpleadosController : Controller
    {
        public INotyfService _notifyService { get; }

        private string urlApi;

        public EmpleadosController(IConfiguration configuration, INotyfService notyfService)
        {
            urlApi = configuration.GetValue("ApiUrlBase", "").ToString() + "/Empleado";



            _notifyService = notyfService;
        }
        // GET: EmpleadosController
        public ActionResult Index()
        {
            var data = AppEmpreza.ConsumeAPI.CRUD<Empleados>.Read(urlApi);
            //_notifyService.Success("Bienvenido a la página de departamentos");
            return View(data);
        }

        // GET: EmpleadosController/Details/5
        public ActionResult Details(int id)
        {
            _notifyService.Success("Bienbenido a Details");
            try
            {
                var data = CRUD<Empleados>.Read_ById(urlApi, id);
                return View(data);
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al cargar los datos");
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: EmpleadosController/Create
        public ActionResult Create()
        {
            var cargos = CRUD<Cargo>.Read("https://appemprezaapi20240501102555.azurewebsites.net/api/Cargos");
            var departamentos = CRUD<Departamento>.Read("https://appemprezaapi20240501102555.azurewebsites.net/api/Departamento");
            ViewBag.Cargos = new SelectList(cargos, "Id", "Nombre");
            ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");

            return View();
        }

        // POST: EmpleadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Empleados data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newData = CRUD<Empleados>.Created(urlApi, data);
                   
                    _notifyService.Success("Empleados creado con éxito!");
                    return RedirectToAction("Index");
                }
                else
                {
                    _notifyService.Error("Error al crear el Empleados");
                    return PartialView("Index", data);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("Index", data);
            }
        }

        // GET: EmpleadosController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = CRUD<Empleados>.Read_ById(urlApi, id);
            var cargos = CRUD<Cargo>.Read("https://appemprezaapi20240501102555.azurewebsites.net/api/Cargos");
            var departamentos = CRUD<Departamento>.Read("https://appemprezaapi20240501102555.azurewebsites.net/api/Departamento");
            ViewBag.Cargos = new SelectList(cargos, "Id", "Nombre");
            ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");

            return View(data);
        }

        // POST: EmpleadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Empleados data)
        {
            try
            {
                CRUD<Empleados>.Update(urlApi, id, data);
                _notifyService.Success("Empleados actualizado con éxito!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al actualizar el Empleados");
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

        // GET: EmpleadosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = CRUD<Empleados>.Read_ById(urlApi, id);
            _notifyService.Warning("Esta apundo de borrar este Empleados");
            return View(data);
        }

        // POST: EmpleadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Empleados data)
        {
            try
            {
                CRUD<Empleados>.Delete(urlApi, id);
                _notifyService.Information("Empleados eliminado con éxito!");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al eliminar el Empleados");
                ModelState.AddModelError("", ex.Message);
                return View(data);
            }
        }

       

    }
}
