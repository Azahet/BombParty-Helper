using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Console = Colorful.Console;

namespace BombParty_Helper
{
    internal static class Program
    {
             private static readonly List<string> word_ = new List<string>();

        [STAThread]
        private static void Main()
        {
            word_.Clear();
            Console.Title = "BombParty Helper - by Azahet";
            Console.WriteLine();
            Console.WriteLine(Textcenter("-----------------------------"), Color.WhiteSmoke);
            Console.WriteLine(Textcenter("BombParty© Helper "), Color.WhiteSmoke);
            Console.WriteLine(Textcenter("A program by Azahet"), Color.WhiteSmoke);
            Console.WriteLine(Textcenter("https://github.com/Azahet"), Color.WhiteSmoke);
            Console.WriteLine(Textcenter("-----------------------------"), Color.WhiteSmoke);
            Console.WriteLine();
            Console.WriteLine();
            ask_letter();
            Console.ReadLine();
        }

       
        private static void ask_letter()
        {
            Console.Write("[-] ", Color.SeaGreen);
            Console.Write("Enter the letter must contain the word : ", Color.WhiteSmoke);
            Word(Console.ReadLine());
        }


        private static void Word(string letter)
        {
            string reply;

            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                reply =
                    webClient.DownloadString("http://mots-avec.dictionnaire-des-rimes.fr/index.php?wi_word= " + letter);
            }

            var match = Regex.Match(reply, @"<ul id=""main_ul"">(.*?)</ul>");
            if (match.Success)
            {
                reply = match.Groups[1].Captures[0].Value.Replace("<li>", "").Replace("</li>", "|");
            }
            else
            {
                Console.Write("[!] ", Color.Red);
                Console.WriteLine("Unable to find words with the given letters", Color.WhiteSmoke);
                Console.WriteLine();
                Console.WriteLine("Press any key for restart the research...");
                Console.ReadLine();
                Console.Clear();
                Main();
            }

            foreach (var t in reply.Split('|'))
            {
                if (!t.Contains("-"))
                {
                    word_.Add(t);
                }
            }

            Clipboard.SetText(word_[0]);
            for (int j = 0; j < 20 ; j++)
            {
                try{Console.WriteLine(word_[j]);}catch (Exception){ }
            }
            Thread.Sleep(5000);
            Console.Clear();
            Main();
        }

        private static string Textcenter(string text)
        {
            return string.Format("{0," + (Console.WindowWidth / 2 + text.Length / 2) + "}", text);
        }

    }
}

