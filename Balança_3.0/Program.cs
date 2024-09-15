using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


class Program
{
    static void Main()
    {
        
       // string caminhoDiretorio = AppDomain.CurrentDomain.BaseDirectory;
       // string caminhoArquivo = Path.Combine(caminhoDiretorio, "Parametros.txt");
        string caminhoRelativo = @"Parametros.txt";
        string caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, caminhoRelativo);
        List<string> arquivos = new List<string>();
        string origem;
        string destino;
        string[] linhas;
        string texto;
        string appName = "MeuSoftwareUnico";
        bool createdNew;

        using (Mutex mutex = new Mutex(true, appName, out createdNew)) { 
            if (!createdNew)
            {
                
                return; // Sai do aplicativo
            }
        }
        // File.WriteAllText(caminhoArquivo, "teste");
        //Console.WriteLine($"Arquivo criado com sucesso em: {caminhoArquivo}");
        
            using (StreamReader sr = new StreamReader(caminho))
            {
                texto = sr.ReadToEnd();
                string v = texto.Replace("\r", "");
                linhas = v.Split('\n');
                origem = linhas[0];
                destino = linhas[1];

            }

            for (int i = 2; i < linhas.Length; i++)
            {
                arquivos.Add(linhas[i]);
            }

        do
        {
            if (File.Exists(Path.Combine(origem, arquivos[arquivos.Count - 1])))
            {
                Thread.Sleep(10000);

                foreach (string arquivo in arquivos)
                {
                    string sourceFilePath = Path.Combine(origem, arquivo);
                    string destinationFilePath = Path.Combine(destino, arquivo);

                    if (File.Exists(sourceFilePath))
                    {
                        try
                        {
                            // Move o arquivo
                            File.Copy(sourceFilePath, destinationFilePath, true);
                            File.Delete(sourceFilePath);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Erro ao mover o arquivo {arquivo}: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Arquivo não encontrado: {sourceFilePath}");
                    }
                }
            }
            
            
        } while (true);
    }
}

