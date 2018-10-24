using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    abstract class Señal
    {
        public List<Muestra> Muestras { get; set; }
        public double AmplitudMaxima { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }

        public abstract double evaluar(double tiempo);

        public void construirSeñalDigital()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;
            for (double i = TiempoInicial; i <= TiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = evaluar(i);

                Muestras.Add(new Muestra(i, valorMuestra));

                if (Math.Abs(valorMuestra) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(valorMuestra);
                }
              
                Muestras.Add(new Muestra(i, valorMuestra));

            }
        }
        public void escalar(double factor)
        {
            foreach(Muestra muestra in Muestras)
            {
                muestra.Y *= factor;
                
            } 
        }
        public void desplazar(double desplazamiento)
        {
            foreach (Muestra muestra in Muestras)
            {
                muestra.Y += desplazamiento;
            }
        }
        public void actualizarAmplitudMaxima()
        {
            AmplitudMaxima = 0;
            foreach(Muestra muestra in Muestras)
            {
                if(Math.Abs(muestra.Y) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(muestra.Y);
                }
            }
        }

        public void truncar(double n)
        {
            foreach(Muestra muestra in Muestras)
            {
                if(Math.Abs(muestra.Y) > n )
                {
                    muestra.Y = n;

                }
                else if(Math.Abs(muestra.Y) < -n)
                {
                    muestra.Y = -n;
                }
            }
        }

        public static Señal sumar(Señal sumado1, Señal sumado2)
        {
            SeñalPersonalizada resultado = new SeñalPersonalizada();
            resultado.TiempoInicial = sumado1.TiempoInicial;
            resultado.TiempoFinal = sumado1.TiempoFinal;
            resultado.FrecuenciaMuestreo = sumado1.FrecuenciaMuestreo;

            int indice = 0;
            foreach(Muestra muestra in sumado1.Muestras)
            {
                Muestra muestraResultado = new Muestra();
                muestraResultado.X = muestra.X;
                muestraResultado.Y = muestra.Y + sumado2.Muestras[indice].Y;
                indice++;
                resultado.Muestras.Add(muestraResultado);
            }
            return resultado; 
        }

        public static Señal multiplicar(Señal multiplicado1, Señal multiplicado2)
        {
            SeñalPersonalizada resultado = new SeñalPersonalizada();
            resultado.TiempoInicial = multiplicado1.TiempoInicial;
            resultado.TiempoFinal = multiplicado1.TiempoFinal;
            resultado.FrecuenciaMuestreo = multiplicado1.FrecuenciaMuestreo;

            int indice = 0;
            foreach (Muestra muestra in multiplicado1.Muestras)
            {
                Muestra muestraResultado = new Muestra();
                muestraResultado.X = muestra.X;
                muestraResultado.Y = muestra.Y * multiplicado2.Muestras[indice].Y;
                indice++;
                resultado.Muestras.Add(muestraResultado);
            }
            return resultado;
        }
        
    }
}
