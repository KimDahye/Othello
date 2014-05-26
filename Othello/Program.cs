using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckBoard board = new CheckBoard(); //기본 2*2 셋팅 
            int turn = 0; //turn이 짝수면 흰색돌, 홀수면 검정돌
               
            while (board.IsBoardFull() == false)
            {
                Console.Clear();
                Console.WriteLine("현재 {0}번째 턴입니다.짝수턴엔 흰색돌, 홀수턴엔 검은돌입니다.", turn);
                board.PrintScore();
                board.PrintBoard();
                Console.Write("X좌표를 입력하세요: ");
                int X= int.Parse(Console.ReadLine());
                Console.Write("Y좌표를 입력하세요: ");
                int Y = int.Parse(Console.ReadLine());


                if(board.IsAroundEmpty(X,Y) || board.IsThereStone(X,Y))
                {
                    // 그 좌표 주위 아홉칸이 다 비었거나, 이미 거기 스톤이 있거나, 아무것도 먹을 수 없으면
                                      
                    if(board.IsAroundEmpty(X,Y))
                    {
                        Console.WriteLine("주위가 다 비었습니다");
                    }
                    if(board.IsThereStone(X,Y))
                    {
                        Console.WriteLine("그자리에 스톤이 있습니다.");
                    }
                    Console.WriteLine("이곳엔 놓을 수 없습니다. 게임을 진행하시려면 엔터키를 눌러주십시오.");
                    Console.ReadLine();
                    turn ++;
                    continue;

                }else
                {
                    if (board.Change(turn, X, Y))
                    {
                        board.SetStone(X, Y, turn);
                        turn++;
                    }
                    else
                    {
                        Console.WriteLine("먹을 돌이 없습니다.");
                        turn++;
                    }
                   
                }
            }
            
            Console.WriteLine("Game is Over. The winner is {0}.", board.PrintScore());
            Console.ReadLine();

        }
    }
}
