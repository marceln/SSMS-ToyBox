using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ToyBox.UI.CustomControls
{
    internal static class ColorTheme
    {
        public static readonly Color ControlBackgroundColor = Color.FromArgb(245, 245, 245);
        public static readonly Brush ControlBackgroundBrush = new SolidBrush(ControlBackgroundColor);

        public static readonly Color SelectionColor = Color.FromArgb(236, 240, 241);
        public static readonly Brush SelectionBrush = new SolidBrush(SelectionColor);

        public static readonly Color ThemeHue = Color.FromArgb(39, 174, 96);

        public static readonly Color LookupAutocompletionColor = Color.FromArgb(26, 188, 156);

        public static readonly Color BadgeForeColor = Color.FromArgb(52, 152, 219);
        public static readonly Brush BadgeForeBrush = new SolidBrush(BadgeForeColor);
        public static readonly Color BadgeBackColor = Color.FromArgb(149, 165, 166);
        public static readonly Brush BadgeBackBrush = new SolidBrush(BadgeBackColor);
    }
}
