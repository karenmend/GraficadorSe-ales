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


        }

        private void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            //CASTING = Convertir entre tipos de datos.
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);


            Señal señal;
            switch (cbTipoSeñal.SelectedIndex)
            {
                //Senoidal
                case 0:
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                //Rampa
                case 1:
                    señal = new SeñalRampa();
                    break;
                default:
                    señal = null;
                    break;
            }

            //Establacer los valores que va usar la clase antes de que la llame.
            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaMuestreo;
            señal.construirSeñalDigital();

            plnGrafica.Points.Clear();

            
            if (señal != null)
            {
                //Recorrer una coleccion o arreglo, solo sirve cuando quieres recorrer todos los elementos.
                //Por cada iteracion se guardara un elemento conforme a la coleccion. (FOREACH)
                //Declarar la variable del tipo de dato que va recorrer
                foreach (Muestra muestra in señal.Muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width,
                    (muestra.Y / señal.AmplitudMaxima) * (((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2))
                    );
                }
            }
            
           
            //EJE X
            plnEjex.Points.Clear();
            //Punto de Principio
            plnEjex.Points.Add(new Point(0, (scrContenedor.Height / 2)));
            //Punto del fin
            plnEjex.Points.Add(new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width, (scrContenedor.Height / 2)));
            //
        
            //EJE Y
            plnEjey.Points.Clear();
            //Punto de Principio
            plnEjey.Points.Add(new Point((0-tiempoInicial) * scrContenedor.Width,  señal.AmplitudMaxima * (((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            //Punto del fin
            plnEjey.Points.Add(new Point((0-tiempoInicial) * scrContenedor.Width, -señal.AmplitudMaxima * (((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            //

            lblAmplitudMaximaY.Text = señal.AmplitudMaxima.ToString();
            lblAmplitudMazimaNegativaY.Text = "-" + señal.AmplitudMaxima.ToString();
        }

        private void btnGrficarRampa_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            
            SeñalRampa señal = new SeñalRampa();

            double periodoMuestreo = 1 / frecuenciaMuestreo;

            plnGrafica.Points.Clear(); //borra los puntos

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {

                double valorMuestra = señal.evaluar(i);

                señal.Muestras.Add(new Muestra(i, valorMuestra));
            }
               
                foreach (Muestra muestra in señal.Muestras)
                {
                    plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width,
                   muestra.Y * (((scrContenedor.Height / 2.0) -30) * -1) + (scrContenedor.Height / 2)));
                }


            
        }
    }
}
