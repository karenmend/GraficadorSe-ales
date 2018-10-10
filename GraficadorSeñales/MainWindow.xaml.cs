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


            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            Señal señal;
            switch (cbTipoSeñal.SelectedIndex)
            {
                //Senoidal
                case 0:
                    double amplitud = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtAmplitud.Text);
                    double fase = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtFase.Text);
                    double frecuencia = double.Parse(
                        ((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);

                    break;
                //Rampa
                case 1:
                    señal = new SeñalRampa();
                    break;
                case 2:
                    double alpha = double.Parse(
                         ((ConfiguracionSeñalExponencial)panelConfiguracion.Children[0]).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
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




            //Escalar
            double factorEscala = double.Parse(txtfactorEscalaAmplitud.Text);
            if (checkboxEscalaAmplitud.IsChecked == true)
            {
                
                señal.escalar(factorEscala);

            }
            else if (checkboxEscalaAmplitud.IsChecked == false)
            {
                factorEscala = 1;
                señal.escalar(factorEscala);
            }

            //Desplazar
            double desplazamientoValor = double.Parse(txtDesplazamientoEnY.Text);
            if(checkboxDesplazamientoY.IsChecked == true)
            {
                señal.desplazar(desplazamientoValor);
            }
           else if (checkboxDesplazamientoY.IsChecked == false)
            {
                desplazamientoValor = 0;
                señal.desplazar(desplazamientoValor);

            }

            //Truncar
            if(checkboxUmbral.IsChecked == true)
            {
                double umbralValor = double.Parse(txtUmbral.Text);
                señal.truncar(umbralValor);
            }
            
            //Amplitud
            señal.actualizarAmplitudMaxima();

            

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

            lblAmplitudMaximaY.Text = señal.AmplitudMaxima.ToString("F");
            lblAmplitudMazimaNegativaY.Text = "-" + señal.AmplitudMaxima.ToString("F");
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

        private void cbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelConfiguracion != null)
            {
                panelConfiguracion.Children.Clear();
                switch (cbTipoSeñal.SelectedIndex)
                {
                    case 0: //Senoidal
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                        break;
                    case 1:
                        break;
                    case 2:
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalExponencial());
                        break;
                    default:
                        break;
                }
            }
        }

        private void cbTipoSeñal_Segunda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
