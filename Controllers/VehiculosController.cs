using LAB04_ED1.Models.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using TDA;
using System.Collections.Generic;
using System.Linq;
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
        public delegate string claveCoordenadas(Vehiculo vehiculo);
        public delegate int clavePosicion(Vehiculo vehiculo, Nodo23<Vehiculo> nodo1);
        public delegate Vehiculo ClaveEdicion(Vehiculo vehiculo1, Vehiculo vehiculo2);
        // GET: VehiculosController
        public ActionResult Index()
        {
            if (Singleton.Instance.flag == 0)
            {
                Singleton.Instance.flag = 0;
                List<Vehiculo> listaValores = Singleton.Instance.Arbol_2_3.ObtenerValoresEnLista();
                return View(listaValores);
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
                Vehiculo NuevoVehiculo = new Vehiculo()
                {
                    Placa = collection["Placa"],
                    Color = collection["Color"],
                    Propietario = collection["Propietario"],
                    Longitud = Convert.ToInt32(collection["Longitud"]),
                    Latitud = Convert.ToInt32(collection["Latitud"]),
                };
                Console.WriteLine("depurar");
                claveCoordenadas claveVehiculo = Vehiculo.ObtenerCoordenadas;
                NuevoVehiculo.Coordenadas = claveVehiculo(NuevoVehiculo);
                clavePosicion posicionAInsertar = Vehiculo.obtenerPos;
                Singleton.Instance.Arbol_2_3.Insertar(NuevoVehiculo, posicionAInsertar);
                Singleton.Instance.flag = 0;
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToAction(nameof(Index));
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
                            Placa = Convert.ToString(fields[0]);
                            Propietario = Convert.ToString(fields[1]);
                            Color = Convert.ToString(fields[2]);
                            Longitud = Convert.ToString(fields[3]);
                            Latitud = Convert.ToString(fields[4]);
                            Vehiculo nuevoVehiculo = new Vehiculo
                            {
                                Placa = Placa,
                                Propietario = Propietario,
                                Color = Color,
                                Longitud = Convert.ToInt32(Longitud),
                                Latitud = Convert.ToInt32(Latitud),

                            };
                            Singleton.Instance.flag = 0;
                            claveCoordenadas claveVehiculo = Vehiculo.ObtenerCoordenadas;
                            nuevoVehiculo.Coordenadas = claveVehiculo(nuevoVehiculo);
                            clavePosicion posicionAInsertar = Vehiculo.obtenerPos;
                            Singleton.Instance.Arbol_2_3.Insertar(nuevoVehiculo, posicionAInsertar);
                            Singleton.Instance.flag = 0;
                            
                        }
                        return RedirectToAction(nameof(Index));
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
