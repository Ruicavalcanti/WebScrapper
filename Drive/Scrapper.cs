using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Data;
using WebScrapper.Model;

namespace WebScrapper.Drive
{
    public class Scrapper : Web
    {
        public DataTable GetData(string link)
        {
            if (driver == null)
                StartBrowser();

            Navigate(link);
            var items = new List<ProductDetails>();
            var elements = GetValue(TypeElement.Xpath,
                LinkProducts.Path).element.FindElements(By.ClassName(LinkProducts.RowProductsClass));

            foreach (var element in elements)
            {
                var item = new ProductDetails();
                item.Title = element.FindElement(By.ClassName(ClassProduct.Title)).GetAttribute(ClassProduct.Title);
                item.Price = element.FindElement(By.ClassName(ClassProduct.Price)).Text;
                item.Description = element.FindElement(By.ClassName(ClassProduct.Description)).Text;

                items.Add(item);
            }
            return Base.ConvertTo(items);
        }
        public static void Go()
        {
            var web = new Scrapper();
            var computers = web.GetData(LinkProducts.Computers);
            var laptops = web.GetData(LinkProducts.Laptops);
            var tablets = web.GetData(LinkProducts.Tablets);

            if (System.IO.Directory.Exists(Arquivo.CaminhoArquivo) == false)
                System.IO.Directory.CreateDirectory(Arquivo.CaminhoArquivo);

            var param = new ParamsDataTable(Arquivo.NomeArquivo, Arquivo.CaminhoArquivo, new List<DataTables>()
            {
              new DataTables("computers", computers),
              new DataTables("laptops", laptops),
              new DataTables("tablets", tablets)

            }); ;
            Base.GenerateExcel(param);
        }

    }
}
