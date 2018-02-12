using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Gewinnt_Programm
{
    class Program
    {
             struct Spielfeld
        {
            // Das eigentliche Spielfeld
            public int[,] Feld;

            // Verbleibende Züge
            public int Züge;

            // Spieler, welcher am Zug ist
            public int Spieler;

            public bool GameOver;
        }

        static Spielfeld NeuesSpiel()
        {
            Spielfeld Spielfeld = new Spielfeld();
            Spielfeld.GameOver = false;

            // 0 = frei, 1 = Spieler 1, 2 = Spieler 2
            Spielfeld.Feld = new int[6, 7];

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Spielfeld.Feld[i, j] = 0;
                } // for End
            } // for End

            Spielfeld.Züge = 22;
            Spielfeld.Spieler = 1;

            return Spielfeld;
        }

        static Spielfeld Zug(Spielfeld Spielfeld)
        {

            // Spiefeld ausgeben
            Ausgabe(Spielfeld);

            while (true)
            {
                ConsoleKeyInfo k = Console.ReadKey();

                // Falls Eingabe Zahl ist
                if (char.IsDigit(k.KeyChar) && Convert.ToInt32(k.KeyChar) - 49 < 7 &&
                    Spielfeld.Feld[0, Convert.ToInt32(k.KeyChar) - 49] == 0)
                {

                    Spielfeld = SetzeStein(Spielfeld, Convert.ToInt32(k.KeyChar) - 49);
                    Spielfeld = GewinnBerechnung(Spielfeld, Convert.ToInt32(k.KeyChar) - 49);

                    Ausgabe(Spielfeld);
                    break;
                }
                else
                {
                    Ausgabe(Spielfeld);
                }  // else End                                         

            } // while End

            return Spielfeld;
        }

        static Spielfeld SetzeStein(Spielfeld Spielfeld, int spalte)
        {


            for (int i = 0; i < 5; i++)
            {
                if (Spielfeld.Feld[i + 1, spalte] != 0)
                {
                    Spielfeld.Feld[i, spalte] = Spielfeld.Spieler;

                    // Nächster Spieler
                    if (Spielfeld.Spieler == 1)
                    {
                        Spielfeld.Spieler = 2;
                    }
                    else
                    {
                        Spielfeld.Spieler = 1;
                    } // else End

                    return Spielfeld;
                } // if End
            } // for End

            Spielfeld.Feld[5, spalte] = Spielfeld.Spieler;

            // Nächster Spieler
            if (Spielfeld.Spieler == 1)
            {
                Spielfeld.Spieler = 2;
            }
            else
            {
                Spielfeld.Spieler = 1;
            } // else End

            return Spielfeld;
        }

        static void Ausgabe(Spielfeld Spielfeld)
        {
            Console.Clear();

            if (Spielfeld.Spieler == 1)
                Console.WriteLine("Spieler 1 ist an der Reihe!\n");
            else
                Console.WriteLine("Spieler 2 ist an der Reihe!\n");

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(Spielfeld.Feld[i, j]);
                } // for End
                Console.WriteLine();
            } // for End

            Console.WriteLine("\n\nWähle eine Spalte durch eingeben der Nummer(1-7) aus.");

            Console.SetCursorPosition(0, 8);
        }

        static Spielfeld GewinnBerechnung(Spielfeld Spielfeld, int spalte)
        {
            int zeile = 0;

            // Zeile ermitteln
            for (int i = 0; i < 6; i++)
            {
                if (Spielfeld.Feld[i, spalte] != 0)
                {
                    zeile = i;
                    break;
                } // if End
            } // for End

            int e = spalte + 1;

            // Erster Stein von Links
            for (int i = spalte; Spielfeld.Feld[zeile, i] == Spielfeld.Feld[zeile, spalte] &&
                i > 0; i--)
            {
                e--;
            } // for End

            // Gewinn in einer Zeile?
            for (int i = e + 1; i < e + 4 && i < 7; i++)
            {
                if (Spielfeld.Feld[zeile, i] != Spielfeld.Feld[zeile, spalte])
                {
                    break;
                }
                else if (i == e + 3)
                {
                    Spielfeld.GameOver = true;
                    return Spielfeld;
                } // else if End
            } // for End

            // Gewinn in einer Spalte?
            for (int i = 1; i < 5 && i + zeile < 6; i++)
            {
                if (Spielfeld.Feld[i + zeile, spalte] != Spielfeld.Feld[zeile, spalte])
                {
                    break;
                }
                else if (i == 3)
                {
                    Spielfeld.GameOver = true;
                    return Spielfeld;
                } // else if End
            } // for End

            // Gewinn Diagonal?
            int diagonal = 1;

            for (int i = 1; i < 5 && spalte - i > 0 && zeile - i > 0; i++)
            {
                if (Spielfeld.Feld[zeile - i, spalte - i] == Spielfeld.Feld[zeile, spalte])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End            

            for (int i = 1; i < 5 && spalte + i < 7 && zeile + i < 6; i++)
            {
                if (Spielfeld.Feld[zeile + i, spalte + i] == Spielfeld.Feld[zeile, spalte])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End     

            if (diagonal >= 4)
            {
                Spielfeld.GameOver = true;
                return Spielfeld;
            } // if End

            diagonal = 1;

            for (int i = 1; i < 5 && spalte - i > 0 && zeile + i < 6; i++)
            {
                if (Spielfeld.Feld[zeile + i, spalte - i] == Spielfeld.Feld[zeile, spalte])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End            

            for (int i = 1; i < 5 && spalte + i < 7 && zeile - i > 0; i++)
            {
                if (Spielfeld.Feld[zeile - i, spalte + i] == Spielfeld.Feld[zeile, spalte])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End     

            if (diagonal >= 4)
            {
                Spielfeld.GameOver = true;
                return Spielfeld;
            } // if End

            return Spielfeld;
        }

        static void Main(string[] args)
        {
            // Neues Spielfeld 
            Spielfeld Spielfeld = NeuesSpiel();

            // Ausgabe
            Console.WriteLine("Willkommen bei 4 Gewinnt!\n\n" +
                "Um ein neues Spiel zu starten drücke eine beliebige Taste...");
            while (!Console.KeyAvailable) { }

            while (true)
            {
                Spielfeld = NeuesSpiel();

                while (!Spielfeld.GameOver)
                {
                    // 1 Zug setzen
                    Spielfeld = Zug(Spielfeld);

                } // while End
                Console.Clear();

                if (Spielfeld.Spieler == 1)
                    Console.Write("Spieler 2 hat gewonnen!");
                else
                    Console.Write("Spieler 1 hat gewonnen!");

                Spielfeld.GameOver = false;
                Console.Write("\n\n\nDrücke eine beliebige Taste um ein neues Spiel zu starten!");
                while (!Console.KeyAvailable) { }
            } // while End
        }
    }
}
