using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Presentation.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        public string Format(StatementResult statementResult)
        {
            var xdoc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),      
                    new XElement("Customer", statementResult.CustomerName),
                    new XElement("Items",
                        statementResult.Performances.Select(p => new XElement("Item",
                            new XElement("AmountOwed", p.Amount),
                            new XElement("EarnedCredits", p.Credits),
                            new XElement("Seats", p.Audience)
                        ))
                    ),
                    new XElement("AmountOwed", statementResult.TotalAmount),
                    new XElement("EarnedCredits", statementResult.VolumeCredits)
                )
            );
           
            using (var memoryStream = new MemoryStream())
            {
                var xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = new UTF8Encoding(true), // Importante: true para incluir BOM
                    Indent = true,
                    OmitXmlDeclaration = false
                };

                using (var xmlTextWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
                {
                    xdoc.Save(xmlTextWriter);
                }

                memoryStream.Position = 0;
                
                // Use UTF8Encoding(true) no StreamReader
                using (var reader = new StreamReader(memoryStream, new UTF8Encoding(true)))
                {
                    return reader.ReadToEnd();
                }
            }
        }  
    }
}