using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Rewards.ItemData
{
    public class ColorData
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public ColorData()
        {

        }
        [JsonIgnore()]
        public Color Color { get
            {
                return GetColor();
            }
                set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        public ColorData(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public int GetColorDec()
        {
            var color = GetColor();
            var colorSum = color.R.ToString("X") + color.G.ToString("X") + color.B.ToString("X");
            return int.Parse(colorSum, System.Globalization.NumberStyles.HexNumber);
        }

        public Color GetColor()
        {
            return Color.FromRgb((byte)R, (byte)G, (byte)B);
        }

        public override string ToString()
        {
            return $"color:{R},{G},{B}";
        }
    }
}
