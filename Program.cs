using excel_reader;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
using excel_reader.Context;

var options = new DbContextOptionsBuilder<ExcelContext>()
    .UseSqlite("Data Source=exceldata.db")
    .Options;

using var context = new ExcelContext(options);

context.Database.EnsureCreated();

ReadXls();

void ReadXls()
{
    var response = new List<Person>();

    FileInfo existingFile = new FileInfo(fileName: "personal file.xlsx");

    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    using (ExcelPackage package = new ExcelPackage(existingFile))
    {
        ExcelWorksheet worksheet = package.Workbook.Worksheets[PositionID: 0];
        int colCount = worksheet.Dimension.End.Column;

        int rowCount = worksheet.Dimension.End.Row;

        for (int row = 2; row <= rowCount; row++)
        {
            var person = new Person();
            person.Name = worksheet.Cells[row, Col: 1].Value.ToString();
            person.Email = worksheet.Cells[row, Col: 2].Value.ToString();

            response.Add(person);           
        }
    }

    context.People.AddRange(response);
    context.SaveChanges();
}