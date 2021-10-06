using System.Threading.Tasks;
using ns_ddfe_core.src.commons;
using Newtonsoft.Json;

namespace ns_ddfe_core.src.ddfe.unique
{
    public class DownloadUnico
    {
        public class Body
        {
            public string CNPJInteressado { get; set; }
            public int nsu { get; set; }
            public int modelo { get; set; }
            public string chave { get; set; }
            public bool apenasComXml { get; set; }
            public bool comEventos { get; set; }
            public int tpAmb { get; set; }
            public bool incluirPDF { get; set; }

        };

        public class Response
        {
            public string status { get; set; }
            public bool listaDocs { get; set; }
            public string nsu { get; set; }
            public string chave { get; set; }
            public string emitCnpj { get; set; }
            public string emitRazao { get; set; }
            public string cSitNFe { get; set; }
            public string modelo { get; set; }
            public string vNF { get; set; }
            public string tpEvento { get; set; }
            public string xml { get; set; }
            public string pdf { get; set; }
            public dynamic[] xmls { get; set; }

        }

        public static async Task<Response> sendPostRequest(Body requestBody, string caminhoSalvar)
        {
            string url = "https://ddfe.ns.eti.br/dfe/unique";

            var responseAPI = JsonConvert.DeserializeObject<Response>(await nsAPI.postRequest(url, JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })));

            if (responseAPI.pdf != null)
            {
                util.salvarArquivo(caminhoSalvar, responseAPI.chave, "-nfeProc.pdf", responseAPI.pdf);
            }

            if (responseAPI.xml != null)
            {
                util.salvarArquivo(caminhoSalvar, responseAPI.chave, "-nfeProc.xml", responseAPI.xml);
            }

            if (responseAPI.xmls != null)
            {
                for(int i = 0; i<=responseAPI.xmls.Length; i++)
                {
                    util.salvarArquivo(caminhoSalvar, responseAPI.xmls[i].chave, "-nfeProc.xml", responseAPI.xmls[i].xml);
                }
            }

            return responseAPI;
        }
    }
}
