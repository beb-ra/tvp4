using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4
{
    internal class Model
    {
        public int[,] field;
        int _rows, _cols;
        public int whoWalks = 1, countMovesDone = 0;

        public Model(int rows, int cols) 
        { 
            _rows = rows;
            _cols = cols;
            field = new int[rows, cols];
        }

        internal void AddObject(int row, int col)
        {
            if (field[row, col] == 0)
            {
                field[row, col] = whoWalks;
            }
            if (field[row, col] == 2 && whoWalks == 1 || field[row, col] == 1 && whoWalks == 2)
            {
                field[row, col] = whoWalks + 2;
            }

            /*
            string message = "";

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    message += field[i, j].ToString() + " "; 
                }
                message += "\n"; 
            }

            MessageBox.Show(message, "Содержимое массива");
            */
            MoveDone();
        }

        internal bool CanMove(int row, int col, int who)
        {
            if (field[row, col] == 0 || (field[row, col] == 2 && who == 1 || field[row, col] == 1 && who == 2))
            {
                if (row > 0)
                {
                    if (field[row - 1, col] == who || (field[row - 1, col] == who + 2 && IsAliveAround(row-1, col, who)))
                    {
                        return true;
                    }
                    if (col > 0)
                    {
                        if (field[row - 1, col - 1] == who || (field[row - 1, col - 1] == who + 2 && IsAliveAround(row-1, col-1, who)))
                        {
                            return true;
                        }
                    }
                    if (col < _cols - 1)
                    {
                        if (field[row - 1, col + 1] == who || (field[row - 1, col + 1] == who + 2 && IsAliveAround(row-1, col+1, who)))
                        {
                            return true;
                        }
                    } 
                }
                if (col > 0)
                {
                    if (field[row, col - 1] == who || (field[row, col - 1] == who + 2 && IsAliveAround(row,col-1,who)))
                    {
                        return true;
                    }
                    if (row < _rows - 1)
                    {
                        if (field[row + 1, col - 1] == who || (field[row + 1, col - 1] == who + 2 && IsAliveAround(row + 1, col - 1, who)))
                        {
                            return true;
                        }
                    }
                }

                if (row < _rows - 1)
                {
                    if (field[row + 1, col] == who || (field[row + 1, col] == who + 2 && IsAliveAround(row + 1, col, who)))
                    {
                        return true;
                    }
                    if (col < _cols - 1)
                    {
                        if (field[row + 1, col + 1] == who || (field[row + 1, col + 1] == who + 2 && IsAliveAround(row + 1, col + 1, who)))
                        {
                            return true;
                        }
                    }
                }
                if (col < _cols - 1)
                {
                    if (field[row, col + 1] == who || (field[row, col + 1] == who + 2 && IsAliveAround(row, col + 1, who)))
                    {
                        return true;
                    }
                }
            } 

            return false;
        }

        private bool IsAliveAround(int row, int col, int who)
        {
            if (row > 0)
            {
                if (field[row - 1, col] == who)
                {
                    return true;
                }
                if (col > 0)
                {
                    if (field[row - 1, col - 1] == who)
                    {
                        return true;
                    }
                }
                if (col < _cols - 1)
                {
                    if (field[row - 1, col + 1] == who)
                    {
                        return true;
                    }
                }
            }
            if (col > 0)
            {
                if (field[row, col - 1] == who)
                {
                    return true;
                }
                if (row < _rows - 1)
                {
                    if (field[row + 1, col - 1] == who)
                    {
                        return true;
                    }
                }
            }

            if (row < _rows - 1)
            {
                if (field[row + 1, col] == who)
                {
                    return true;
                }
                if (col < _cols - 1)
                {
                    if (field[row + 1, col + 1] == who)
                    {
                        return true;
                    }
                }
            }
            if (col < _cols - 1)
            {
                if (field[row, col + 1] == who)
                {
                    return true;
                }
            }

            return false;
    }

        internal void Init()
        {
            field[0, 0] = 1; field[_rows - 1, _cols - 1] = 2;
        }

        internal bool IsFinall()
        {
            bool isWinRed = true, isWinBlue = true;
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    if (field[i, j] == 1)
                    {
                        isWinBlue = false;
                    }
                    if (field[i, j] == 2)
                    {
                        isWinRed = false;
                    }

                    if (!isWinRed && !isWinBlue) break;
                }
            }


            if (isWinRed && !isWinBlue)
            {
                MessageBox.Show("Красные выиграли");
            }
            if (isWinBlue && !isWinRed)
            {
                MessageBox.Show("Красные выиграли");
            }
            if (isWinBlue && isWinRed)
            {
                MessageBox.Show("Ничья");
            }
            return isWinRed || isWinBlue;
        }

        internal void MoveDone()
        {
            countMovesDone++;
            if (countMovesDone == 3) 
            { 
                countMovesDone = 0;

                if (whoWalks == 1)
                {
                    whoWalks = 2;
                }
                else
                {
                    whoWalks = 1;
                }
            }
        }
    }
}
