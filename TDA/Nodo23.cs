namespace TDA
{
    public class Nodo23<T> where T : IComparable<T>
    {
        public Nodo23<T> Lhijo { get; set; }//dato izquierdo
        public Nodo23<T> Chijo { get; set; }//dato del medio
        public Nodo23<T> Rhijo { get; set; }//dato de la derecha
        public T VIzq { get; set; } //valor izquierdp
        public T VDer { get; set; }//valor derecho


    }
}