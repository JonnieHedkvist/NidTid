using iTextSharp.tool.xml.parser.io;
using iTextSharp.tool;
using iTextSharp.tool.xml.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace NidTid.WebUI.Controllers
{
    public class PdfController : Controller
    {
        protected string RenderActionResultToString(ActionResult result)
        {
            // Create memory writer.
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            // Create fake http context to render the view.
            var fakeResponse = new HttpResponse(memWriter);
            var fakeContext = new HttpContext(System.Web.HttpContext.Current.Request,
                fakeResponse);
            var fakeControllerContext = new ControllerContext(
                new HttpContextWrapper(fakeContext),
                this.ControllerContext.RouteData,
                this.ControllerContext.Controller);
            var oldContext = System.Web.HttpContext.Current;
            System.Web.HttpContext.Current = fakeContext;

            // Render the view.
            result.ExecuteResult(fakeControllerContext);

            // Restore old context.
            System.Web.HttpContext.Current = oldContext;

            // Flush memory and return output.
            memWriter.Flush();
            return sb.ToString();
        }

        protected ActionResult ViewPdf(object model)
        {
            // Create the iTextSharp document.
            Document doc = new Document();
            // Set the document to write to memory.
            MemoryStream memStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memStream);
            writer.CloseStream = false;
            doc.Open();

            // Render the view xml to a string, then parse that string into an XML dom.
            string xmltext = this.RenderActionResultToString(this.View(model));
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.InnerXml = xmltext.Trim();

            // Parse the XML into the iTextSharp document.
            XMLParser textHandler = new ITextHandler(doc);
            textHandler.Parse(xmldoc);

            // Close and get the resulted binary data.
            doc.Close();
            byte[] buf = new byte[memStream.Position];
            memStream.Position = 0;
            memStream.Read(buf, 0, buf.Length);

            // Send the binary data to the browser.
            return new BinaryContentResult(buf, "application/pdf");
        }
    }
}
