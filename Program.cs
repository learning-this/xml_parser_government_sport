using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace xml_gov
{
    class Sport
    {
        public string Name;
        public string Data;
        public string Type;

        public Sport()
        {
            
        }

        public void Show()
        {
            Console.WriteLine(this.Name);
            Console.WriteLine(this.Type); // вид мероприятия
            Console.WriteLine(this.Data);
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Sport[] array = new Sport[0];
            string html = System.IO.File.ReadAllText(@"base.xml");
            //Console.WriteLine(html);
            
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var nodes = htmlDoc.DocumentNode.SelectNodes("descendant::item");

            Sport newSport = new Sport();
            int counter = 0;
            foreach (var node in nodes)
            {
                var newHtmlDoc = new HtmlDocument();
                newHtmlDoc.LoadHtml(node.InnerHtml);

                var n = newHtmlDoc.DocumentNode.SelectSingleNode("caption");
                if (n != null)
                {
                    if (n.InnerText == "Наименование" || n.InnerText == "Дата проведения" ||
                        n.InnerText == "Вид мероприятия")
                    {
                        //Console.WriteLine(n.InnerText);
                        
                        var n11 = newHtmlDoc.DocumentNode.SelectSingleNode("dopattrval");
                        //if (n11 != null)
                            //Console.WriteLine(n11.InnerText);
                        //if (n.InnerText == "Вид мероприятия")
                            //Console.WriteLine();


                        if (n.InnerText == "Наименование")
                        {
                            newSport.Name = n11.InnerText;
                        }
                        else if (n.InnerText == "Вид мероприятия")
                        {
                            newSport.Type = n11.InnerText;
                        }
                        else
                            if (n.InnerText == "Дата проведения")
                                {
                                    newSport.Data = n11.InnerText;
                                    Array.Resize(ref array, array.Length + 1);
                                    array[array.Length - 1] = newSport;
                                    newSport = new Sport();
                                }
                    }
                }
            }

            foreach (Sport sport in array)
            {
                sport.Show();
            }

            Console.ReadKey();
        }
    }
}
