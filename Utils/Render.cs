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

        public static void AnimationWrite(string text, float duration, ConsoleColor color = ConsoleColor.Gray)
        {
            for (int i = 0; i < text.Length; i++)
            {
                ColorWrite(text[i], color);
                Thread.Sleep((int)(1000f * duration / text.Length));
                while (Console.KeyAvailable) { Console.ReadKey(true); } // 키보드 버퍼삭제
            }
        }

        public static void AnimationWriteLine(string text, float duration, ConsoleColor color = ConsoleColor.Gray)
        {
            for (int i = 0; i < text.Length; i++)
            {
                ColorWrite(text[i], color);
                Thread.Sleep((int)(1000f * duration / text.Length));
                while (Console.KeyAvailable) { Console.ReadKey(true); } // 키보드 버퍼삭제
            }
            Console.WriteLine();
        }

    }
}
