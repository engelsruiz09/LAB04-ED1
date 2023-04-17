using TDA;

namespace LAB04_ED1.Models.Datos
{
    public class Singleton
    {
        public int flag;
        //public List<ExtensionVehiculo> Aux = new List<ExtensionVehiculo>();
        //public Es_Arboles.ABB<ExtensionVehiculo> ArbolVehiculos = new Es_Arboles.ABB<ExtensionVehiculo>();
        //public Es_Arboles.AVL<ExtensionVehiculo> AVL = new Es_Arboles.AVL<ExtensionVehiculo>();
        public TDA.Arbol2_3<Nodo23<Vehiculos>> Arbol_2_3 = new TDA.Arbol2_3<Nodo23<Vehiculos>>();
        public List<Vehiculos> lista_arbol = new List<Vehiculos>();

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
