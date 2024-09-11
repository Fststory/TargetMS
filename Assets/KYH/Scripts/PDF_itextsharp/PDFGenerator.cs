using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using UnityEngine;

public class PDFGenerator : MonoBehaviour
{
    private void Start()
    {
        CreatePDF(Application.dataPath + "/test.pdf");
    }

    public void CreatePDF(string filePath)
    {
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();
        document.Add(new Paragraph("Hello, iTextSharp!"));
        document.Add(new Paragraph("My name is Kim Young Ho!"));
        document.Add(new Paragraph("My name is Kim Dong Hwi!"));
        document.Add(new Paragraph("My name is Park Jeong Hyun!"));
        document.Close();
        print("PDF 생성 완료!");
    }
}
