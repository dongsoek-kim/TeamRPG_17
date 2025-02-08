using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public static class Render
    {
        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void ColorWrite(char ch, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(ch);
            Console.ResetColor();
        }

        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void ColorWriteLine(char ch, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(ch);
            Console.ResetColor();
        }

        public static void AnimationWrite(string text, float duration, bool canSkip, ConsoleColor color = ConsoleColor.Gray)
        {
            int count = 0;
            for (count = 0; count < text.Length; count++)
            {
                ColorWrite(text[count], color);
                Thread.Sleep((int)(1000f * duration / text.Length));
                while (Console.KeyAvailable) 
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    if (canSkip)
                    {
                        if ( info.Key != ConsoleKey.None && info.Key != ConsoleKey.Enter) // 엔터는 무시
                        {
                            ColorWrite(text.Substring(count + 1), color);
                            count = text.Length;
                            break;
                        }
                    }
                }
            }
        }

        public static void AnimationWriteLine(string text, float duration,bool canSkip, ConsoleColor color = ConsoleColor.Gray)
        {
            int count = 0;
            for (count = 0; count < text.Length; count++)
            {
                ColorWrite(text[count], color);
                Thread.Sleep((int)(1000f * duration / text.Length));
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    if (canSkip)
                    {
                        if (info.Key != ConsoleKey.None && info.Key != ConsoleKey.Enter) // 엔터는 무시
                        {
                            ColorWrite(text.Substring(count + 1), color);
                            count = text.Length;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine();
        }

    }
}
