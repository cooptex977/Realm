﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm
{
    public class End
    {   
        public static void GameOver()
        {
            Interface.type("Game Over. Press any key to exit, esc during non-combat to exit. Your save has been deleted.");
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.rlm";
            if (File.Exists(path))
                File.Delete(path);
            Interface.readkey();
            Environment.Exit(0);
        }

    }
}
