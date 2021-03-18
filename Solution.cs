using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetcodeValidSudoku
{
    public class Solution
    {
        private const int MaxDuplicateNumbersAllowedInAColumn = 1;
        private const int MaxDuplicateNumbersAllowedOnARow = 1;
        private const int MaxDuplicateNumbersAllowedInACell = 1;


        // Used to record quantity of numbers in columns.
        // For example, key "6_2" means "How many number 2's are in column 6?"
        private Dictionary<string, int> ColumnCountDictionary { get; set; }
        private Dictionary<string, int> RowCountDictionary { get; set; }
        private Dictionary<string, int> CellCountDictionary { get; set; }

        public bool IsValidSudoku(char[][] board)
        {
            ColumnCountDictionary = new Dictionary<string, int>();
            RowCountDictionary = new Dictionary<string, int>();
            CellCountDictionary = new Dictionary<string, int>();

            for (int row = 0; row < 9; row++)
                for (int col = 0; col < 9; col++)
                {
                    var number = board[row][col];
                    if (number >= '0' && number <= '9')
                    {
                        if (Increment(ColumnCountDictionary, (col + "_" + number)) > MaxDuplicateNumbersAllowedInAColumn)
                            return false;

                        if (Increment(RowCountDictionary, (row + "_" + number)) > MaxDuplicateNumbersAllowedOnARow)
                            return false;

                        var whatCellIsNumberIn = GetCellNumber(col, row);
                        if (Increment(CellCountDictionary, (whatCellIsNumberIn + "_" + number)) > MaxDuplicateNumbersAllowedInACell)
                            return false;
                    }
                }

            return true;
        }





        // Yes, I know this definitely can be optimised...
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

        private int Increment(IDictionary<string, int> dictionary, string key)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = 1;
            else
                dictionary[key]++;

            return dictionary[key];
        }


    }
}
