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
        public ColorInterpolationType interpolation = ColorInterpolationType.Linear;
        public bool squareRootAlpha = false;

        public Color RandomizedColor()
        {
            if (only.a >= 0f)
            {
                return only;
            }

            Color offset = new Color(Rand.Range(min.r, max.r), Rand.Range(min.g, max.g), Rand.Range(min.b, max.b), Rand.Range(min.a, max.a));
            Color res;
            switch (interpolation)
            {
                case ColorInterpolationType.Linear:
                    res = Color.Lerp(a, b, Rand.Range(0f, 1f)) + offset;
                    break;
                default: // SquareRoot
                    res = GetSquareRootInterpolated(Rand.Range(0f, 1f)) + offset;
                    break;
            }
            return new Color(Mathf.Clamp(res.r, 0, 1), Mathf.Clamp(res.g, 0, 1), Mathf.Clamp(res.b, 0, 1), Mathf.Clamp(res.a, 0, 1));
        }

        public Color AverageColor()
        {
            if (only.a >= 0f)
            {
                return only;
            }

            Color offset = new Color((min.r + max.r) / 2f, (min.g + max.g) / 2f, (min.b + max.b) / 2f, (min.a + max.a) / 2f);
            Color res;
            switch (interpolation)
            {
                case ColorInterpolationType.Linear:
                    res = Color.Lerp(a, b, 0.5f) + offset;
                    break;
                default: // SquareRoot
                    res = GetSquareRootInterpolated(0.5f) + offset;
                    break;
            }
            return new Color(Mathf.Clamp(res.r, 0, 1), Mathf.Clamp(res.g, 0, 1), Mathf.Clamp(res.b, 0, 1), Mathf.Clamp(res.a, 0, 1));
        }

        Color GetSquareRootInterpolated(float i)
        {
            Color aRoot = new Color(Mathf.Sqrt(a.r), Mathf.Sqrt(a.g), Mathf.Sqrt(a.b), squareRootAlpha ? Mathf.Sqrt(a.a) : a.a);
            Color bRoot = new Color(Mathf.Sqrt(b.r), Mathf.Sqrt(b.g), Mathf.Sqrt(b.b), squareRootAlpha ? Mathf.Sqrt(b.a) : b.a);
            Color resRoot = Color.Lerp(aRoot, bRoot, i);
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