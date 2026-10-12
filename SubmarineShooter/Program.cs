using System;

namespace SubmarineShooter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SUBMARINE ANIMATION/MINI-GAME | Made by Jack G");
            Console.WriteLine("\nPLEASE NOTE, this animation works best in windowed mode.\n");
            Console.WriteLine("I'm not very good at ascii art so I decided to make something interactive to compensate :)");
            Thread.Sleep(8000);
            Console.Clear();

            startGame();

            Console.WriteLine("Thank you for playing my short game! Press [ENTER] to exit. Type 'Y' to play again.");
            string input = Console.ReadLine();
            char playAgain = Convert.ToChar(input.Substring(0, 1).ToUpper());

            if (playAgain == 'Y')
            {
                Console.Clear();
                startGame();
            }
        }

        static void startGame()
        {
            TitleScreen();
            DrawSubSpotted();
            DrawRecticle();
        }

        static void TitleScreen()
        {
            Console.SetCursorPosition(52, 13);
            Console.Write("December 1941");
            for (int i = 0; i < 4; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.Clear();
            Console.SetCursorPosition(10, 13);
            Console.Write("Kriegsmarine submarines have been indiscriminately targeting merchant vessels across the Atlantic");
            for (int i = 0; i < 6; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.Clear();
            Console.SetCursorPosition(16, 13);
            Console.Write("HMS Starling, The Royal Navy's finest, has been tasked with destroying the German subs");
            for (int i = 0; i < 6; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.Clear();
        }

        static void DrawSubSpotted()
        {
            Thread.Sleep(1000);

            for (int i = 0; i < 25; i++)
            {
                Console.SetCursorPosition(36, 10);
                Console.WriteLine("An enemy U-boat has been spotted near the surface!");
                Console.SetCursorPosition(28, 13);
                Console.WriteLine("""__)\__)\__)\__)\_)\__)\__)\__)\__)\__)\__)\__)\_)\__)\__)\__)\__""");
                Console.SetCursorPosition(55 - i, 16);
                Console.WriteLine("""        __|]_              """);
                Console.SetCursorPosition(55 - i, 17);
                Console.WriteLine("""  _____|  _  |\________    """);
                Console.SetCursorPosition(55 - i, 18);
                Console.WriteLine(""" (_____________________|)=|""");
                Thread.Sleep(333);
                Console.Clear();
                //I don't like this but it works, should find a more efficient method of drawing instead of using a million setcursor statements
            }

            Console.SetCursorPosition(47, 10);
            Console.WriteLine("Gunner, man your stations!");
            Console.SetCursorPosition(22, 12);
            Console.WriteLine("Aim the ship's cannons at the submarine. Remember to account for distance!");
            Console.SetCursorPosition(32, 14);
            Console.WriteLine("The submarine is indicated by the legend: ___|]___");
            Thread.Sleep(10000);
            Console.Clear();
        }

        static void DrawRecticle()
        {
            char input = 'C';
            int xAxis = 54, yAxis = 10, subBearing = 55;
            double range = 0, yAxisRadians = 0;
            bool isAiming;

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Random rand = new Random();
            int subDistance = rand.Next(7, 16);

            do
            {
                subBearing = rand.Next(30, 90);

            } while (subBearing >= 53 && subBearing <= 58); //this is just to stop the sub from appearing below the origin

            do
            {
                isAiming = true;

                DrawSubTarget(subDistance, subBearing);

                Console.SetCursorPosition(0, 17);
                Console.WriteLine("-- SEA LEVEL -------->");

                Console.SetCursorPosition(58, 12);
                Console.WriteLine("-o-");

                //this calculates the range of the projectile
                if ((15 + (-yAxis)) < 0)
                {
                    range = 0;
                }
                else
                {
                    yAxisRadians = (15 + (-yAxis)) * (Math.PI / 180);
                    range = ((Math.Pow(800, 2) * Math.Sin(2 * yAxisRadians)) / 9.8) * 0.5; //halving is my attempt to account for drag
                }

                try
                {
                    Console.SetCursorPosition(xAxis, yAxis);
                    Console.WriteLine("""    ___    """);
                    Console.SetCursorPosition(xAxis, yAxis + 1);
                    Console.WriteLine("""     |     """);
                    Console.SetCursorPosition(xAxis, yAxis + 2);
                    Console.WriteLine(""" |---O---| """);
                    Console.SetCursorPosition(xAxis, yAxis + 3);
                    Console.WriteLine("""     |     """);
                    Console.SetCursorPosition(xAxis, yAxis + 4);
                    Console.WriteLine("""    ---    """);

                    if ((10 + (yAxis) > 0))
                    {
                        for (int j = 0; j < (13 + (-yAxis)); j++)
                        {
                            Console.SetCursorPosition(xAxis + 5, (yAxis + 5) + j);
                            Console.WriteLine(".");
                        }
                        Console.SetCursorPosition(xAxis + 12, (yAxis + 4));
                        Console.WriteLine($"<- {range:F2} m");
                    }

                }
                catch
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Error! Perhaps you tried moving the recticle out of bounds?");
                    Thread.Sleep(5000);
                    xAxis = 52;
                    yAxis = 10;
                    Console.Clear();
                    Console.WriteLine("Press [ENTER] to restore the recticle to the origin.");
                }

                Console.SetCursorPosition(17, 24);
                Console.WriteLine("""
                        Use 'A', 'D', 'W', 'S' and hold [ENTER] to aim the targeting recticle over the sub!
                                         A = LEFT | D = RIGHT | W = UP | S = DOWN |  Increase range by raising the recticle!
                        """);
                Console.SetCursorPosition(27, 27);
                Console.WriteLine($"Recticle Bearing: {xAxis}° | Gun Elevation Relative to Horizon: {15 + (-yAxis)}°");

                try //lazy exception handling
                {
                    string inputString = Console.ReadLine().ToUpper();
                    input = Convert.ToChar(inputString);
                }
                catch
                {

                }

                switch (input)
                {
                    case 'A':
                        xAxis -= 1;
                        break;
                    case 'D':
                        xAxis += 1;
                        break;
                    case 'W':
                        yAxis -= 1;
                        break;
                    case 'S':
                        yAxis += 1;
                        break;
                    case 'F':
                        isAiming = false;
                        break;
                }

                Console.Clear();

            } while (isAiming == true);

            FireCannons(xAxis, subBearing, subDistance, range);
        }

        static void DrawSubTarget(int subDistance, int subBearing)
        {
            Console.WriteLine("When ready, enter 'F' to fire!");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"Target Distance: ~{subDistance} KM ");
            Console.SetCursorPosition(subBearing, 17);
            Console.WriteLine("___|]___");

        }

        static void FireCannons(int bearing, int subBearing, int subDistance, double range)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(48, 13);
            Console.WriteLine("Aye sir! Weapons away!");
            Console.SetCursorPosition(37, 15);
            Console.WriteLine($"On your target bearing: {bearing}°, {range:F2} meters");
            Thread.Sleep(3000);

            CannonAnimation();

            //the following block of code is for debugging the hit check system
            /*Console.Clear();
            Console.WriteLine($"Sub bearing is {subBearing}. Sub distance is {subDistance*1000}");
            Console.WriteLine($"User bearing is {bearing}. User range is {range}");*/

            int bearingLower = subBearing - 3, bearingUpper = subBearing + 3;
            int rangeLower = (subDistance * 1000) - 500, rangeUpper = (subDistance * 1000) + 500;
            double deltaDistance = 0;

            //hit check system
            if (bearing >= bearingLower && bearing <= bearingUpper)
            {
                if (range >= rangeLower && range <= rangeUpper)
                {
                    //Console.WriteLine("Hit!");
                    HitAnimation();
                }
                else
                {
                    deltaDistance = (subDistance * 1000) - range;
                    //Console.WriteLine($"Shell was short by {deltaDistance:F2} meters!");
                    ShortAnimation(deltaDistance);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.SetCursorPosition(37, 13);
                Console.WriteLine("Miss! Adjust your firing solution and try again!");
                Thread.Sleep(3000);
                Console.Clear();
                DrawRecticle();
            }
        }

        static void CannonAnimation()
        {
            Console.Clear();
            Console.SetCursorPosition(55, 13);
            Console.WriteLine(""" ______""");
            Console.SetCursorPosition(55, 14);
            Console.WriteLine("""|______|====   __________""");
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("""__|__|________/         /""");
            Thread.Sleep(750);

            Console.Clear();
            Console.SetCursorPosition(55, 13);
            Console.WriteLine(""" ______""");
            Console.SetCursorPosition(55, 14);
            Console.WriteLine("""|______====    __________""");
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("""__|__|________/         /""");
            Thread.Sleep(750);

            Console.Clear();
            Console.SetCursorPosition(55, 13);
            Console.WriteLine("""  _____""");
            Console.SetCursorPosition(55, 14);
            Console.WriteLine(""" |____==o      __________""");
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("""__|__|________/         /""");
            Thread.Sleep(750);

            Console.Clear();
            Console.SetCursorPosition(55, 13);
            Console.WriteLine("""  ____""");
            Console.SetCursorPosition(55, 14);
            Console.WriteLine(""" |___=oo       __________""");
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("""__|__|________/         /""");
            Thread.Sleep(750);

            Console.Clear();
            Console.SetCursorPosition(55, 13);
            Console.WriteLine("""  ____""");
            Console.SetCursorPosition(55, 14);
            Console.WriteLine(""" |__=oo        __________""");
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("""__|__|________/         /""");
            Thread.Sleep(750);

            Console.Clear();
            Console.SetCursorPosition(55, 13);
            Console.WriteLine("""  ____""");
            Console.SetCursorPosition(55, 14);
            Console.WriteLine(""" | oo |        __________""");
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("""__|__|________/         /""");
            Thread.Sleep(750);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(53, 13);
            Console.WriteLine("""   /\/\/\/\ """);
            Console.SetCursorPosition(53, 14);
            Console.WriteLine(" < BOOM !!! >");
            Console.SetCursorPosition(53, 15);
            Console.WriteLine("""   \/\/\/\/ """);
            Thread.Sleep(750);

        }

        static void HitAnimation()
        {
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < 16; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(54, 0 + i);
                Console.WriteLine("""||""");

                Console.SetCursorPosition(45, 16);
                Console.WriteLine("""        __|]_                  <(Scheiße...)""");
                Console.SetCursorPosition(45, 17);
                Console.WriteLine("""  _____|  _  |\________    """);
                Console.SetCursorPosition(45, 18);
                Console.WriteLine(""" (_____________________|)=|""");
                Thread.Sleep(20);
            }

            Console.Clear();
            Console.SetCursorPosition(45, 14);
            Console.WriteLine("""         *                 """);
            Console.SetCursorPosition(45, 15);
            Console.WriteLine("""       . ^   .        .    """);
            Console.SetCursorPosition(45, 16);
            Console.WriteLine("""    \  -^/\^ . /           """);
            Console.SetCursorPosition(45, 17);
            Console.WriteLine("""  _*_\_\ /\_ /\_/*_/___    """);
            Console.SetCursorPosition(45, 18);
            Console.WriteLine(""" (_____________________|)=|""");
            Thread.Sleep(400);
            Console.Clear();
            Console.SetCursorPosition(45, 15);
            Console.WriteLine("""         .                 """);
            Console.SetCursorPosition(45, 16);
            Console.WriteLine(""" \  .  -^*/^\  -     /     """);
            Console.SetCursorPosition(45, 17);
            Console.WriteLine("""  _.-./\ /\_*\/_\.-____    """);
            Console.SetCursorPosition(45, 18);
            Console.WriteLine(""" (_____________________|)=|""");
            Thread.Sleep(400);
            Console.Clear();
            Console.SetCursorPosition(45, 16);
            Console.WriteLine("""    . *-^ .   -           """);
            Console.SetCursorPosition(45, 17);
            Console.WriteLine("""  _.-^- -_^ ^*_\.-____    """);
            Console.SetCursorPosition(45, 18);
            Console.WriteLine(""" (_____________________|)=|""");
            Thread.Sleep(400);
            Console.Clear();
            Console.SetCursorPosition(45, 16);
            Console.WriteLine("""       .        *          """);
            Console.SetCursorPosition(45, 17);
            Console.WriteLine("""  _.*^_ _-. /|__.-*___     """);
            Console.SetCursorPosition(45, 18);
            Console.WriteLine(""" (_____________________|)=|""");
            Thread.Sleep(400);
            Console.Clear();
            Console.SetCursorPosition(45, 17);
            Console.WriteLine("""  _._-_ _-.*/|__._____     """);
            Console.SetCursorPosition(45, 18);
            Console.WriteLine(""" (_____________________|)=|""");
            Thread.Sleep(400);
            Console.SetCursorPosition(51, 10);
            Console.WriteLine("Target destroyed!");
            Thread.Sleep(1500);
            Console.SetCursorPosition(51, 12);
            Console.WriteLine("Well done gunner!");

            Thread.Sleep(5000);
            Console.Clear();
        }

        static void ShortAnimation(double deltaDistance)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            string overUnder = "undershot";

            if(deltaDistance < 0) {
                deltaDistance *= -1;
                overUnder = "overshot";
            }

            Console.SetCursorPosition(42, 13);
            Console.WriteLine($"Your shell {overUnder} by {deltaDistance:F2} meters!");
            Console.SetCursorPosition(40, 15);
            Console.WriteLine($"Adjust your firing solution and try again!");

            Thread.Sleep(3000);
            Console.Clear();
            DrawRecticle();
        }
    }
}


