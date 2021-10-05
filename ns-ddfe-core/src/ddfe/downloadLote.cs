using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ns_ddfe_core.src.commons;
using Newtonsoft.Json;
using System.IO;

namespace ns_ddfe_core.src.ddfe.bunch
{
    public class DownloadLote
    {
        public class Body
        {
            public string CNPJInteressado { get; set; }
            public dynamic ultNSU { get; set; }
            public string dhInicial { get; set; }
            public string dhFinal { get; set; }
            public bool apenasPendManif { get; set; }
            public bool apenasComXml { get; set; }
            public bool comEventos { get; set; }
            public string[] removerEventosCodigos { get; set; }
            public int modelo { get; set; }
            public int tpAmb { get; set; }
            public bool incluirPDF { get; set; }
        };

        public class Response
        {
            public string status { get; set; }
            public int ultNSU { get; set; }
            public doc[] xmls { get; set; }
            public class doc
            {
                public int nsu { get; set; }
                public string chave { get; set; }
                public string emitCnpj { get; set; }
                public string emitRazao { get; set; }
                public string cSitNFe { get; set; }
                public int modelo { get; set; }
                public long vNF { get; set; }
                public string tpEvento { get; set; }
                public string xml { get; set; }
                public string pdf { get; set; }
            }
        }

        public static async Task<Response> sendPostRequest(Body requestBody, string caminhoSalvar, bool controlador = false)
        {
            string url = "https://ddfe.ns.eti.br/dfe/bunch";

            var responseAPI = new Response();

            if (controlador)
            {
                if (requestBody.ultNSU.Equals(null))
                {
                    requestBody.ultNSU = DDFeBunchController.reader();
                }

                int ultimoNSUDipnivel = 0;

                for (int i = 0; i <= ultimoNSUDipnivel;)
                {
                    responseAPI = JsonConvert.DeserializeObject<Response>(await nsAPI.postRequest(url, JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })));
                    
                    if (responseAPI.ultNSU == DDFeBunchController.reader()){break;}

                    if (responseAPI.xmls != null)
                    {
                        for (int x = 0; x <= responseAPI.xmls.Length - 1; x++)
                        {
                            if (responseAPI.xmls[x].pdf != null)
                            {
                                util.salvarArquivo(caminhoSalvar, responseAPI.xmls[x].chave, "-nfeProc.pdf", responseAPI.xmls[x].pdf);
                            }

                            if (responseAPI.xmls[x].xml != null)
                            {
                                util.salvarArquivo(caminhoSalvar, responseAPI.xmls[x].chave, "-nfeProc.xml", responseAPI.xmls[x].xml);
                            }

                        }
                    }

                    if (responseAPI.xmls[responseAPI.xmls.Length - 1].nsu == responseAPI.ultNSU) // significa que finalizou a busca com todos os documentos possiveis
                    {
                        DDFeBunchController.logger(responseAPI.ultNSU); 
                        break; 
                    };

                    ultimoNSUDipnivel = responseAPI.ultNSU;
                    i = responseAPI.xmls[responseAPI.xmls.Length - 1].nsu;
                    requestBody.ultNSU = responseAPI.xmls[responseAPI.xmls.Length - 1].nsu;

                }
                
            }
            
            else
            {
                if (requestBody.ultNSU.Equals(null))
                {
                    requestBody.ultNSU = 0;
                }

                responseAPI = JsonConvert.DeserializeObject<Response>(await nsAPI.postRequest(url, JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })));

                if (responseAPI.xmls != null)
                {
                    for (int i = 0; i <= responseAPI.xmls.Length - 1; i++)
                    {
                        if (responseAPI.xmls[i].pdf != null)
                        {
                            util.salvarArquivo(caminhoSalvar, responseAPI.xmls[i].chave, "-nfeProc.pdf", responseAPI.xmls[i].pdf);
                        }

                        if (responseAPI.xmls[i].xml != null)
                        {
                            util.salvarArquivo(caminhoSalvar, responseAPI.xmls[i].chave, "-nfeProc.xml", responseAPI.xmls[i].xml);
                        }

                    }
                }
            }
            return responseAPI;
        }

        public static class DDFeBunchController 
        {
            public static void logger(int ultNSU)
            {
                util.salvarArquivo(@"DDFe/logController/","ultNSUlog",".txt",ultNSU.ToString());
            }

            public static int reader()
            {
                try
                {
                    int ultNSU = Int32.Parse(File.ReadAllLines(@"DDFe/logController/ultNSUlog.txt")[0]);
                    return ultNSU;
                }

                catch (Exception) // finally ?
                {
                    return 0;
                }
            }
        }
    }
}
