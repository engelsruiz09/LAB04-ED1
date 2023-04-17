using LAB04_ED1.Models.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

using System.IO;
using LAB04_ED1.Models;

namespace LAB04_ED1.Controllers
{
    public class VehiculosController : Controller
    {
        private IWebHostEnvironment Environment;
        public VehiculosController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        // GET: VehiculosController
        public ActionResult Index()
        {
            if (Singleton.Instance.flag == 0)
            {
                Singleton.Instance.flag = 0;
                return View(Singleton.Instance.lista_arbol);
            }
            else
            {
                return View();
            }
        }

        // GET: VehiculosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehiculosController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: VehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var NuevoAuto = new Models.Vehiculos
                {
                    ID = collection["ID"],
                    Placa = collection["Placa"],
                    Propietario = collection["Propietario"],
                    Color = collection["Color"],
                    Latitud = collection["Latitud"],
                    Longitud = collection["Longitud"]
                };
                //Singleton.Instance.Arbol_2_3.Insertar(NuevoAuto);
                Singleton.Instance.flag = 0;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CargarArchivo()
        {
            return View();
        }

        public ActionResult CargarArchivo2(IFormFile File)
        {
            string ID = "", Placa = "", Propietario = "", Color = "", Latitud = "", Longitud = "";

            try
            {

                if (File != null)
                {
                    string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string FileName = Path.GetFileName(File.FileName);
                    string FilePath = Path.Combine(path, FileName);
                    using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }
                    using (TextFieldParser csvFile = new TextFieldParser(FilePath))
                    {

                        csvFile.CommentTokens = new string[] { "#" };
                        csvFile.SetDelimiters(new string[] { "," });
                        csvFile.HasFieldsEnclosedInQuotes = true;

                        csvFile.ReadLine();

                        while (!csvFile.EndOfData)
                        {
                            string[] fields = csvFile.ReadFields();
                            ID = Convert.ToString(fields[0]);
                            Placa = Convert.ToString(fields[1]);
                            Propietario = Convert.ToString(fields[2]);
                            Color = Convert.ToString(fields[3]);
                            Longitud = Convert.ToString(fields[4]);
                            Latitud = Convert.ToString(fields[5]);
                            Vehiculos nuevoVehiculo = new Vehiculos
                            {
                                ID = ID,
                                Placa = Placa,
                                Propietario = Propietario,
                                Color = Color,
                                Longitud = Longitud,
                                Latitud = Latitud,

                            };
                            Singleton.Instance.flag = 0;
                            Singleton.Instance.lista_arbol.Add(nuevoVehiculo);
                            //Singleton.Instance.Arbol_2_3
                            //agregar metodo cuando este arbol binario
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["Message"] = "Algo sucedio mal";
                return RedirectToAction(nameof(Index));

            }
        }

        // GET: VehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehiculosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehiculosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
