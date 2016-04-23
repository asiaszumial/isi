using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;

namespace ISIProject.Helpers
{
    public static class XmlRender
    {
        /// <summary>
        /// Applies an XSL transformation to an XML document.
        /// </summary>
        public static HtmlString RenderXml(this HtmlHelper helper, string xmlPath, string xsltPath)
        {
            XsltArgumentList args = new XsltArgumentList();

            XslCompiledTransform t = new XslCompiledTransform();
            t.Load(xsltPath);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;

            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                StringWriter writer = new StringWriter();
                t.Transform(reader, args, writer);
                return new HtmlString(writer.ToString());
            }

        }

    }
}