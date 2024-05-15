using System;

namespace WebScrapper.Model
{
    public class Arquivo
    {
        public static string NomeArquivo { get; set; } = "Dados " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
        public static string CaminhoArquivo { get; set; } = @"C:\WebScrapper\Excel";
    }
}
