using System;

namespace Lolighter.Data.Structure
{
    internal class CustomDataDifficulty // For external tools
    {
        public float t { get; set; } // Time
        public Bookmark[] bm { get; set; } = Array.Empty<Bookmark>(); // Bookmarks
    }

    internal class Bookmark // Bookmarks
    {
        public Bookmark(float time, string name, float[] color)
        {
            this.b = time;
            this.n = name;
            this.c = color;
        }

        public float b { get; set; } = 0; // Time
        public string n { get; set; } = String.Empty; // Text
        public float[] c { get; set; } = new float[4] { 1, 1, 1, 1 }; // Color RGBA
    }
}
