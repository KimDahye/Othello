using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    class CheckBoard
    {
        Stone[,] board = new Stone[8, 8];

        public CheckBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if ((i == 0 || i == 7) || (j == 0 || j == 7))
                    {
                        board[i, j] = new Stone("Red", i, j);
                        continue;
                    }
                    board[i, j] = new Stone(null, i, j);
                }
            }

            board[3, 3] = new Stone("White", 3, 3);
            board[3, 4] = new Stone("Black", 3, 4);
            board[4, 3] = new Stone("Black", 4, 3);
            board[4, 4] = new Stone("White", 4, 4);
        }

        public void SetStone(int x, int y, int turn)
        {
            if (turn % 2 == 0)
            {
                board[x, y] = new Stone("White", x, y);
            }
            else
            {
                
                board[x, y] = new Stone("Black", x, y);
                
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine(" Y |1 2 3 4 5 6");
            Console.WriteLine("X  |");
            Console.WriteLine("-----------------");
            for (int i = 1; i < board.GetLength(0)-1; i++)
            {
                Console.Write("{0}  |", i);
                for (int j = 1; j < board.GetLength(1)-1; j++)
                {
                    Console.Write("{0} ", board[i,j].PrintStone());
                }
                Console.WriteLine("|");   
            }
            Console.WriteLine("-----------------");
        }

        public bool IsBoardFull()
        {
            for (int i = 1; i < board.GetLength(0)-1; i++)
            {
                for (int j = 1; j < board.GetLength(1)-1; j++)
                {
                    if(board[i,j].GetColor() == null)
                    {
                        return false;
                    }
                
                }
            }
            return true;   
        }

        public bool IsThereStone(int x, int y)
        {
            if (board[x, y].GetColor() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Change(int turn, int x, int y)
        {
            if (turn % 2 == 0)
            {
                //백돌이 놓여진 턴
                bool result1 = Change_1(x - 1, y - 1, turn % 2);
                bool result2 = Change_2(x - 1, y, turn % 2);
                bool result3 = Change_3(x - 1, y + 1, turn % 2);
                bool result4 = Change_4(x, y + 1, turn % 2);
                bool result5 = Change_5(x + 1, y + 1, turn % 2);
                bool result6 = Change_6(x + 1, y, turn % 2);
                bool result7 = Change_7(x + 1, y - 1, turn % 2);                
                bool result8 = Change_8(x, y - 1, turn % 2);
                
                return result1 || result2 || result3 || result4 || result5 || result6 || result7 || result8;
            }
            else
            {
                //흑돌이 놓여진 턴
                bool result1 = Change_1(x - 1, y - 1, turn % 2);
                bool result2 = Change_2(x - 1, y, turn % 2);
                bool result3 = Change_3(x - 1, y + 1, turn % 2);
                bool result4 = Change_4(x, y + 1, turn % 2);
                bool result5 = Change_5(x + 1, y + 1, turn % 2);
                bool result6 = Change_6(x + 1, y, turn % 2);
                bool result7 = Change_7(x + 1, y - 1, turn % 2);
                bool result8 = Change_8(x, y - 1, turn % 2);

                return result1 || result2 || result3 || result4 || result5 || result6 || result7 || result8;
            }
        }

       


        public bool IsAroundEmpty(int x, int y)
        {

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }

                    if ((board[i, j].GetColor() != null) && (board[i,j].GetColor() != "Red"))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public string PrintScore()
        {
            int b = 0;
            int w = 0;
            for (int i = 1; i < board.GetLength(0) -1; i++)
            {
                for (int j = 1; j < board.GetLength(1) -1; j++)
                {
                    if (board[i,j].GetColor() == "Black")
                    {
                        b++;
                    }
                    else if (board[i,j].GetColor() == "White")
                    {
                        w++;
                    }
                }
            }

            Console.WriteLine("현재 스코어 Black: {0}, White: {1}", b, w);
            if (b > w)
            {
                return "Black";
            }
            else if (b < w)
            {
                return "White";
            }
            else
            {
                return "Draw"; 
            }

        }

        public bool Change_1(int x, int y, int turn)
        {
            int n = 0;
            int j = y;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int i = x; i > 0; i--)  // 0을 안가게 해도 되는지 가게 해야하는지는 좀 더 고민해보자!
                {

                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        j--;
                        continue;
                       
                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x - t, y - t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                return false;
            }
            else //검은돌 놓일 때
            {

                for (int i = x; i > 0; i--)
                {

                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        j--;
                        continue;
                        
                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x - t, y - t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }


        }


        public bool Change_2(int x, int y, int turn)
        {
            int n = 0;
            int j = y;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int i = x; i > 0; i--)
                {
                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        continue;
                        
                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x - t, y].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                for (int i = x; i > 0; i--)
                {
                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x - t, y].ChangeColor();
                            }
                            return true;
                        }
                        return false ;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
              
            }
        }

        public bool Change_3(int x, int y, int turn)
        {
            int n = 0;
            int j = y;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int i = x; i > 0; i--)
                {
                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        j++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n; t++)
                            {
                                board[x - t, y +t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for (int i = x; i > 0; i--)
                {
                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        j++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x - t, y+t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            }
        }


        public bool Change_4(int x, int y, int turn)
        {
            int n = 0;
            int i = x;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int j = y; j < 7; j++)
                {
                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x, y + t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                for (int j = y; j < 7; j++)
                {
                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x, y + t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            }
        }

        public bool Change_5(int x, int y, int turn)
        {
            int n = 0;
            int j = y;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int i = x; i < 7; i++) 
                {

                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        j++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x + t, y + t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }

                }

                return false;
            }
            else //검은돌 놓일 때
            {

                for (int i = x; i < 7; i++)
                {

                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        j++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x + t, y + t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        public bool Change_6(int x, int y, int turn)
        {
            int n = 0;
            int j = y;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int i = x; i < 7; i++)
                {
                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n; t++)
                            {
                                board[x + t, y].ChangeColor();
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                for (int i = x; i < 7; i++)
                {
                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x + t, y].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            }
        }

        public bool Change_7(int x, int y, int turn)
        {
            int n = 0;
            int j = y;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int i = x; i < 7; i++)
                {
                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        j--;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x + t, y - t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                for (int i = x; i < 7; i++)
                {
                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        j--;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x + t, y - t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;

            }
        }

        public bool Change_8(int x, int y, int turn)
        {
            int n = 0;
            int i = x;
            if (turn == 0) //흰색 돌 놓일 때
            {
                for (int j = y; j > 0; j--)
                {
                    if (board[i, j].GetColor() == "Black")
                    {
                        n++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "White")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x, y - t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                for (int j = y; j >0 ; j--)
                {
                    if (board[i, j].GetColor() == "White")
                    {
                        n++;
                        continue;

                    }
                    else if (board[i, j].GetColor() == "Black")
                    {
                        if (n > 0)
                        {
                            for (int t = 0; t < n ; t++)
                            {
                                board[x, y - t].ChangeColor();
                            }
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            }
        }


    }
}
