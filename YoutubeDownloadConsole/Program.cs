using System;
using System.Diagnostics;
using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;
using static MediaToolkit.Model.Metadata;

namespace YoutubeDownloadConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Write("Digite o caminho da pasta que deseja salvar: ");
                string? source = Console.ReadLine();
                Console.Write("Digite link da url do Youtube: ");
                string? url = Console.ReadLine();
                Console.Write("Deseja Converter para MP3 ? (S/N) ");
                string? condicao = Console.ReadLine();
                bool mp3 = (condicao == "S" || condicao == "s"|| condicao == "Sim" || condicao == "sim") ? true : false;
                var youtube = YouTube.Default;
                Console.WriteLine("Fazendo Download do video...");
                var video = youtube.GetVideo(url);
                Console.WriteLine("Download Concluido!");
                Console.WriteLine("Criando Arquivo MP4...");
                File.WriteAllBytes(source + video.FullName, video.GetBytes());

                if (mp3.Equals(true))
                {
                    SalvarMp3(source, video.FullName);
                }

                

                Console.WriteLine("Conversão Concluida na pasta: " + source);
                Console.Read();
            }
            catch(IOException ex)
            {
                throw new Exception("Erro no arquivo " + ex.Message);
                Console.Read();
            }
            catch(Exception e)
            {
                    throw new Exception("Error " + e.Message);
                Console.Read();
            }
        }

        private static void SalvarMp3(string path, string mp3Name)
        {
            try
            {
                var inputFile = new MediaFile { Filename = path + mp3Name };
                var outputFile = new MediaFile { Filename = $"{path + mp3Name}.mp3" };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    Console.WriteLine("Convertendo arquivo para MP3...");
                    engine.Convert(inputFile, outputFile);
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
                Console.Read();
            }
        }
    }
}
