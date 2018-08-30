using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalSenoidal
    {   //Enacapsulamiento = no exponer variables de manera publica solo hacerlo por medio de metodos.{get;set;}

        //Atributos de clase
        public double Amplitud { get; set; }
        public double Fase { get; set; }
        public double Frecuencia { get; set; }
        public List<Muestra> Muestras { get; set; }

        public double AmplitudMaxima { get; set; }

        
        //Valores Arbitrarios
        public SeñalSenoidal()
        {
            Amplitud = 1.0;
            Fase = 0.0;
            Frecuencia = 1.0;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0; //el valor maxima que va a reflejar en todos los instantes
            //La amplitud maxima va a definir el tamaño del scrollviewer
        }

        //Crear un contructor que lo obligue a establecer las variables.
        //Los constructores se pone el nombre de la clase.
        //Se puede tener mas de un comstructor en una clase pero se tiene que poner distintos parametros.
        public SeñalSenoidal(double amplitud, double fase, double frecuencia)
        {
            Amplitud = amplitud;
            Fase = fase;
            Frecuencia = frecuencia;
            Muestras = new List<Muestra>();

        }

        public double evaluar(double tiempo)
        {
            double resultado;
            resultado = Amplitud * Math.Sin(((2 * Math.PI * Frecuencia) * tiempo) + Fase);

            return resultado;

        }
    }
}
