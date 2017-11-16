using System;
using System.Drawing;
using System.Globalization;

namespace GraphicManager
{
    public class Axe
    {
        #region private settings
        private Size ArrowSize = new Size(6, 4);
        private int TickHeight = 5;
        private int TickLabelMargin = 3;
        private int LabelMargin = 0;
        #endregion

        #region public properties
        public Pen AxePen { get; set; } = Pens.Black;
        public Pen TickPen { get; set; } = Pens.Black;
        public Pen GridPen { get; set; } = Pens.LightGray;
        public Brush TickLabelBrush { get; set; } = Brushes.Black;
        public Brush AxeLabelBrush { get; set; } = Brushes.Black;
        public Font TickLabelFont { get; set; } = new Font("Arial", 10);
        public Font AxeLabelFont { get; set; } = new Font("Arial", 16);
        public string AxeLabel { get; set; }
        public Font Title { get; set; }
        public Point Origin { get; set; }
        public Size Size { get; set; }
        public bool Horizontal { get; set; }
        public double Minimum { get; set; } = 0;
        public double Maximum { get; set; } = 100;
        public double Increment { get; set; } = 5;
        public bool ShowGrid { get; set; } = true;
        #endregion

        #region constructors
        public Axe(bool Horizontal)
        {
            this.Horizontal = Horizontal;
            InitAxeLabel();
            AxePen = new Pen(Color.Black, 2);
        }
        public Axe(Point Origin, Size Size, bool Horizontal, double Minimum = 0, double Maximum = 1, double Increment = 0.1)
        {
            this.Origin = Origin;
            this.Size = Size;
            this.Horizontal = Horizontal;
            this.Minimum = Minimum;
            this.Maximum = Maximum;
            this.Increment = Increment;
            InitAxeLabel();
        }
        private void InitAxeLabel()
        {
            if (Horizontal)
                AxeLabel = "X";
            else
                AxeLabel = "Y";
        }
        #endregion

        #region Unit handling
        public int UnitToPixel(double unit)
        {
            if (Horizontal)
                return (int)Math.Round((unit - Minimum) / (Maximum - Minimum) * Size.Width) + Origin.X;
            else
                return Origin.Y - (int)Math.Round((unit - Minimum) / (Maximum - Minimum) * Size.Height);
        }
        public double PixelToUnit(int pixel)
        {
            if (Horizontal)
                return (double)(pixel - Origin.X) / Size.Width * (Maximum - Minimum) + Minimum;
            else
                return (double)(-pixel + Origin.Y) / Size.Height * (Maximum - Minimum) + Minimum;
        }
        public double GridConstrainedUnit(double unit)
        {
            if (ShowGrid)
            {
                return Math.Truncate((unit + Increment / 2) / Increment) * Increment;
            }
            return unit;
        }
        public int GridConstrainedPixel(int pixel)
        {
            return (Horizontal ? UnitToPixel(GridConstrainedUnit(PixelToUnit(pixel))) :
                                 UnitToPixel(GridConstrainedUnit(PixelToUnit(pixel))));
        }
        public double RoundUnit(double unit, bool extraDecimal = false)
        {
            double roundedUnit = unit;
            int nbDecimals = 0;
            double inc = Increment - (int)Increment;
            if (inc > 0)
            {
                nbDecimals = (int)Math.Ceiling(Math.Log10(1 / inc)) + (extraDecimal ? 1 : 0);
                roundedUnit = Math.Round(unit * Math.Pow(10, nbDecimals)) / Math.Pow(10, nbDecimals);
            }

            return roundedUnit;
        }
        public string MakeNumberFormat(bool extraDecimal = false)
        {
            double inc = Increment - (int)Increment;
            if (inc > 0)
            {
                int nbDecimals = (int)Math.Ceiling(Math.Log10(1 / inc));
                return "{0:0." + new string('0', nbDecimals) + (extraDecimal ? "0" : "") + "}";
            }
            return "{0:0}";
        }
        public string UnitToFormatedString(double unit, bool extraDecimal = false)
        {
            return string.Format(CultureInfo.InvariantCulture, MakeNumberFormat(extraDecimal), RoundUnit(unit, extraDecimal));
        }
        #endregion

        #region Drawing
        private void DrawAxe(Graphics DC)
        {
            Point endPoint = (Horizontal ?
                              new Point(Origin.X + Size.Width, Origin.Y) :
                              new Point(Origin.X, Origin.Y - Size.Height));

            DC.DrawLine(AxePen, Origin, endPoint);
        }
        private void DrawArrow(Graphics DC)
        {
            Point[] arrowPoints = new Point[4];
            int AxePenWidth = (int)AxePen.Width - 1;
            Size arrowSize = new Size(ArrowSize.Width + 3 * AxePenWidth, ArrowSize.Height + 2 * AxePenWidth);
            if (Horizontal)
            {
                arrowPoints[0] = new Point(Origin.X + Size.Width, Origin.Y);
                arrowPoints[1] = new Point(arrowPoints[0].X - arrowSize.Width, Origin.Y - arrowSize.Height / 2);
                arrowPoints[2] = new Point(arrowPoints[0].X - arrowSize.Width, Origin.Y + arrowSize.Height / 2);
            }
            else
            {
                arrowPoints[0] = new Point(Origin.X, Origin.Y - Size.Height);
                arrowPoints[1] = new Point(Origin.X - arrowSize.Height / 2, arrowPoints[0].Y + arrowSize.Width);
                arrowPoints[2] = new Point(Origin.X + arrowSize.Height / 2, arrowPoints[0].Y + arrowSize.Width);
            }
            arrowPoints[3] = arrowPoints[0];

            DC.FillPolygon(new SolidBrush(AxePen.Color), arrowPoints);
            DC.DrawPolygon(AxePen, arrowPoints);
        }
        private SizeF TickLabelSize(Graphics DC)
        {
            string tickLabelMinimum = UnitToFormatedString(Minimum);
            string tickLabelMaximum = UnitToFormatedString(Maximum);
            return DC.MeasureString((tickLabelMaximum.Length > tickLabelMinimum.Length? tickLabelMaximum : tickLabelMinimum), TickLabelFont);
        }
        private double AjustIncrement(Graphics DC)
        {
            double newIncrement = Increment;
            SizeF TicklabelSize = TickLabelSize(DC);

            int MinimumTicksDistance = (int)(Horizontal ? TicklabelSize.Width : TicklabelSize.Height) + 2*TickLabelMargin;

            while (Math.Abs((UnitToPixel(Minimum + newIncrement) - UnitToPixel(Minimum))) < MinimumTicksDistance)
            {
                newIncrement = newIncrement * 2;
            }
            return newIncrement;
        }
        public bool IsValid()
        {
            return (Size.Width > 20) &&
                   (Size.Height > 20) &&
                   (Minimum < Maximum) &&
                   (Increment > 0) &&
                   (Increment < (Maximum - Minimum));
        }
        private void DrawTicks(Graphics DC)
        {
            double ajustedIncrement = AjustIncrement(DC);
            bool skipFirstGrid = true;
            for (double unit = Minimum; unit < Maximum; unit += ajustedIncrement)
            {
                int pixel = UnitToPixel(unit);
                string tickLabel = UnitToFormatedString(unit);

                if ((Horizontal? pixel - Origin.X : -pixel + Origin.Y) < (Horizontal ? Size.Width : Size.Height) - ArrowSize.Width)
                {
                    Point tickStartPoint;
                    Point tickEndPoint;
                    Point gridEndPoint;
                    PointF tickLabelLocation;

                    SizeF TickLabelSize = DC.MeasureString(tickLabel, TickLabelFont);

                    if (Horizontal)
                    {
                        tickStartPoint = new Point(pixel, Origin.Y);
                        tickEndPoint = new Point(pixel, Origin.Y + TickHeight);
                        gridEndPoint = new Point(tickStartPoint.X, Origin.Y - Size.Height);
                        tickLabelLocation = new PointF(tickEndPoint.X - TickLabelSize.Width / 2, tickEndPoint.Y + TickLabelMargin);
                    }
                    else
                    {
                        tickStartPoint = new Point(Origin.X, pixel);
                        tickEndPoint = new Point(Origin.X - TickHeight, pixel);
                        gridEndPoint = new Point(Origin.X + Size.Width, pixel);
                        tickLabelLocation = new PointF(tickEndPoint.X - TickLabelSize.Width - TickLabelMargin, tickEndPoint.Y - TickLabelSize.Height / 2);
                    }

                    if (ShowGrid && !skipFirstGrid)
                        DC.DrawLine(GridPen, tickStartPoint, gridEndPoint);
                    skipFirstGrid = false;

                    DC.DrawLine(TickPen, tickStartPoint, tickEndPoint);
                    DC.DrawString(tickLabel, TickLabelFont, TickLabelBrush, tickLabelLocation);
                }
            }
        }
        private void DrawOrientedText(Graphics DC, String text, Brush brush, Font font, PointF location, int angle)
        {
            SizeF mesuredTextSize = DC.MeasureString(text, font);
            DC.TranslateTransform(location.X, location.Y);
            DC.RotateTransform(angle);
            DC.TranslateTransform(-location.X, -location.Y);
            DC.DrawString(text, font, brush, location);
            DC.ResetTransform();
        }
        private void DrawAxeLabel(Graphics DC)
        {
            PointF axeLabelLocation;
            SizeF axeLabelSize = DC.MeasureString(AxeLabel, AxeLabelFont);
            SizeF tickLabelSize = TickLabelSize(DC);

            if (Horizontal)
            {
                axeLabelLocation = new PointF(Origin.X + Size.Width - axeLabelSize.Width, Origin.Y + tickLabelSize.Height + TickHeight + TickLabelMargin + LabelMargin);
                DC.DrawString(AxeLabel, AxeLabelFont, AxeLabelBrush, axeLabelLocation);
            }
            else
            {
                axeLabelLocation = new PointF(Origin.X - tickLabelSize.Width - TickHeight - axeLabelSize.Height - TickLabelMargin - LabelMargin, Origin.Y - Size.Height + axeLabelSize.Width);
                DrawOrientedText(DC, AxeLabel, AxeLabelBrush, AxeLabelFont, axeLabelLocation, 270);
            }
        }
        public void Draw(Graphics DC)
        {
            if (IsValid())
            {
                DrawAxe(DC);
                DrawArrow(DC);
                DrawTicks(DC);
                DrawAxeLabel(DC);
            }
        }
        #endregion
    }
}
