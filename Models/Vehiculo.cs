using System.ComponentModel.DataAnnotations;
using TDA;


namespace LAB04_ED1.Models
{
    public class Vehiculo
    {
        //1
        [Required(ErrorMessage = "Debes rellenar este campo"), MinLength(6, ErrorMessage = "El valor debe tener 6 digitos"), MaxLength(6, ErrorMessage = "El valor debe tener 6 digitos")]
        public string Placa { get; set; }

        //2
        [Required(ErrorMessage = "Debes rellenar este campo")]
        public string Color { get; set; }

        //3
        [Required(ErrorMessage = "Debes rellenar este campo")]
        public string Propietario { get; set; }

        //4
        [Required(ErrorMessage = "Debes rellenar este campo")]
        [Range(-90, 90, ErrorMessage = "El valor debe estar entre -90 y 90")]
        public int Latitud { get; set; }

        //5
        [Required(ErrorMessage = "Debes rellenar este campo"), Range(-180, 180, ErrorMessage = "El valor debe estar entre -180 y 180")]
        public int Longitud { get; set; }


        public string Coordenadas;

        public static string ObtenerCoordenadas(Vehiculo vehiculo)
        {
            string longitud = Convert.ToString(vehiculo.Longitud);
            string latitud = Convert.ToString(vehiculo.Latitud);
            string coordenadas = Convert.ToString(longitud) + "," + Convert.ToString(latitud);
            return coordenadas;
        }

        public static Comparison<Vehiculo> compararPlaca = delegate (Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return vehiculo1.Placa.CompareTo(vehiculo2.Placa);
        };

        public static Comparison<Vehiculo> compararPropietario = delegate (Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return vehiculo1.Propietario.CompareTo(vehiculo2.Propietario);
        };

        public static Comparison<Vehiculo> CompararColor = delegate (Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return vehiculo1.Color.CompareTo(vehiculo2.Color);
        };

        public static Comparison<Vehiculo> CompararCoordenadas = delegate (Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return vehiculo1.Coordenadas.CompareTo(vehiculo2.Coordenadas);
        };

        public static Vehiculo editarDato(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            Vehiculo resultante = new Vehiculo();
            vehiculo2.Latitud = vehiculo1.Latitud;
            vehiculo2.Longitud = vehiculo1.Longitud;
            vehiculo2.Coordenadas = ObtenerCoordenadas(vehiculo2);
            resultante = vehiculo2;
            return resultante;
        }

        public static int obtenerPos(Vehiculo vehiculo1, Nodo23<Vehiculo> raiz)
        {
            //la raiz no tiene aún dos valores
            if (!raiz.ocupado)
            {
                if (compararPlaca(vehiculo1, raiz.Valor2) < 0)
                {
                    return 1;
                }
                else if (compararPlaca(vehiculo1, raiz.Valor2) > 0)
                {
                    return 2;
                }
                else
                {
                    //Para el caso que el valor insertado sea igual a algún valor en algún nodo, no se inserta el dato, dado que un árbol 2-3 no acepta valores repetidos
                    return 0;
                }
            }
            //la raíz tiene ya dos valores, está llena y debe balancearse el árbol
            else
            {
                //Para el caso que el valor insertado sea igual a algún valor en algún nodo, no se inserta el dato, dado que un árbol 2-3 no acepta valores repetidos
                if (compararPlaca(vehiculo1, raiz.Valor2) == 0 || compararPlaca(vehiculo1, raiz.Valor1) == 0)
                {
                    return 0;
                }
                else if (compararPlaca(vehiculo1, raiz.Valor2) < 0)
                {
                    return 1;
                }
                else if (compararPlaca(vehiculo1, raiz.Valor2) > 0 && compararPlaca(vehiculo1, raiz.Valor1) < 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }
    }
}
