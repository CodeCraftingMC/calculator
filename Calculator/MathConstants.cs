using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class MathConstants
    {
        public static double PI
        {
            get
            {
                if(_pi == 0)
                {
                    if (File.Exists("pi.txt") && double.TryParse(File.ReadAllText("pi.txt"), out double pi))
                    {
                        _pi = pi;
                    }
                    else
                    {
                        _pi = double.Pi;
                    }
                }
                return _pi;
            }
        }
        private static double _pi;

        public static double E
        {
            get
            {
                if (_e == 0)
                {
                    if (File.Exists("e.txt") && double.TryParse(File.ReadAllText("e.txt"), out double e))
                    {
                        _e = e;
                    }
                    else
                    {
                        _e = double.E;
                    }
                }
                return _e;
            }
        }
        private static double _e;

        public static double GoldenRatio
        {
            get
            {
                if (_goldenRatio == 0)
                {
                    if (File.Exists("goldenratio.txt") && double.TryParse(File.ReadAllText("goldenratio.txt"), out double goldenRatio))
                    {
                        _goldenRatio = goldenRatio;
                    }
                    else
                    {
                        _goldenRatio = double.E;
                    }
                }
                return _goldenRatio;
            }
        }
        private static double _goldenRatio;
    }
}
