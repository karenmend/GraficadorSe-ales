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
            //CASTING
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);


            SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);

            double periodoMuestreo = 1 / frecuenciaMuestreo;
            plnGrafica.Points.Clear();
            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                //
                double valorMuestra = señal.evaluar(i);
                señal.Muestras.Add(new Muestra(i, valorMuestra));
                if (Math.Abs(valorMuestra) > señal.AmplitudMaxima)
                {
                    señal.AmplitudMaxima = Math.Abs(valorMuestra);
                }
                //
            }
                //Recorrer una coleccion o arreglo, solo sirve cuando quieres recorrer todos los elementos.
                //Por cada iteracion se guardara un elemento conforme a la coleccion. (FOREACH)
                //Declarar la variable del tipo de dato que va recorrer
                foreach (Muestra muestra in señal.Muestras)
                {
                    plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width,
                   (muestra.Y / señal.AmplitudMaxima) * (((scrContenedor.Height / 2.0) - 30) * - 1) + (scrContenedor.Height / 2))
                   );
                }

            // (muestra.Y / señal.AmplitudMaxima) escalo la muestra

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
