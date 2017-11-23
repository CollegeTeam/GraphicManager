using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicManager
{
    public partial class MainForm : Form
    {
        Graphic Graphic;
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            Graphic = new Graphic(PN_Graphic);
            MakeCoordinatesExample2();
        }
        private void MakeCoordinatesExample1()
        {
            Graphic.Title = "Some data";
            Graphic.Coordinates.Add(new Coordinate { X = 0, Y = 50 });
            Graphic.Coordinates.Add(new Coordinate { X = 10, Y = 40 });
            Graphic.Coordinates.Add(new Coordinate { X = 20, Y = 15 });
            Graphic.Coordinates.Add(new Coordinate { X = 30, Y = 10 });
            Graphic.Coordinates.Add(new Coordinate { X = 40, Y = 25 });
            Graphic.Coordinates.Add(new Coordinate { X = 50, Y = 35 });
            Graphic.Coordinates.Add(new Coordinate { X = 60, Y = 30 });
            Graphic.Coordinates.Add(new Coordinate { X = 70, Y = 50 });
            Graphic.Coordinates.Add(new Coordinate { X = 80, Y = 65 });
            Graphic.Coordinates.Add(new Coordinate { X = 90, Y = 90 });
            Graphic.Coordinates.Add(new Coordinate { X = 100, Y = 80 });
        }
        private void MakeCoordinatesExample2()
        {
            Graphic.Title = "Random values";
            Random rnd = new Random();
            for (int x = -100; x <= 200; x++)
            {
                int y = rnd.Next(10, 90);
                Graphic.Coordinates.Add(new Coordinate { X = x, Y = y });
            }

        }
        private void MakeCoordinatesExample3()
        {
            Graphic.Title = "Sinus party";
            Graphic.SmoothCoordinates = true;
            Graphic.AxeX.AxeLabel = "X axis";
            Graphic.AxeY.AxeLabel = "Sinus fonction";
            for (double x = -100; x <= 200; x+=0.1)
            {
                Graphic.Coordinates.Add(new Coordinate { X = x, Y = Math.Sin(x/5.0) * Math.Sin(x /1.4) * Math.Sin(x / 1.5) * 40.0 + 50.0 });
            }
        }

        private void MnuAdoucirCourbe_Click(object sender, EventArgs e)
        {
            Graphic.SmoothCoordinates = !Graphic.SmoothCoordinates;
        }

        private void MnuShowGrille_Click(object sender, EventArgs e)
        {
            Graphic.AxeX.ShowGrid = !Graphic.AxeX.ShowGrid;
            Graphic.AxeY.ShowGrid = !Graphic.AxeY.ShowGrid;
        }
    }
}
