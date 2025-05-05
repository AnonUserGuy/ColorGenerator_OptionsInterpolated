using UnityEngine;
using Verse;

namespace CGOI
{
    public class ColorOptionInterpolated
    {
        public float weight = 1f;

        public Color min = new Color(0f, 0f, 0f, 0f);
        public Color max = new Color(0f, 0f, 0f, 0f);
        
        public Color only = new Color(-1f, -1f, -1f, -1f);

        public Color a = new Color(0f, 0f, 0f, 0f);
        public Color b = new Color(0f, 0f, 0f, 0f);
        public ColorInterpolationType interpolation = ColorInterpolationType.SquareRoot;
        public bool squareRootAlpha = false;

        public Color RandomizedColor()
        {
            if (only.a >= 0f)
            {
                return only;
            }

            Color offset = new Color(Rand.Range(min.r, max.r), Rand.Range(min.g, max.g), Rand.Range(min.b, max.b), Rand.Range(min.a, max.a));
            switch (interpolation)
            {
                case ColorInterpolationType.Linear:
                    return Color.Lerp(min, max, Rand.Range(0f, 1f)) + offset;
                default: // SquareRoot
                    return GetSquareRootInterpolated(Rand.Range(0f, 1f)) + offset;
            }
        }

        public Color AverageColor()
        {
            if (only.a >= 0f)
            {
                return only;
            }

            Color offset = new Color((min.r + max.r) / 2f, (min.g + max.g) / 2f, (min.b + max.b) / 2f, (min.a + max.a) / 2f);
            switch (interpolation)
            {
                case ColorInterpolationType.Linear:
                    return Color.Lerp(min, max, 0.5f);
                default: // SquareRoot
                    return GetSquareRootInterpolated(0.5f);
            }
        }

        Color GetSquareRootInterpolated(float i)
        {
            Color minRoot = new Color(Mathf.Sqrt(min.r), Mathf.Sqrt(min.g), Mathf.Sqrt(min.b), squareRootAlpha ? Mathf.Sqrt(min.a) : min.a);
            Color maxRoot = new Color(Mathf.Sqrt(max.r), Mathf.Sqrt(max.g), Mathf.Sqrt(max.b), squareRootAlpha ? Mathf.Sqrt(max.a) : min.a);
            Color resRoot = Color.Lerp(minRoot, maxRoot, i);
            return new Color(resRoot.r * resRoot.r, resRoot.g * resRoot.g, resRoot.b * resRoot.b, squareRootAlpha ? (resRoot.a * resRoot.a) : resRoot.a);
        }

        public void SetSingle(Color color)
        {
            only = color;
        }
        public void SetMin(Color color)
        {
            min = color;
        }
        public void SetMax(Color color)
        {
            max = color;
        }
        public void SetA(Color color)
        {
            a = color;
        }
        public void SetB(Color color)
        {
            b = color;
        }
        public void SetInterpolation(ColorInterpolationType val)
        {
            interpolation = val;
        }
        public void SetSquareRootAlpha(bool val)
        {
            squareRootAlpha = val;
        }
    }
}