using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetcodeValidSudoku
{
    public class Solution
    {
        private Dictionary<string, int> ColumnCountDictionary { get; set; }
        private Dictionary<string, int> RowCountDictionary { get; set; }
        private Dictionary<string, int> CellCountDictionary { get; set; }

        public bool IsValidSudoku(char[][] board)
        {
            ColumnCountDictionary = new Dictionary<string, int>();
            RowCountDictionary = new Dictionary<string, int>();
            CellCountDictionary = new Dictionary<string, int>();

            AnalyzeBoard(board);

            return
                AllCellsHaveUniqueNumbers() &&
                AllRowsHaveUniqueNumbers() &&
                AllColumnsHaveUniqueNumbers();
        }



        private void AnalyzeBoard(char[][] board)
        {
            for(int i=0; i<9; i++)
                for (int j=0; j<9; j++)
                {
                    var number = board[i][j];
                    if (number >= '0' && number <='9')
                    {
                        Increment(ColumnCountDictionary, (j + "_" + number));
                        Increment(RowCountDictionary, (i + "_" + number));

                        var whatCellIsNumberIn = GetCellNumber(j,i);

                        Increment(CellCountDictionary, (whatCellIsNumberIn + "_" + number));
                    }
                }
        }

        private int GetCellNumber(int x, int y)
        {
            if (y<3)
            {
                return x / 3;
            }

            if (y<6)
            {
                return 3 + (x / 3); 
            }

            if (y<9)
            {
                return 6 + (x / 3);
            }

            throw new InvalidOperationException("Not meant to happen!");
        }

        private void Increment(IDictionary<string, int> dictionary, string key)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = 1;
            else
                dictionary[key]++;
        }


        private bool AllCellsHaveUniqueNumbers()
        {
            return CellCountDictionary.All(x => x.Value <= 1);
        }

        private bool AllColumnsHaveUniqueNumbers()
        {
            return ColumnCountDictionary.All(x => x.Value <= 1);
        }

        private bool AllRowsHaveUniqueNumbers()
        {
            return RowCountDictionary.All(x => x.Value <= 1);
        }
    }
}
