using System.IO;
using OfficeOpenXml;

namespace PruebaTecnica.Utiles
{
    public class Utilidades
    {

        public Utilidades() 
        {
        }

        public ExcelWorksheet GetWorksheet(string Filepath, int hoja) 
        {
            FileInfo FileInfo = new FileInfo(Filepath);
            ExcelPackage package = new ExcelPackage(FileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[hoja];
            return worksheet;
        }
        
    }
}
