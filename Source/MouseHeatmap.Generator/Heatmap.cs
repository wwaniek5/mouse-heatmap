using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MouseHeatmap.Collector;

namespace MouseHeatmap.Generator
{
    public class Heatmap
    {
        private IEnumerable<ScreenUnit> _screenUnits;

        public Heatmap(IEnumerable<ScreenUnit> screenUnits)
        {
            _screenUnits = screenUnits;
        }

        public Bitmap Draw(Func<ScreenUnit, long> selector)
        {
            var maxX = _screenUnits.Max(screenUnit => screenUnit.X);
            var maxY = _screenUnits.Max(screenUnit => screenUnit.Y);

            var bitmap = new Bitmap(maxX + 1, maxY + 1);

            var maxValue = _screenUnits.Max(selector);
            var minValue = _screenUnits.Min(selector);

            foreach (var screenUnit in _screenUnits)
            {
                bitmap.SetPixel(
                    screenUnit.X,
                    screenUnit.Y,
                    TranslateValueToColor(selector(screenUnit), minValue, maxValue));
            };

            return bitmap;
        }

        private static Color TranslateValueToColor(long count, long min, long max)
        {
            double relativeValue = (double)(count - min) / (max - min);

            return Color.FromArgb(
                255,
                (int)(255 * (1 - relativeValue)),
                (int)(255 * relativeValue));

        }   
    }
}