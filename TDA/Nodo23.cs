namespace TDA
{
    public class Nodo23<T> 
    {
        //Valores en un nodo, (valor 1 > valor 2 al insertar)
        public T Valor1 { get; set; }
        public T Valor2 { get; set; }
        public bool ocupado { get; set; }

        public Nodo23<T> NodoIzq;
        public Nodo23<T> NodoCen;
        public Nodo23<T> NodoDer;
        public Nodo23()
        {
            NodoIzq = null;
            NodoCen = null;
            NodoDer = null;
            ocupado = false;
        }   
        //Constructor para dar un valor al valor menor en el nodo
        public Nodo23(T ValorM)
        {
            NodoIzq = null;
            NodoCen = null;
            NodoDer = null;
            ocupado = false;
            Valor2 = ValorM;
        }


    }
}