﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Realm
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight);
                NativeMethods.ShowWindow(NativeMethods.ThisConsole, NativeMethods.MAXIMIZE);
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\Realm");
                Realm.Main.tpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\Realm\temp_save.rlm";
                Realm.Main.path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\Realm\save.rlm";
                Realm.Main.achpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\Realm\achievements.rlm";
                Realm.Main.tachpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\Realm\temp_achievements.rlm";
                Realm.Main.crashpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\my games\Realm\crashlog.txt";
                if (args.Contains("-wipe"))
                {
                    try
                    {
                        File.Delete(Realm.Main.achpath);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Realm.Main.path);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Realm.Main.tpath);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Realm.Main.tachpath);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\update.exe");
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    try
                    {
                        File.Delete(Realm.Main.crashpath);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                }
                if (args.Contains("-noachievements"))
                    Realm.Main.achievements_disabled = true;
                if (args.Contains("-devmode"))
                    Realm.Main.devmode = true;
                Init.Initialize();
            }
            catch (Exception e)
            {
                Save.WriteError(e);
            }
        }
    }
}