namespace bti.rpa.common;
using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

    public class RPAPDFSplitter
    {

        public void splitPDF(String source, String destination, long splitSize)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(source));

            FileInfo file = new FileInfo(destination);
            if( file != null && file.Directory != null){
            file.Directory.Create();
            }
            IList<PdfDocument> splitDocuments = new CustomPdfSplitter(pdfDoc, destination).SplitBySize(splitSize);
            foreach (PdfDocument doc in splitDocuments)
            {
                Console.WriteLine("Splitt PDF Doc:" + doc.GetNumberOfPages());
                PdfDocumentInfo docInfo = doc.GetDocumentInfo();
                doc.Close();
            }
            pdfDoc.Close();
        }
        
        private class CustomPdfSplitter : PdfSplitter
        {
            private String dest;
            private int partNumber = 1;
            
            public CustomPdfSplitter(PdfDocument pdfDocument, String dest) : base(pdfDocument)
            {
                this.dest = dest;
            }
            
            protected override PdfWriter GetNextPdfWriter(PageRange documentPageRange) {
                return new PdfWriter(String.Format(dest, partNumber++));
            }
        }
    }
