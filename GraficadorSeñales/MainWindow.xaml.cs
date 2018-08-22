using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            plnGrafica.Points.Add(new Point(50, 10));
            plnGrafica.Points.Add(new Point(100, 50));
            plnGrafica.Points.Add(new Point(150, 50));
            plnGrafica.Points.Add(new Point(200, 30));
            plnGrafica.Points.Add(new Point(250, 10));
            plnGrafica.Points.Add(new Point(300, 25));
            plnGrafica.Points.Add(new Point(350, 55));
            plnGrafica.Points.Add(new Point(400, 11));
            plnGrafica.Points.Add(new Point(450, 11));
            plnGrafica.Points.Add(new Point(550, 11));
            plnGrafica.Points.Add(new Point(650, 11));
            plnGrafica.Points.Add(new Point(750, 11));
            plnGrafica.Points.Add(new Point(850, 11));
            plnGrafica.Points.Add(new Point(1050, 11));

        }
    }
}
