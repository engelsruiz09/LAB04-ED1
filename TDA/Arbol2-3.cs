using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TDA
{
    public class Arbol2_3<T>
    {
        Nodo23<T> raiz;
        Nodo23<T> Aux;
        Queue<T> cola;
        public List<T> ListaDatos;
        public List<T> Busquedas;
        public Arbol2_3()
        {
            raiz = null;
            cola = new Queue<T>();
            ListaDatos = new List<T>();
            Busquedas = new List<T>();
        }

        Nodo23<T> balanceo(T Valor, Nodo23<T> padre, int i)
        {
            Nodo23<T> nodoNuevo = new Nodo23<T>();
            if (padre.NodoIzq == null && padre.NodoCen == null && padre.NodoDer == null)
            {
                if (i == 1)
                {
                    nodoNuevo.Valor2 = padre.Valor2;
                    nodoNuevo.NodoIzq = new Nodo23<T>(Valor);
                    nodoNuevo.NodoCen = new Nodo23<T>(padre.Valor1);
                }
                else if (i == 2)
                {
                    nodoNuevo.Valor2 = Valor;
                    nodoNuevo.NodoIzq = new Nodo23<T>(padre.Valor2);
                    nodoNuevo.NodoCen = new Nodo23<T>(padre.Valor1);
                }
                else
                {
                    nodoNuevo.Valor2 = padre.Valor1;
                    nodoNuevo.NodoIzq = new Nodo23<T>(padre.Valor2);
                    nodoNuevo.NodoCen = new Nodo23<T>(Valor);
                }
            }
            else
            {
                if (i == 1)
                {
                    nodoNuevo.Valor2 = padre.Valor2;
                    nodoNuevo.NodoIzq = Aux;
                    nodoNuevo.NodoCen = new Nodo23<T>(padre.Valor1);
                    nodoNuevo.NodoCen.NodoIzq = padre.NodoCen;
                    nodoNuevo.NodoCen.NodoCen = padre.NodoDer;
                }
                else if (i == 2)
                {
                    nodoNuevo.Valor2 = Aux.Valor2;
                    nodoNuevo.NodoIzq = new Nodo23<T>(padre.Valor2);
                    nodoNuevo.NodoCen = new Nodo23<T>(padre.Valor1);
                    nodoNuevo.NodoIzq.NodoIzq = padre.NodoIzq;
                    nodoNuevo.NodoIzq.NodoCen = Aux.NodoIzq;
                    nodoNuevo.NodoCen.NodoIzq = Aux.NodoCen;
                    nodoNuevo.NodoCen.NodoCen = padre.NodoDer;
                }
                else
                {
                    nodoNuevo.Valor2 = padre.Valor1;
                    nodoNuevo.NodoIzq = new Nodo23<T>(padre.Valor2);
                    nodoNuevo.NodoCen = Aux;
                    nodoNuevo.NodoIzq.NodoIzq = padre.NodoIzq;
                    nodoNuevo.NodoIzq.NodoCen = padre.NodoCen;
                }
            }
            return nodoNuevo;
        }

        private void Inserción(T Valor, ref Nodo23<T> padre, Delegate delegado1)
        {
            //Primer caso, la raiz del árbol es nula, y el valor se ingresa ahí
            if (padre == null)
            {
                Nodo23<T> NuevoNodo = new Nodo23<T>();
                NuevoNodo.Valor2 = Valor;
                padre = NuevoNodo;
            }
            else
            {
                int i = Convert.ToInt32(delegado1.DynamicInvoke(Valor, padre));
                if (i == 1)
                {
                    if (padre.NodoIzq != null)
                    {
                        Inserción(Valor, ref padre.NodoIzq, delegado1);
                    }
                    else
                    {
                        if (!padre.ocupado)
                        {
                            padre.Valor1 = padre.Valor2;
                            padre.Valor2 = Valor;
                            //la raíz se llena, por lo que no pueden haber más valores en ella, y se debe hacer balanceo de insertar un nuevo valor
                            padre.ocupado = true;
                        }
                        else
                        {
                            Aux = balanceo(Valor, padre, i);
                        }
                    }
                }
                else if (i == 2)
                {
                    if (padre.NodoCen != null)
                    {
                        Inserción(Valor, ref padre.NodoCen, delegado1);
                    }
                    else
                    {
                        if (!padre.ocupado)
                        {
                            padre.Valor1 = Valor;
                            padre.ocupado = true;
                        }
                        else
                        {
                            Aux = balanceo(Valor, padre, i);
                        }
                    }
                }
                else if (i == 3)
                {
                    if (padre.NodoDer != null)
                    {
                        Inserción(Valor, ref padre, delegado1);
                    }
                    else
                    {
                        Aux = balanceo(Valor, padre, i);
                    }
                }
                if (Aux != null)
                {
                    i = Convert.ToInt32(delegado1.DynamicInvoke(Valor, padre));
                    if (padre == raiz)
                    {
                        if (!padre.ocupado)
                        {
                            if (i == 1)
                            {
                                padre.Valor1 = padre.Valor2;
                                padre.Valor2 = Aux.Valor2;
                                padre.NodoDer = padre.NodoCen;
                                padre.NodoIzq = Aux.NodoIzq;
                                padre.NodoCen = Aux.NodoCen;
                            }
                            else
                            {
                                padre.Valor1 = Aux.Valor2;
                                padre.NodoCen = Aux.NodoIzq;
                                padre.NodoDer = Aux.NodoCen;
                            }
                            Aux = null;
                            padre.ocupado = true;
                        }
                        else
                        {
                            Aux = balanceo(Valor, padre, i);
                            raiz = Aux;
                            Aux = null;
                        }
                    }
                    else
                    {
                        if (!padre.ocupado)
                        {
                            if (i == 1)
                            {
                                padre.Valor1 = padre.Valor2;
                                padre.Valor2 = Aux.Valor2;
                                padre.NodoDer = padre.NodoCen;
                                padre.NodoIzq = Aux.NodoIzq;
                                padre.NodoCen = Aux.NodoCen;
                            }
                            else
                            {
                                padre.Valor1 = Aux.Valor2;
                                padre.NodoCen = Aux.NodoIzq;
                                padre.NodoDer = Aux.NodoCen;
                            }
                            Aux = null;
                            padre.ocupado = true;
                        }
                        else
                        {
                            if (padre.NodoIzq != null && padre.NodoCen != null)
                            {
                                Aux = balanceo(Valor, padre, i);
                            }
                        }
                    }
                }
            }
        }

        public void Insertar(T Data, Delegate delegado1)
        {
            Inserción(Data, ref raiz, delegado1);
            Inorden(raiz);
        }

        public void busqueda(Nodo23<T> buscado, Delegate delegado1)
        {

            Busquedas.Clear();
            busqueda2(raiz, delegado1, buscado);

        }

        public void busqueda2(Nodo23<T> aux, Delegate delegado1, Nodo23<T> buscado)
        {
            if (aux != null)
            {
                busqueda2(aux.NodoIzq, delegado1, buscado);
                if (Convert.ToInt32(delegado1.DynamicInvoke(buscado.Valor1, aux.Valor2)) == 0)
                {
                    Busquedas.Add(aux.Valor2);
                }
                if (aux.ocupado)
                {
                    if (Convert.ToInt32(delegado1.DynamicInvoke(buscado.Valor1, aux.Valor1)) == 0)
                    {
                        Busquedas.Add(aux.Valor1);
                    }
                }
                busqueda2(aux.NodoCen, delegado1, buscado);
                busqueda2(aux.NodoDer, delegado1, buscado);
            }
        }

        public void Edicion(T Valor, Nodo23<T> buscado, Delegate delegado1, Delegate delegado2)
        {
            editarNodo(Valor, raiz, delegado1, delegado2, buscado);
            Inorden(raiz);
        }

        public void editarNodo(T Valor, Nodo23<T> aux, Delegate delegado1, Delegate delegado2, Nodo23<T> buscado)
        {
            if (aux != null)
            {
                editarNodo(Valor, aux.NodoIzq, delegado1, delegado2, buscado);
                if (Convert.ToInt32(delegado1.DynamicInvoke(buscado.Valor1, aux.Valor2)) == 0)
                {
                    aux.Valor2 = (T)delegado2.DynamicInvoke(Valor, aux.Valor2);
                }
                if (aux.ocupado)
                {
                    if (Convert.ToInt32(delegado1.DynamicInvoke(buscado.Valor1, aux.Valor1)) == 0)
                    {
                        aux.Valor1 = (T)delegado2.DynamicInvoke(Valor, aux.Valor1);
                    }
                }
                editarNodo(Valor, aux.NodoCen, delegado1, delegado2, buscado);
                editarNodo(Valor, aux.NodoDer, delegado1, delegado2, buscado);
            }
        }

        public void Inorden(Nodo23<T> padre)
        {
            ListaDatos.Clear();
            RecorridoInOrden(padre);
        }
        public void RecorridoInOrden(Nodo23<T> padre)
        {
            if (padre.NodoIzq == null)
            {
                ListaDatos.Add(padre.Valor2);
                if (padre.ocupado)
                {
                    ListaDatos.Add(padre.Valor1);
                }
            }
            else
            {
                RecorridoInOrden(padre.NodoIzq);
                ListaDatos.Add(padre.Valor2);
                RecorridoInOrden(padre.NodoCen);
                if (padre.ocupado)
                {
                    ListaDatos.Add(padre.Valor1);
                    RecorridoInOrden(padre.NodoDer);
                }
            }
        }
        public List<T> ObtenerValoresEnLista()
        {
            ListaDatos.Clear(); // Limpiamos la lista antes de obtener los valores
            ObtenerValoresEnLista(raiz); // Llamamos al método recursivo para obtener los valores
            return ListaDatos;
        }

        private void ObtenerValoresEnLista(Nodo23<T> nodo)
        {
            if (nodo != null)
            {
                // Agregamos los valores del nodo a la lista
                ListaDatos.Add(nodo.Valor1);
                if (nodo.Valor2 != null)
                {
                    ListaDatos.Add(nodo.Valor2);
                }

                // Recorremos los nodos hijos en orden
                ObtenerValoresEnLista(nodo.NodoIzq);
                ObtenerValoresEnLista(nodo.NodoCen);
                ObtenerValoresEnLista(nodo.NodoDer);
            }
        }
    }



}
