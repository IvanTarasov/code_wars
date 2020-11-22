using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace scriptRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = "C:/Users/ivant/Desktop/test/";

            Console.Write("File name: ");
            string filePath = Console.ReadLine();

            Execute(folderPath + filePath);
        }

        static void Execute(string path)
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile(path);
        }
    }
}
