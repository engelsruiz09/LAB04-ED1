using TDA;

namespace LAB04_ED1.Models.Datos
{
    public class Singleton
    {
        public int flag;
        public TDA.Arbol2_3<Vehiculo> Arbol_2_3 = new TDA.Arbol2_3<Vehiculo>();
        public List<Vehiculo> lista_arbol = new List<Vehiculo>();
        public List<Vehiculo> lista_busquedas = new List<Vehiculo>();
        public TDA.Nodo23<Vehiculo> lista_ = new Nodo23<Vehiculo> ();
        
        private static Singleton _instance = null;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }

    }
}
