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
            Interface.type("Game Over. You have been revived at the last inn you stayed in. You have lost all your gold and half of your levels.", ConsoleColor.DarkRed);
            Main.Player.level /= 2;
            Main.Player.g = 0;
            Main.Player.reputation = -50;
            Map.PlayerPosition.x = Main.Player.last_inn[0];
            Map.PlayerPosition.y = Main.Player.last_inn[1];
        }
        public static void Endgame()
        {
            if (!Main.devmode)
            {
                if (Main.magiccounter >= 1)
                    Interface.type("You recognize the magic man from the alley in Central. It is he who stands before you.");
                Interface.type("\"I am Janus.\" The man before you says. You stand in front of the protetorate, Janus. He says \"" + Main.Player.name + ", have you realized that this world is an illusion?\" You nod your head. \"You will result in the Realm\"s demise, you must be purged. Shimmering light gathers around him, and beams of blue light blast out of him.");
                Interface.type("I regret to say it, but we must fight.");
                Combat.BattleLoop(new finalboss());
                Main.ach.Get("finalboss");
                Interface.type("Janus defeated, the world vanishes and you both are standing on glass in a blank world of black void.");
                Interface.type("The protectorate kneels on the ground in front of you. \"Do you truly wish to end the illusion?\", he says. You look at him without uttering a word. \"Very well, I must ask of you one last favor; even though the realm is no more, do not let it vanish from within you.\" Everything around you goes black. (Press any key to continue)");
                Interface.readkey();
                Console.Clear();
            }
            Interface.type("GAME CLEAR!", ConsoleColor.Yellow);
            Interface.type("==========STATS==========", ConsoleColor.Yellow);
            Interface.type("Name: " + Main.Player.name, ConsoleColor.Yellow);
            Interface.type("Race: " + Main.Player.race.ToUpperFirstLetter(), ConsoleColor.Yellow);
            Interface.type("Class: " + Main.Player.pclass.ToUpperFirstLetter(), ConsoleColor.Yellow);
            Interface.type("Level: " + Main.Player.level, ConsoleColor.Yellow);
            Interface.type("Gold: " + Main.Player.g, ConsoleColor.Yellow);
            Interface.type("Reputation " + Main.Player.reputation, ConsoleColor.Yellow);
            Interface.type("FINAL SCORE: " + calcScore().Item2, ConsoleColor.Yellow);
            Interface.type("FINAL RANK: " + calcScore().Item1, ConsoleColor.Yellow);
            Interface.type("=========================\r\n", ConsoleColor.Yellow);
            Interface.type("Press any key to continue.", ConsoleColor.White);
            Interface.readkey();
            Console.Clear();
            Credits();
        }
        public static void Credits()
        {
            Interface.type("A child awakes from his sleep and looks out the window feeling fulfilled as if a story has come to a close.");
            Interface.type("Press any key to continue.", ConsoleColor.White);
            Interface.readkey();
            Console.Clear();
            Interface.type("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.DarkMagenta);
            Interface.type("----------------------Credits---------------------", ConsoleColor.DarkMagenta);
            Interface.type("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.DarkMagenta);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Executive Developer: Cooper Teixeira", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Secondary Developer: Giorgio Lo", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Executive Producer: Cooper Teixeira", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Co-Producer: Giorgio Lo", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Lead Content Designer: Giorgio Lo", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Secondary Content Designer: Cooper Teixeira", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Chief Tester: Rosemary Rogal", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Lead Art Designer: Rosemary Rogal", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Secondary Art Designer: Giorgio Lo", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Secondary Art Designer: Cooper Teixeira", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Concept Artist: Rosemary Rogal", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Special thanks to:", ConsoleColor.DarkMagenta);
            Interface.type("- Steve Teixeira", ConsoleColor.Yellow);
            Interface.type("- Ryan Teixeira", ConsoleColor.Yellow);
            Interface.type("- Paul Pfenning", ConsoleColor.Yellow);
            Interface.type("- Charlie Catino", ConsoleColor.Yellow);
            Interface.type("- Bradley Lignoski", ConsoleColor.Yellow);
            Interface.type("- Alexander Pfenning", ConsoleColor.Yellow);
            Interface.type("- Ben Boyd", ConsoleColor.Yellow);
            Interface.type("__________________________________________________", ConsoleColor.DarkMagenta);
            Interface.type("Copyright(c) 2013", ConsoleColor.White);
            Interface.type("Press any key to continue.", ConsoleColor.White);
            Interface.readkey();
            Environment.Exit(0);
        }

        public static Tuple<string, int> calcScore()
        {
            int score = (Main.Player.reputation + (Main.Player.level * 10) + Main.Player.g);
            string rank = (score >= 1500 ? "S Rank" : score >= 1000 ? "A Rank" : score >= 750 ? "B Rank" : score >= 500 ? "C Rank" : score >= 250 ? "D Rank" : score >= 0 ? "F Rank" : "Reggie?");
            return new Tuple<string, int>(rank, score);
        }
    }
}
