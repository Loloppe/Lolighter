using Lolighter.Data.Structure;
using System.Collections.Generic;
using static Lolighter.Info.Helper;

namespace Lolighter.Info
{
    class Pattern
    {
        public static int patternID = -1;

        #region Stream

        public static List<ColorNote> SelectStream(int id)
        {
            List<ColorNote> Stream = new List<ColorNote>();

            if (id != 999)
            {
                patternID = id;
            }
            else
            {
                do
                {
                    id = RandNumber(0, 18);

                } while (id == patternID);

                patternID = id;
            }

            int lay = 0;
            int li;

            switch (patternID)
            {
                case 0: //Generic stream
                    if (RandNumber(0, 2) == 0)
                    {
                        li = 2;
                    }
                    else if (RandNumber(0, 2) == 0)
                    {
                        li = 3;
                    }
                    else
                    {
                        li = 1;
                    }
                    ColorNote note = new ColorNote(0, li, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, (li - 1), Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, li, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, (li - 1), lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 1: //Piano roll
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 2: //Z pattern
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 3: //MIDDLE-cross pattern
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 4: //Inward to outward
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 5: //Piano roll mixed with middle notes
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 6: //W pattern
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 7: //2-2
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 8: //RED spin
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 9: //BLUE spin
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 10: //Generic one-lane stream (got to randomise Line after RED UP).
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 11: //Big cross-spin
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 12: //Triple triple
                    int i;
                    if (RandNumber(0, 2) == 0)
                    {
                        i = 0;
                    }
                    else
                    {
                        i = 1;
                    }
                    note = new ColorNote(0, Line.RIGHT - i, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT - i, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT - i, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT - i, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT - i, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT - i, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT - i, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT - i, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 13: //Outward to inward
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 14: //4 - 2 - 2 - 3
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 15: //3 - 1 - 2 - 3
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 16: //Reversed piano roll
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 17: //Inverted piano roll
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 18: //Inverted blue spin
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.RIGHT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
                case 19: //Inverted red spin
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.LEFT, lay, ColorType.BLUE, CutDirection.UP);
                    Stream.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, lay, ColorType.RED, CutDirection.UP);
                    Stream.Add(note);
                    break;
            }

            return Stream;
        }

        #endregion

        #region randomstream

        public static List<ColorNote> SelectRandomStream(int id)
        {
            List<ColorNote> random = new List<ColorNote>();

            int cut = 1;
            int color = 1;
            int count = 0;

            for (int i = 0; i < 4; i++)
            {
                int line = RandNumber(0, 4);

                ColorNote note = new ColorNote(0, line, Layer.BOTTOM, color, cut);
                random.Add(note);
                if (cut == 1 && count == 1)
                {
                    cut = 0;
                }
                if (color == 1)
                {
                    color = 0;
                }
                else
                {
                    color = 1;
                }
                count++;
            }

            return random;
        }

        #endregion

        #region Complex

        public static List<ColorNote> SelectComplex(int id)
        {
            List<ColorNote> Complex = new List<ColorNote>();

            if (id != 999)
            {
                patternID = id;
            }
            else
            {
                do
                {
                    id = RandNumber(0, 18);
                } while (id == patternID);

                patternID = id;
            }

            switch (patternID)
            {
                case 0: //Complexification by fraies
                    ColorNote note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
                case 1: //Aimer with chelly (EGOIST) by Saut
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 2: //FIRST - AKI AKANE by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 3: //FIRST - AKI AKANE by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
                case 4: //FIRST - AKI AKANE by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 5: //FIRST - AKI AKANE by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    //Not sure if more = better
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
                case 6: //FIRST - AKI AKANE by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
                case 7: //FIRST - AKI AKANE by Saut
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 8: //Kodoku no Kakurenbo - Himeringo by Saut
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 9: //Kodoku no Kakurenbo - Himeringo by Saut
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
                case 10: //Kodoku no Kakurenbo - Himeringo by Saut
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 11: //Kodoku no Kakurenbo - Himeringo by Saut
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 12: //If You Can't Hang - Sleeping With Sirens by Saut
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 13: //If You Can't Hang - Sleeping With Sirens by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    break;
                case 14: //If You Can't Hang - Sleeping With Sirens by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
                case 15: //Your voice so... feat. Such - PSYQUI by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    break;
                case 16: //CANDYYYLAND (Pa's Lam System Remix) - tofubeats by Saut
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.BLUE, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.TOP, ColorType.RED, CutDirection.UP);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.MIDDLE, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.MIDDLE, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
                case 17: // Generic cross-stream
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_LEFT, Layer.BOTTOM, ColorType.BLUE, CutDirection.DOWN_LEFT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.MIDDLE_RIGHT, Layer.BOTTOM, ColorType.RED, CutDirection.DOWN_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.RIGHT, Layer.TOP, ColorType.BLUE, CutDirection.UP_RIGHT);
                    Complex.Add(note);
                    note = new ColorNote(0, Line.LEFT, Layer.TOP, ColorType.RED, CutDirection.UP_LEFT);
                    Complex.Add(note);
                    break;
            }

            return Complex;
        }

        #endregion

        public static List<ColorNote> GetNewPattern(string name, int id)
        {
            switch (name)
            {
                case "RandomStream":
                    return new List<ColorNote>(SelectRandomStream(id));
                case "Complex":
                    return new List<ColorNote>(SelectComplex(id));
            }

            return null;
        }

        public static int GetPatternID()
        {
            return patternID;
        }
    }
}
