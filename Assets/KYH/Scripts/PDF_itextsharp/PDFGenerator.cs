using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using UnityEngine;

public class PDFGenerator : MonoBehaviour
{
    //private void Start()
    //{
    //    CreatePDF(Application.dataPath + "/test.pdf");
    //}

    private void Update()
    {
        // 0번 누르면 PDF 생성
        if (Input.GetKeyDown(KeyCode.Alpha0)) CreatePDF(Application.dataPath + "/test.pdf");
    }

    public void CreatePDF(string filePath)
    {
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();

        // 한글 폰트 설정
        string fontPath = System.IO.Path.Combine(Application.dataPath, "Fonts", "나눔손글씨 야근하는 김주임.ttf");
        BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //BaseFont bf = BaseFont.CreateFont("D:\\UnityProjects\\TargetMS\\Assets\\Fonts\\나눔손글씨 야근하는 김주임.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12); // 폰트 크기 설정

        document.Add(new Paragraph(ProposalMgr.instance.text_response.text, font));
        document.Close();
        print("PDF 생성 완료!");
    }
}
