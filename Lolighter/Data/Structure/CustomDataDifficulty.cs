using System;
using System.Collections.Generic;

namespace Lolighter.Data.Structure
{
    internal class CustomDataDifficulty // For external tools
    {
        public float t { get; set; } // Time
        public List<Bookmark> bm { get; set; } = new(); // Bookmarks
    }

    internal class Bookmark // Bookmarks
    {
        public Bookmark(float b, string n, float[] c)
        {
            this.b = b;
            this.n = n;
            this.c = c;
        }

        public float b { get; set; } = 0; // Time
        public string n { get; set; } = String.Empty; // Text
        public float[] c { get; set; } = new float[4] { 1, 1, 1, 1 }; // Color RGBA
    }
}
