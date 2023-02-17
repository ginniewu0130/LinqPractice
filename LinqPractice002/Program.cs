using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice002
{
    internal class Program
    {
        static void Main(string[] args)
        {              
            //重複遊玩的迴圈
            bool playAgain=true;
            while(playAgain)
            {
                Console.WriteLine("歡迎來到1A2B猜數字的遊戲~ ");
                //產生四位數
                Random random = new Random();
                ////可以將測試用產生數字那段註解後用這行來產生4位數
                //int[] answer = Enumerable.Range(0, 10).OrderBy(x => random.Next()).Take(4).ToArray();
                
                //測試用產生數字的方式(偶爾會有重複數字的狀況發生，待改)
                int[] answer = new int[4];
                for (int i = 0; i < answer.Length; i++)
                {
                    answer[i] = random.Next(0, 10);
                    //答案
                    Console.Write(answer[i]);
                }
                Console.WriteLine();

                //儲存輸入的數
                int[] guess = new int[4];
                //重複執行到猜對
                while (true)
                {
                    Console.Write("-----\n請輸入四個數字：");
                    string input = Console.ReadLine();

                    //防呆
                    //輸入超過4位數從新輸入
                    if (input.Length != 4)
                    {
                        Console.WriteLine("輸入錯誤，請輸入四位數的數字！");
                        continue;
                    }
                    //輸入重複數字從新輸入
                    bool isDuplicate = input.Distinct().Count() < input.Length;
                    if (isDuplicate)
                    {
                        Console.WriteLine("輸入錯誤，請輸入不重複的數字！");
                        continue;
                    }

                    guess = input.Select((x) => int.Parse(x.ToString())).ToArray();
                    //for (int i = 0; i < guess.Length; i++)
                    //{
                    //    guess[i] = int.Parse(input[i].ToString());
                    //}

                    //判斷幾A幾B
                    int aCount = answer.Where((num, index) => num == guess[index]).Count();
                    int bCount = answer.Intersect(guess).Count() - aCount;
                    //int aCount = 0, bCount = 0;
                    //for (int i = 0; i < answer.Length; i++)
                    //{
                    //    if (guess[i] == answer[i])
                    //    {
                    //        aCount++;
                    //    }
                    //    else if (answer.Contains(guess[i]))
                    //    {
                    //        bCount++;
                    //    }
                    //}
                    Console.WriteLine($"判定結果是{aCount}A{bCount}B");
                    
                    if (aCount == 4)
                    {
                        Console.WriteLine("恭喜你!猜對了！!");
                        break;
                    }
                    
                }
                Console.WriteLine("你要繼續玩嗎(y/n):");
                string yOrn = Console.ReadLine();
                if (yOrn == "y")
                {
                    playAgain = true;
                }
                else
                {
                    Console.WriteLine("遊戲結束，下次再來玩喔~");
                    break;
                }
            }
        }
    }
}
