using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСourseWork
{
    public class TableCup<T>
    {
        private T[,] _table;

        public TableCup(int n, int m) => _table = new T[n, m];

        public TableCup()
        {
            _table = new T[0, 0];
        }
       

        public int Rows
        {
            get { return _table.GetLength(0); }
        }

        public int Columns
        {
            get { return _table.GetLength(1); }
        }

        public T this[int i, int j]
        {
            get { return _table[i, j]; }
            set { _table[i, j] = value; }
        }

        public void AddRow(List<T> row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            if (_table.Length == 0)
            {
                _table = new T[1, row.Count];
                for (int j = 0; j < row.Count; j++)
                {
                    _table[0, j] = row[j];
                }
            }
            else
            {
                T[,] newTable = new T[_table.GetLength(0) + 1, _table.GetLength(1)];
                for (int i = 0; i < _table.GetLength(0); i++)
                {
                    for (int j = 0; j < _table.GetLength(1); j++)
                    {
                        newTable[i, j] = _table[i, j];
                    }
                }

                for (int j = 0; j < row.Count; j++)
                {
                    newTable[_table.GetLength(0), j] = row[j];
                }

                _table = newTable;
            }
        }

        public void AddColumn(List<T> column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }

            if (_table.Length == 0)
            {
                _table = new T[column.Count, 1];
                for (int i = 0; i < column.Count; i++)
                {
                    _table[i, 0] = column[i];
                }
            }
            else
            {
                T[,] newTable = new T[_table.GetLength(0), _table.GetLength(1) + 1];
                for (int i = 0; i < _table.GetLength(0); i++)
                {
                    for (int j = 0; j < _table.GetLength(1); j++)
                    {
                        newTable[i, j] = _table[i, j];
                    }
                }

                for (int i = 0; i < column.Count; i++)
                {
                    newTable[i, _table.GetLength(1)] = column[i];
                }

                _table = newTable;
            }
        }
    }
}
