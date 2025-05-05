using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace CGOI
{
    public class ColorGenerator_OptionsInterpolated : ColorGenerator
    {
        public List<ColorOptionInterpolated> options = new List<ColorOptionInterpolated>();

        public override Color ExemplaryColor
        {
            get
            {
                ColorOptionInterpolated colorOption = null;
                for (int i = 0; i < options.Count; i++)
                {
                    if (colorOption == null || options[i].weight > colorOption.weight)
                    {
                        colorOption = options[i];
                    }
                }
                if (colorOption == null)
                {
                    return Color.white;
                }
                return colorOption.AverageColor();
            }
        }

        public override Color NewRandomizedColor()
        {
            return options.RandomElementByWeight((ColorOptionInterpolated pi) => pi.weight).RandomizedColor();
        }
    }
}
