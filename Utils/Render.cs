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
        /// <summary>
        /// 색을 추가하여 출력하는 메서드 ( Write )
        /// </summary>
        /// <param name="text">출력 데이터</param>
        /// <param name="color">색</param>
        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// 색을 추가하여 출력하는 메서드 ( Write )
        /// </summary>
        /// <param name="ch">출력 데이터</param>
        /// <param name="color">색</param>
        public static void ColorWrite(char ch, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(ch);
            Console.ResetColor();
        }

        /// <summary>
        /// 색을 추가하여 출력하는 메서드 ( WriteLine )
        /// </summary>
        /// <param name="text">출력 데이터</param>
        /// <param name="color">색</param>
        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// 색을 추가하여 출력하는 메서드 ( WriteLine )
        /// </summary>
        /// <param name="ch">출력 데이터</param>
        /// <param name="color">색</param>
        public static void ColorWriteLine(char ch, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(ch);
            Console.ResetColor();
        }

        /// <summary>
        /// 한글자씩 출력되는 애니메이션 텍스트 ( Write )
        /// </summary>
        /// <param name="text">출력 데이터</param>
        /// <param name="duration">시간 *높을수록 느려짐</param>
        /// <param name="canSkip">스킵 가능 여부</param>
        /// <param name="color">색</param>
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

        /// <summary>
        /// 한글자씩 출력되는 애니메이션 텍스트 ( WriteLine )
        /// </summary>
        /// <param name="text">출력 데이터</param>
        /// <param name="duration">시간 *높을수록 느려짐</param>
        /// <param name="canSkip">스킵 가능 여부</param>
        /// <param name="color">색</param>
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
