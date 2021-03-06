﻿// /////////////////////////////////////////////////////////////////////////////////////////////////////                                                                                           
// Project - Realm created on 09/27/2013 by Cooper Teixeira                                           //
//                                                                                                    //
// Copyright (c) 2014 - All rights reserved                                                           //
//                                                                                                    //
// This software is provided 'as-is', without any express or implied warranty.                        //
// In no event will the authors be held liable for any damages arising from the use of this software. //
// /////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace Realm
{
    public enum Difficulty
    {
        easy = 1,
        normal,
        impossible
    }
    public class Enemy
    {
        public string name;
        public int level, hp, atk, def, spd, xpdice, gpdice, fire, extrarep, mod;
        public bool trapped = false, cursed, on_fire, stunned, blinded;
        protected List<string> abilities;

        public virtual void attack(out string ability_used)
        {
            ability_used = "";
        }

        public void droploot()
        {
            var gold = Main.rand.Next(gpdice / 2, gpdice);
            Interface.type("You gain " + gold + " gold.", ConsoleColor.Yellow);
            if (!Main.is_theif)
                Main.Player.g += gold;
            else
                Main.Player.g += (gold + (gold/2));

            var xp = Dice.roll(1, xpdice);
            Interface.type("You gained " + xp + " xp.", ConsoleColor.Yellow);
            Main.Player.xp += xp;
            var dropcands = Player.getCorrectlyTieredItems();

            if (Main.rand.NextDouble() <= .1d)
            {
                var i = dropcands[Main.rand.Next(0, dropcands.Count - 1)];
                Main.Player.backpack.Add(i);
            }
            if (Main.rand.NextDouble() <= .005d)
            {
                Main.Player.backpack.Add(new lucky_slots());
            }

            Main.Player.reputation += (1 + extrarep);
        }

        public Enemy()
        {
            level = Main.Player.level;
        }
    }

    public class Slime : Enemy
    {
        public Slime()
        {
            mod = (int)((level <= 10 ? 2 : level <= 15 ? 7 : 18) * (Main.difficulty == Difficulty.easy ? 1 : Main.difficulty == Difficulty.normal ? 1.5 : 2));
            name = "Slime";
            hp = level + mod;
            atk = 1 + mod;
            def = mod;
            spd = mod;
            xpdice = 12 - (level/5);
            gpdice = 8 - (level/5);
            abilities = new List<string> {"BasicAttack", "SuperSlimySlam"};
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "SuperSlimySlam")
            {
                double damage = Dice.roll(1, (atk*2) + 1);
                dmg = (Convert.ToInt32(damage) - Main.Player.def) + 1;
                ability_used = "Super Slimy Slam";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class Goblin : Enemy
    {
        public Goblin()
        {
            name = "Goblin";
            mod = (int)((level <= 10 ? 2 : level <= 15 ? 7 : 18) * (Main.difficulty == Difficulty.easy ? 1 : Main.difficulty == Difficulty.normal ? 1.5 : 2));
            hp = 7 + mod;
            atk = 3 + mod;
            def = 1 + mod;
            spd = 1 + mod;
            xpdice = 20;
            gpdice = 15;
            abilities = new List<string> {"BasicAttack", "Impale", "CrazedSlashes"};
        }

        public override void attack(out string ability_used)
        {
            var dmg = 0;
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Math.Max((Convert.ToInt32(damage) - (Main.Player.def/3)), 0);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Impale")
            {
                double damage = Dice.roll(1, atk*2);
                dmg = Convert.ToInt32(damage);
                ability_used = "Impale";
            }
            else if (Combat.DecideAttack(abilities) == "CrazedSlashes")
            {
                double damage = Dice.roll(1, atk);
                damage *= Dice.roll(1, 5);
                dmg = Convert.ToInt32(damage);
                ability_used = "Crazed Slashes";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class Bandit : Enemy
    {
        public Bandit()
        {
            name = "Bandit";
            mod = (int)((level <= 10 ? 2 : level <= 15 ? 7 : 18) * (Main.difficulty == Difficulty.easy ? 1 : Main.difficulty == Difficulty.normal ? 1.5 : 2));
            hp = 12 + mod;
            atk = 1 + mod;
            def = 0 + mod;
            spd = 3 + mod;
            xpdice = 25;
            gpdice = 18;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("DustStorm");
            abilities.Add("RavenousPound");
        }

        public override void attack(out string ability_used)
        {
            var dmg = 0;
            ability_used = "";
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Math.Max((Convert.ToInt32(damage) - (Main.Player.def/3)), 0);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "DustStorm")
            {
                double damage = Dice.roll(atk, 3);
                dmg = Convert.ToInt32(damage);
                ability_used = "Dust Storm";
            }
            else if (Combat.DecideAttack(abilities) == "RavenousPound")
            {
                double damage = Dice.roll(4, 2);
                dmg = Convert.ToInt32(damage);
                ability_used = "Ravenous Pound";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class WesternKing : Enemy
    {
        public WesternKing()
        {
            name = "Western King";
            hp = 1500 + (level/2);
            atk = 50 + (level/2);
            def = 35 + (level/2);
            spd = 25 + (level/2);
            xpdice = 100 + (level/2);
            gpdice = 100 + (level/2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Terminate");
            extrarep = 99;
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - -(Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Terminate")
            {
                double damage = Dice.roll(2, atk / 3);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Terminate";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class Drake : Enemy
    {
        public Drake()
        {
            name = "Drake";
            mod = (int)((level <= 10 ? 2 : level <= 15 ? 7 : 18) * (Main.difficulty == Difficulty.easy ? 1 : Main.difficulty == Difficulty.normal ? 1.5 : 2));
            hp = 35;
            atk = 5 + mod;
            def = 6 + mod;
            spd = 7 + mod;
            xpdice = 35;
            gpdice = 35;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Singe");
            abilities.Add("Chomp");
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Singe")
            {
                double damage = Dice.roll(1, atk*2/3);
                Main.Player.on_fire = true;
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Singe";
            }
            else if (Combat.DecideAttack(abilities) == "Chomp")
            {
                double damage = Dice.roll(2, atk);
                Main.Player.stunned = true;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Chomp";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class RavenKing : Enemy
    {
        public RavenKing()
        {
            name = "Raven King";
            hp = 250 + (level/2);
            atk = 45 + (level/2);
            def = 20 + (level/2);
            spd = 25 + (level/2);
            xpdice = 80;
            gpdice = 80;
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Crow Call");
            abilities.Add("Murder");
            extrarep = 49;
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Crow Call")
            {
                double damage = Dice.roll(2, atk/3);
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Crow Call";
            }
            else if (Combat.DecideAttack(abilities) == "Murder")
            {
                double damage = Dice.roll(3, atk);
                Main.Player.stunned = true;
                dmg = Convert.ToInt32(damage) - Main.Player.def;
                ability_used = "Murder";
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class finalboss : Enemy
    {
        public finalboss()
        {
            name = "Janus";
            hp = 750 + (level/2);
            atk = 150 + (level/2);
            def = 85 + (level/2);
            spd = 60 + (level/2);
            xpdice = 1 + (level/2);
            gpdice = 1 + (level/2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Illusory Slash");
            abilities.Add("Time Bend");
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Illusory Slash")
            {
                double damage = Dice.roll(3, atk);
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage);
                ability_used = "Illusory Slash";
            }
            else if (Combat.DecideAttack(abilities) == "Time Bend")
            {
                double damage = Dice.roll(2, atk/2);
                Main.Player.stunned = true;
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class cavespider : Enemy
    {
        public cavespider()
        {
            name = "Cave Spider";
            mod = (int)((level <= 10 ? 2 : level <= 15 ? 7 : 18) * (Main.difficulty == Difficulty.easy ? 1 : Main.difficulty == Difficulty.normal ? 1.5 : 2));
            hp = 20 + (level/2) + mod;
            atk = 10 + (level/2) + mod;
            def = 5 + (level/2) + mod;
            spd = 6 + (level/2) + mod;
            xpdice = 30 + (level/2);
            gpdice = 30 + (level/2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Poison Bite");
            abilities.Add("Cocoon");
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Poison Bite")
            {
                double damage = Dice.roll(1, atk);
                Main.Player.cursed = true;
                dmg = Convert.ToInt32(damage);
                ability_used = "Posion Bite";
            }
            else if (Combat.DecideAttack(abilities) == "Cocoon")
            {
                Main.Player.stunned = true;
                dmg = 2;
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class cavebat : Enemy
    {
        public cavebat()
        {
            name = "Cave Bat";
            mod = (int)((level <= 10 ? 2 : level <= 15 ? 7 : 18) * (Main.difficulty == Difficulty.easy ? 1 : Main.difficulty == Difficulty.normal ? 1.5 : 2));
            hp = 10 + (level/2) + mod;
            atk = 6 + (level/2) + mod;
            def = 3 + (level/2) + mod;
            spd = 4 + (level/2) + mod;
            xpdice = 15 + (level/2);
            gpdice = 15 + (level/2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Screech");
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Screech")
            {
                if (Combat.stunchance(3))
                    Main.Player.stunned = true;
                dmg = 3;
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }

    public class Dragon : Enemy
    {
        public Dragon()
        {
            name = "Tyrone the Dragon";
            hp = 100 + (level/2);
            atk = 50 + (level/2);
            def = 25 + (level/2);
            spd = 30 + (level/2);
            xpdice = 300 + (level/2);
            gpdice = 75 + (level/2);
            abilities = new List<string>();
            abilities.Add("BasicAttack");
            abilities.Add("Flame Breath");
            abilities.Add("Cursed Claw");
            extrarep = 149;
        }

        public override void attack(out string ability_used)
        {
            ability_used = "";
            var dmg = 0;
            if (Combat.DecideAttack(abilities) == "BasicAttack")
            {
                double damage = Dice.roll(1, atk);
                dmg = Convert.ToInt32(damage) - (Main.Player.def/3);
                ability_used = "Basic Attack";
            }
            else if (Combat.DecideAttack(abilities) == "Flame Breath")
            {
                Main.Player.on_fire = true;
                dmg = Dice.roll(1, atk/2);
            }
            else if (Combat.DecideAttack(abilities) == "Cursed Claw")
            {
                Main.Player.cursed = true;
                dmg = Dice.roll(2, 20);
            }
            if (dmg <= 0)
                dmg = 1;
            if (Dice.misschance(Main.Player.spd))
                dmg = 0;
            Main.Player.hp -= dmg;
        }
    }
}