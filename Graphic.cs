using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicManager
{
    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
    public class Graphic
    {
        #region private settings
        private DoubleBufferPanel ParentPanel = null;
        public Axe AxeX;
        public Axe AxeY;
        private Font FontCoordinates = new Font("Arial", 14);
        private Label LBL_Coordinates;
        #endregion

        #region public properties
        public int Margins { get; set; } = 70;
        public Brush BrushTilte { get; set; } = Brushes.Black;
        public Font FontTitle { get; set; } = new Font("Arial", 24);
        public string Title { get; set; } = "Untitled";
        public bool SmoothCoordinates { get; set; } = false;

        public List<Coordinate> Coordinates = new List<Coordinate>();
        #endregion

        #region Intialisation
        public Graphic(DoubleBufferPanel parentPanel)
        {
            ParentPanel = parentPanel;
            AxeX = new Axe(true);
            AxeY = new Axe(false);
            FitAxes();
            SetParentPanelEventHandlers();
            Install_Coordinates_Label();
        }

        private void SetParentPanelEventHandlers()
        {
            if (ParentPanel != null)
            {
                ParentPanel.Paint += ParentPanel_Paint;
                ParentPanel.Resize += ParentPanel_Resize;
                ParentPanel.MouseDown += ParentPanel_MouseDown;
                ParentPanel.MouseMove += ParentPanel_MouseMove;
                ParentPanel.MouseUp += ParentPanel_MouseUp;
                ParentPanel.DoubleClick += ParentPanel_DoubleClick;
                ParentPanel.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region Axes scale stack handling
        Stack<double> AxeXMinimim = new Stack<double>();
        Stack<double> AxeXMaximim = new Stack<double>();
        Stack<double> AxeXIncrement = new Stack<double>();
        Stack<double> AxeYMinimim = new Stack<double>();
        Stack<double> AxeYMaximim = new Stack<double>();
        Stack<double> AxeYIncrement = new Stack<double>();

        private void StackPushScale()
        {
            AxeXMinimim.Push(AxeX.Minimum);
            AxeXMaximim.Push(AxeX.Maximum);
            AxeXIncrement.Push(AxeX.Increment);
            AxeYMinimim.Push(AxeX.Minimum);
            AxeYMaximim.Push(AxeX.Maximum);
            AxeYIncrement.Push(AxeX.Increment);
        }
        private void StackPopScale()
        {
            if (AxeXMinimim.Count > 0)
            {
                AxeX.Minimum = AxeXMinimim.Pop();
                AxeX.Maximum = AxeXMaximim.Pop();
                AxeX.Increment = AxeXIncrement.Pop();
                AxeY.Minimum = AxeYMinimim.Pop();
                AxeY.Maximum = AxeYMaximim.Pop();
                AxeY.Increment = AxeYIncrement.Pop();
                Update_Coordinates_Label_Location();
            }
        }
        #endregion

        #region Graphic transformation

        private void FitAxes()
        {
            Point origin = new Point(Margins, ParentPanel.ClientSize.Height - Margins);
            AxeX.Origin = origin;
            AxeY.Origin = origin;
            AxeX.Size = new Size(ParentPanel.ClientSize.Width - 2 * Margins, ParentPanel.ClientSize.Height - 2 * Margins);
            AxeY.Size = AxeX.Size;
            ParentPanel.Refresh();
        }

        private int ScaleSizeTolerance = 10;
        private void ScaleGraphic()
        {
            if ((Math.Abs(MouseDownLocation.X - MouseUpLocation.X) > ScaleSizeTolerance) &&
                (Math.Abs(MouseDownLocation.Y - MouseUpLocation.Y) > ScaleSizeTolerance))
            {
                StackPushScale();

                double AxeX_Minimum = AxeX.GridConstrainedUnit(AxeX.PixelToUnit((int)Math.Min(MouseDownLocation.X, MouseUpLocation.X)));
                double AxeX_Maximum = AxeX.GridConstrainedUnit(AxeX.PixelToUnit((int)Math.Max(MouseDownLocation.X, MouseUpLocation.X)));
                double AxeY_Minimum = AxeY.GridConstrainedUnit(AxeY.PixelToUnit((int)Math.Max(MouseDownLocation.Y, MouseUpLocation.Y)));
                double AxeY_Maximum = AxeY.GridConstrainedUnit(AxeY.PixelToUnit((int)Math.Min(MouseDownLocation.Y, MouseUpLocation.Y)));

                AxeX.Minimum = AxeX_Minimum;
                AxeX.Maximum = AxeX_Maximum;
                AxeX.Increment = (AxeX_Maximum - AxeX_Minimum) / 10;
                AxeY.Minimum = AxeY_Minimum;
                AxeY.Maximum = AxeY_Maximum;
                AxeY.Increment = (AxeY_Maximum - AxeY_Minimum) / 10;


                if (!AxeX.IsValid() || !AxeY.IsValid())
                    StackPopScale();
                Update_Coordinates_Label_Location();
            }
        }
        private void ParentPanel_Resize(object sender, EventArgs e)
        {
            Update_Coordinates_Label_Location();
            FitAxes();
        }
        private void MoveOrigin(Point location)
        {
            double deltaX = AxeX.GridConstrainedUnit(AxeX.PixelToUnit(location.X)) -
                            AxeX.GridConstrainedUnit(AxeX.PixelToUnit(LastMouseLocation.X));
            AxeX.Minimum -= deltaX;
            AxeX.Maximum -= deltaX;
            double deltaY = AxeY.GridConstrainedUnit(AxeY.PixelToUnit(location.Y)) -
                            AxeY.GridConstrainedUnit(AxeY.PixelToUnit(LastMouseLocation.Y));
            AxeY.Minimum -= deltaY;
            AxeY.Maximum -= deltaY;
        }

        #endregion

        #region Cursor coordinates monitoring
        private void Install_Coordinates_Label()
        {
            string coordinates = "X: " + AxeX.UnitToFormatedString(AxeX.PixelToUnit(AxeX.Origin.X + AxeX.Size.Width), true) + " " +
                                 "Y: " + AxeX.UnitToFormatedString(AxeY.PixelToUnit(AxeX.Origin.Y - AxeX.Size.Height), true);
            SizeF sizeCoordinates = ParentPanel.CreateGraphics().MeasureString(coordinates, FontCoordinates);
            LBL_Coordinates = new Label();
            LBL_Coordinates.Text = "";
            LBL_Coordinates.Font = FontCoordinates;
            LBL_Coordinates.ForeColor = Color.Blue;
            LBL_Coordinates.BorderStyle = BorderStyle.Fixed3D;
            LBL_Coordinates.TextAlign = ContentAlignment.MiddleRight;
            LBL_Coordinates.Size = new Size((int)sizeCoordinates.Width + (int)FontCoordinates.Size / 2, (int)sizeCoordinates.Height + (int)FontCoordinates.Size);
            LBL_Coordinates.Location = new Point((int)(ParentPanel.ClientSize.Width - sizeCoordinates.Width) - Margins, Margins - (int)sizeCoordinates.Height);
            ParentPanel.Controls.Add(LBL_Coordinates);
        }

        private void Update_Coordinates_Label_Location()
        {
            string coordinates = "X: " + AxeX.UnitToFormatedString(AxeX.PixelToUnit(AxeX.Origin.X + AxeX.Size.Width), true) + " " +
                                 "Y: " + AxeX.UnitToFormatedString(AxeY.PixelToUnit(AxeX.Origin.Y - AxeX.Size.Height), true);
            SizeF sizeCoordinates = ParentPanel.CreateGraphics().MeasureString(coordinates, FontCoordinates);
            LBL_Coordinates.Text = "";
            LBL_Coordinates.Size = new Size((int)sizeCoordinates.Width + (int)FontCoordinates.Size / 2, (int)sizeCoordinates.Height + (int)FontCoordinates.Size);
            LBL_Coordinates.Location = new Point((int)(ParentPanel.ClientSize.Width - sizeCoordinates.Width) - Margins, Margins - (int)sizeCoordinates.Height);
        }
        private void UpdateCoordinatesLabel(Point mouseLocation)
        {
            LBL_Coordinates.Text = "X: " + AxeX.UnitToFormatedString(AxeX.PixelToUnit(mouseLocation.X), true) + " " +
                                   "Y: " + AxeX.UnitToFormatedString(AxeY.PixelToUnit(mouseLocation.Y), true);
        }
        #endregion

        #region Drawing 
        private void DrawSelectionRect(Graphics DC)
        {
            Point Location = new Point((int)Math.Min(AxeX.GridConstrainedPixel(MouseDownLocation.X), AxeX.GridConstrainedPixel(LastMouseLocation.X)),
                                       (int)Math.Min(AxeY.GridConstrainedPixel(MouseDownLocation.Y), AxeY.GridConstrainedPixel(LastMouseLocation.Y)));

            Size Size = new Size((int)Math.Abs(AxeX.GridConstrainedPixel(LastMouseLocation.X) - AxeX.GridConstrainedPixel(MouseDownLocation.X)),
                                 (int)Math.Abs(AxeY.GridConstrainedPixel(LastMouseLocation.Y) - AxeY.GridConstrainedPixel(MouseDownLocation.Y)));

            Rectangle selectionRect = new Rectangle(Location, Size);
            DC.DrawRectangle(Pens.Aqua, selectionRect);
        }
        private void DrawCrossHair(Graphics DC)
        {
            Point Location = new Point(AxeX.GridConstrainedPixel(LastMouseLocation.X), AxeY.GridConstrainedPixel(LastMouseLocation.Y));
            DC.DrawLine(Pens.Aqua, Location.X, AxeX.Origin.Y, Location.X, AxeX.Origin.Y - AxeX.Size.Height);
            DC.DrawLine(Pens.Aqua, AxeX.Origin.X, Location.Y, AxeX.Origin.X + AxeX.Size.Width, Location.Y);
        }
        private void DrawTitle(Graphics DC)
        {
            SizeF titleSize = DC.MeasureString(Title, FontTitle);
            PointF titleLocation = new PointF(ParentPanel.ClientSize.Width / 2 - titleSize.Width / 2, 0);
            DC.DrawString(Title, FontTitle, BrushTilte, titleLocation);
        }
        private void DrawCoordinates(Graphics DC)
        {
            List<Point> points = new List<Point>();
            foreach (Coordinate coordinate in Coordinates)
            {
                points.Add(new Point { X = AxeX.UnitToPixel(coordinate.X), Y = AxeY.UnitToPixel(coordinate.Y) });
            }
            DC.SetClip(new Rectangle(AxeX.Origin.X, AxeX.Origin.Y - AxeX.Size.Height, AxeX.Size.Width, AxeX.Size.Height));

            Pen curvePen = new Pen(Color.Black, 4);
            curvePen.StartCap = curvePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            curvePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            if (SmoothCoordinates)
                DC.DrawCurve(curvePen, points.ToArray());
            else
                DC.DrawLines(curvePen, points.ToArray());
            DC.ResetClip();
        }
        private void ParentPanel_Paint(object sender, PaintEventArgs e)
        {
            if (AxeX.IsValid() && AxeY.IsValid())
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                DrawTitle(e.Graphics);

                AxeX.Draw(e.Graphics);
                AxeY.Draw(e.Graphics);

                if (MouseIsDown)
                    if (ScaleMode)
                        DrawSelectionRect(e.Graphics);
                    else
                        DrawCrossHair(e.Graphics);

                DrawCoordinates(e.Graphics);
            }
        }
        #endregion

        #region Mouse events handlers
        private Point MouseDownLocation;
        private Point MouseUpLocation;
        private Point LastMouseLocation;
        private bool MouseIsDown = false;
        private bool ScaleMode = false;

        private void ParentPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ScaleMode = Control.ModifierKeys == Keys.Control;
            MouseIsDown = true;
            LastMouseLocation = MouseDownLocation = e.Location;
        }
        private void ParentPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Location.X != LastMouseLocation.X) || (e.Location.Y != LastMouseLocation.Y))
            {
                MouseUpLocation = e.Location;
                UpdateCoordinatesLabel(e.Location);
                if (MouseIsDown)
                {
                    if (ScaleMode)
                        ParentPanel.Cursor = Cursors.Cross;
                    else
                    {
                        ParentPanel.Cursor = Cursors.SizeAll;
                        MoveOrigin(e.Location);
                    }
                    LastMouseLocation = e.Location;
                    ParentPanel.Refresh();
                }
            }
        }
        private void ParentPanel_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;

            if (ScaleMode)
            {
                ScaleGraphic();
            }
            else
            {
                MoveOrigin(e.Location);
            }
            LastMouseLocation = e.Location;
            ScaleMode = false;
            ParentPanel.Cursor = Cursors.Arrow;
            ParentPanel.Refresh();
        }

        private void ParentPanel_DoubleClick(object sender, EventArgs e)
        {
            StackPopScale();
            ParentPanel.Refresh();
        }
        #endregion
    }
}
