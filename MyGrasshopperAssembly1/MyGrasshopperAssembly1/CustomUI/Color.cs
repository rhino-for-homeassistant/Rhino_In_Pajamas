using System.Drawing;

namespace CustomUI
{
    public class SliderColours
    {
        //Set colours for Component UI
        static readonly Color Primary = Color.FromArgb(255, 229, 27, 36);
        static readonly Color Primary_light = Color.FromArgb(255, 255, 93, 78);
        static readonly Color Primary_dark = Color.FromArgb(255, 170, 0, 0);
        public static Brush ButtonColor
        {
            get { return new SolidBrush(Primary); }
        }
        public static Brush DragElementEdge
        {
            get { return new SolidBrush(Primary); }
        }
        public static Color DragElementFill
        {
            get { return Color.FromArgb(255, 244, 244, 244); }
        }
        public static Color RailColour
        {
            get { return Color.FromArgb(255, 164, 164, 164); }
        }
        public static Color ClickedBorderColour
        {
            get { return Primary; }
        }
        public static Color SpacerColour
        {
            get { return Color.DarkGray; }
        }
        public static Brush AnnotationTextDark
        {
            get { return Brushes.Black; }
        }
        public static Brush AnnotationTextBright
        {
            get { return Brushes.White; }
        }
    }

}