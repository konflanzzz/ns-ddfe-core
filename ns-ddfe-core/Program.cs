using System;
using System.Threading.Tasks;
using ns_ddfe_core.src.ddfe.unique;
using ns_ddfe_core.src.ddfe.bunch;

namespace ns_ddfe_core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await downlaodLote();
        }

        static async Task downloadUnico()
        {
            var requisicaoDownloadUnico = new DownloadUnico.Body
            {
                CNPJInteressado = "",
                chave = "",
                apenasComXml = true,
                comEventos = false,
                incluirPDF = true,
            };

            var retornoDownload = await DownloadUnico.sendPostRequest(requisicaoDownloadUnico, @"DDFe/");
            Console.WriteLine();
        }

        static async Task downlaodLote()
        {
            var requisicaoDownloadLote = new DownloadLote.Body
            {
                CNPJInteressado = "",
                tpAmb = 2,
                incluirPDF = true,
                ultNSU = 0,
                modelo = 55,
                apenasComXml = true
            };

            var retornoDownload = await DownloadLote.sendPostRequest(requisicaoDownloadLote, @"DDFe/",true);
            Console.WriteLine(retornoDownload);
        }
    }
}
