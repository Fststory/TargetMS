using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public class PDFGenerator
{
    public void CreatePDF(string filePath)
    {
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();
        document.Add(new Paragraph("Hello, iTextSharp!"));
        document.Add(new Paragraph("My name is Kim Young Ho!"));
        document.Close();
    }
}
