﻿using System;
using System.IO;

namespace ns_ddfe_core.src.commons
{
    class util
    {
        public static void gravarLinhaLog(string registro)
        {
            string caminho = @".\logs\";

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            try
            {
                using StreamWriter outputFile = new StreamWriter(@".\logs\" + DateTime.UtcNow.ToString("MMddyyyy") + ".log", true);
                outputFile.WriteLine(DateTime.Now.ToShortDateString() + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff") + " - " + registro);
            } 
            
            catch (Exception ex)
            {
                gravarLinhaLog("[ERRO]: " + ex.Message);
            }

        }

        public static void salvarArquivo(string caminho, string nomeArquivo, string extensao, string conteudo) 
        {
            //string caminhoSalvar = Path.Combine(caminho, nomeArquivo + extensao);

            try
            {
                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho);
            }

            catch(Exception ex)
            {
                gravarLinhaLog("[ERRO]: " + ex.Message);
            }

            try 
            { 
                switch (extensao)
                {
                    case var target when extensao.Contains(".xml"):

                        try
                        {
                            conteudo = conteudo.Replace(@"\""", "");
                            using (StreamWriter outputFile = new StreamWriter(caminho + nomeArquivo + extensao))
                            {
                                outputFile.WriteLine(conteudo);
                            };
                        }

                        catch (Exception ex)
                        {
                            gravarLinhaLog("[ERRO]: " + ex.Message);
                        }

                        break;

                    case var target when extensao.Contains(".json"):

                        try
                        {
                            using (StreamWriter outputFile = new StreamWriter(caminho + nomeArquivo + extensao))
                            {
                                outputFile.WriteLine(conteudo);
                            };
                        }

                        catch(Exception ex)
                        {
                            gravarLinhaLog("[ERRO]: " + ex.Message);
                        }

                        break;

                    case var target when extensao.Contains(".pdf"):

                        try
                        {
                            caminho = Path.Combine(caminho, nomeArquivo + extensao);
                            byte[] bytes = Convert.FromBase64String(conteudo);
                            if (File.Exists(caminho))
                                File.Delete(caminho);
                            FileStream stream = new FileStream(caminho, FileMode.CreateNew);
                            BinaryWriter writer = new BinaryWriter(stream);
                            writer.Write(bytes, 0, bytes.Length);
                            writer.Close();
                        }

                        catch (Exception ex)
                        {
                            gravarLinhaLog("[ERRO]: " + ex.Message);
                        }

                        break;

                    case var target when extensao.Contains(".txt"):

                        try
                        {
                            using (StreamWriter outputFile = new StreamWriter(caminho + nomeArquivo + extensao))
                            {
                                outputFile.WriteLine(conteudo);
                            };
                        }

                        catch (Exception ex)
                        {
                            gravarLinhaLog("[ERRO]: " + ex.Message);
                        }

                        break;
                }
            } 

            catch (Exception ex) 
            {
                gravarLinhaLog("[ERRO]: " + ex.Message);
            }
        }
    }
}
