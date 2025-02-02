using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using PruebaTecnica.Models;
using PruebaTecnica.Utiles;

namespace PruebaTecnica.Controllers
{ 
    public class VisitasController : Controller
    {
        public string filePath = Environment.CurrentDirectory + "/Registro de visitas.xlsx";
        public Utilidades Utilidades = new();

        // GET: VisitasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VisitasController/Listado
        public ActionResult Listado()
        {
            ExcelWorksheet worksheet = Utilidades.GetWorksheet(filePath, 0);
            int rowCount = worksheet.Dimension.Rows;

            SetCaptions(worksheet, 1);
            List<Visitas> visitas = SetDataTable(worksheet, rowCount);

            return View(visitas);

        }

        // GET: VisitasController/Edicion/5?
        public ActionResult Edicion(int? id)
        {
            Visitas visita = new Visitas();
            ExcelWorksheet worksheet = Utilidades.GetWorksheet(filePath, 0);
            SetCaptions(worksheet, 1);

            if (id == null)
            {
                ViewBag.titulo = "Nueva visita";
                ViewBag.subtitulo = "Añade la visita";
            }
            else
            {
                ViewBag.titulo = "Edición de visita";
                ViewBag.subtitulo = "Edita la visita";
                visita = SetDataRow(worksheet, id.Value);
                if (visita.PedidoRealizado)
                    ViewBag.PedidoRealizadoSi = "selected";
                if (!visita.PedidoRealizado)
                    ViewBag.PedidoRealizadoNo = "selected";
            }

            return View(visita);
        }


        // GET: VisitasController/Guardar/5?
        public ActionResult Guardar(int? id)
        {

            if (id == null)
            {
                TempData["Mensaje"] = "Vista creada correctamente.";
            }
            else
            {
                TempData["Mensaje"] = "Vista actualizada correctamente.";
            }

            GuardarVisita(id);

            return RedirectToAction(nameof(Listado));
        }

        // GET: VisitasController/Eliminar/5?
        public ActionResult Eliminar(int id)
        {
            try
            {
                FileInfo FileInfo = new FileInfo(filePath);
                using (var package = new ExcelPackage(FileInfo))
                {
                    // Obtén la primera hoja del archivo Excel
                    ExcelWorksheet hoja = package.Workbook.Worksheets[0]; // 0 es el índice de la primera hoja, puedes cambiar el índice si es necesario

                    // Elimina la fila especificada (el índice de la fila comienza en 1, no en 0)
                    hoja.DeleteRow(id);

                    // Guarda los cambios en el archivo Excel
                    package.Save();
                }

                TempData["Mensaje"] = "Visita borrada correctamente.";
                return RedirectToAction(nameof(Listado));
            }
            catch
            {
                TempData["MensajeError"] = "No se ha podido borrar la visita.";
                return RedirectToAction(nameof(Listado));
            }
        }


        //------------------- Funciones privadas

        private void SetCaptions(ExcelWorksheet worksheet, int row)
        {
            ViewBag.NombreColumnaComercial = worksheet.Cells[row, 1].Text;
            ViewBag.NombreColumnaCliente = worksheet.Cells[row, 2].Text;
            ViewBag.NombreColumnaDireccionCliente = worksheet.Cells[row, 3].Text;
            ViewBag.NombreColumnaPersonaContactoCliente = worksheet.Cells[row, 4].Text;
            ViewBag.NombreColumnaTelefonoCliente = worksheet.Cells[row, 5].Text;
            ViewBag.NombreColumnaEmailCliente = worksheet.Cells[row, 6].Text;
            ViewBag.NombreColumnaFechaVisita = worksheet.Cells[row, 8].Text;
            ViewBag.NombreColumnaHoraInicio = worksheet.Cells[row, 9].Text;
            ViewBag.NombreColumnaHoraFin = worksheet.Cells[row, 10].Text;
            ViewBag.NombreColumnaPedidoRealizado = worksheet.Cells[row, 12].Text;
            ViewBag.NombreColumnaImportePedidoRealizado = worksheet.Cells[row, 13].Text;
            ViewBag.NombreColumnaNotas = worksheet.Cells[row, 15].Text;
        }

        private List<Visitas> SetDataTable(ExcelWorksheet worksheet, int rowCount)
        {
            List<Visitas> visitas = new List<Visitas>();
            for (int row = 2; row <= rowCount; row++)
            {
                Visitas visita = new Visitas();
                visita.Id = row;
                visita.Comercial = worksheet.Cells[row, 1]?.Text;
                visita.Cliente = worksheet.Cells[row, 2].Text;
                visita.DireccionCliente = worksheet.Cells[row, 3].Text;
                visita.PersonaContactoCliente = worksheet.Cells[row, 4]?.Text;
                visita.TelefonoCliente = worksheet.Cells[row, 5]?.Text;
                visita.EmailCliente = worksheet.Cells[row, 6]?.Text;
                visita.FechaVisita = Convert.ToDateTime(worksheet.Cells[row, 8]?.Value);
                visita.HoraInicio = Convert.ToDateTime(worksheet.Cells[row, 9]?.Value);
                visita.HoraFin = Convert.ToDateTime(worksheet.Cells[row, 10]?.Value);
                visita.PedidoRealizado = worksheet.Cells[row, 12].Text.StartsWith("S") ? true : false;
                if(!string.IsNullOrWhiteSpace(worksheet.Cells[row, 13]?.Text))
                    visita.ImportePedidoRealizado = Convert.ToDecimal(worksheet.Cells[row, 13]?.Text);
                visita.Notas = worksheet.Cells[row, 15].Text;

                visitas.Add(visita);
            }

            return visitas;
        }

        private Visitas SetDataRow(ExcelWorksheet worksheet, int row)
        {
            Visitas visita = new Visitas();
            visita.Id = row;
            visita.Comercial = worksheet.Cells[row, 1]?.Text;
            visita.Cliente = worksheet.Cells[row, 2].Text;
            visita.DireccionCliente = worksheet.Cells[row, 3].Text;
            visita.PersonaContactoCliente = worksheet.Cells[row, 4]?.Text;
            visita.TelefonoCliente = worksheet.Cells[row, 5]?.Text;
            visita.EmailCliente = worksheet.Cells[row, 6]?.Text;
            visita.FechaVisita = Convert.ToDateTime(worksheet.Cells[row, 8]?.Value);
            visita.HoraInicio = Convert.ToDateTime(worksheet.Cells[row, 9]?.Value);
            visita.HoraFin = Convert.ToDateTime(worksheet.Cells[row, 10]?.Value);
            visita.PedidoRealizado = worksheet.Cells[row, 12].Text.StartsWith("S") ? true : false;
            visita.ImportePedidoRealizado = Convert.ToDecimal(worksheet.Cells[row, 13]?.Value);
            visita.Notas = worksheet.Cells[row, 15].Text;


            return visita;
        }


        private int GuardarVisita(int? id)
        {
            int row = 0;
            FileInfo FileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(FileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                if (id == null)
                    row = worksheet.Dimension.Rows + 1;
                else
                    row = id.Value;

                worksheet.Cells[row, 1].Value = Request.Form["Comercial"];
                worksheet.Cells[row, 2].Value = Request.Form["Cliente"];
                worksheet.Cells[row, 3].Value = Request.Form["DireccionCliente"];
                worksheet.Cells[row, 4].Value = Request.Form["PersonaContactoCliente"];
                worksheet.Cells[row, 5].Value = Request.Form["TelefonoCliente"];
                worksheet.Cells[row, 6].Value = Request.Form["EmailCliente"];
                worksheet.Cells[row, 8].Value = Request.Form["FechaVisita"];
                worksheet.Cells[row, 9].Value = Request.Form["HoraInicio"];
                worksheet.Cells[row, 10].Value = Request.Form["HoraFin"];
                worksheet.Cells[row, 12].Value = Request.Form["PedidoRealizado"];
                worksheet.Cells[row, 13].Value = Request.Form["ImportePedidoRealizado"].ToString().Replace(".",",");
                worksheet.Cells[row, 15].Value = Request.Form["Notas"];

                package.Save();
            }

            return row;

        }

        //------------------------------- Funciones privadas

    }
}
