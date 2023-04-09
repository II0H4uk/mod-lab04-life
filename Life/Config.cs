using System;
using System.Collections.Generic;
using System.Text;

namespace cli_life
{
    class Config
    {
        public int width;
        public int height;
        public int cellSize;
        public double liveDensity;

        public Config(string JSON)
        {
            string[] a = JSON.Split(new char[] { '\r', '\n', ' ', '\\', '{', '}', '\"', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
            width = int.Parse(a[1]);
            height = int.Parse(a[3]);
            cellSize = int.Parse(a[5]);
            a[7] = a[7].Replace(".", ",");
            liveDensity = double.Parse(a[7]);
        }
    }
}
