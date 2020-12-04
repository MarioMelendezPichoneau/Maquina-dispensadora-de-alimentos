using System;
using System.Collections.Generic;

namespace examen_Final
{

    // crear una aplicacion que simule una maquina dispensadora de alimentos. debe tener un maximo de 10 productos 
    // y solo acepta moneda (5,10,25) y billetes de (50,100 y 200).
    // clse producto: nombre, presio, existencia 
    class Program
    {
        static void Main(string[] args)
        {
            Dispensadora dispensador = new Dispensadora();

            Console.WriteLine("Bienvenido a la dispensadora ITLA \n ");



            while (true)
            {


                Console.WriteLine(dispensador.listaProducto());

                Console.WriteLine(" 1. apregar producto");
                Console.WriteLine(" 2. Comprar producto  ");
                string opcion = Console.ReadLine();


                switch (opcion)
                {
                    case "1":
                        Producto producto = new Producto();
                        Console.WriteLine("codigo ");
                        producto.codigo = Console.ReadLine();

                        Console.WriteLine("nombre ");
                        producto.Nombre = Console.ReadLine();

                        Console.WriteLine("Categoria ");
                        producto.Categoria = Console.ReadLine();

                        Console.WriteLine("cantidad ");
                        producto.Cantidad = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Presio ");
                        producto.Valor = double.Parse(Console.ReadLine());

                        dispensador.agreProducto(producto);

                        break;

                    
                    case "2":
                        Console.WriteLine(" codigo");
                        string codigo_venta = Console.ReadLine();

                        Console.WriteLine(" Dinero solo de (200-100-50-25-10-5) ");
                        dispensador.pago = Console.ReadLine();

                        Producto preComprado = dispensador.Vender(codigo_venta);

                        if (preComprado == null)
                        {
                            Console.WriteLine(" No se pudo sacar el producto");
                        }
                        else
                        {
                            Console.WriteLine($"\n su producto es {preComprado.codigo} y la devuelta es {preComprado.Cambio} ");
                        }

                        break;
                }

                Console.WriteLine(" Desea continuar si/no");
                if (Console.ReadLine() == "no")
                {
                    
                    break;
                }
            }


            Console.ReadKey();


        }
    }






}

   public  class Producto
   {
        public string codigo { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public double Valor { get; set; }
        public double Cambio { get; set; }


        public void SumarCantidad(int cantidad)
        {
            this.Cantidad += cantidad;
        }

        public bool Validarcantodad()
        {
            if (this.Cantidad > 0)
            {
                return true;
            }
            return false;
        }

        public bool validarValor(double valor)
        {
            if (this.Valor <= valor)
            {
                this.Cambio = valor - this.Valor;
                return true;
            }
            return false;
        }

        public void restarProductos()
        {
            this.Cantidad--;
        }

    

    }

    public class Dispensadora
    {
        public List<Producto> Productos { get; set; }

        public string pago { get; set; }
        public double Cambio { get; set; }
        public int valor { get; set; }
        public int cantidad { get; set; }

        public Dispensadora()
        {
            this.Productos = new List<Producto>();
            Producto chocolate = new Producto();
            chocolate.codigo = "01";
            chocolate.Nombre = "chocolate sniker";
            chocolate.Categoria = "A";
            chocolate.Cantidad = 12;
            chocolate.Valor = 50;

            Producto galletas = new Producto();
            galletas.codigo = "02";
            galletas.Nombre = "Princesa";
            galletas.Categoria = "B";
            galletas.Cantidad = 12;
            galletas.Valor = 200;

            Producto refresco = new Producto();
            refresco.codigo = "03";
            refresco.Nombre = "coca-cola";
            refresco.Categoria = "C";
            refresco.Cantidad = 12;
            refresco.Valor = 15;

            Producto agua = new Producto();
            agua.codigo = "04";
            agua.Nombre = "Agua Dasani";
            agua.Categoria = "D";
            agua.Cantidad = 12;
            agua.Valor = 10;

            Producto jugo = new Producto();
            jugo.codigo = "05";
            jugo.Nombre = " Jugo del Valle";
            jugo.Categoria = "E";
            jugo.Cantidad = 12;
            jugo.Valor = 25;


            Producto papitas = new Producto();
            papitas.codigo = "06";
            papitas.Nombre = "Papita Frito Lays";
            papitas.Categoria = "p";
            papitas.Cantidad = 12;
            papitas.Valor = 25;







            this.Productos.Add(chocolate);
            this.Productos.Add(galletas);
            this.Productos.Add(refresco);
            this.Productos.Add(agua);
            this.Productos.Add(jugo);
            this.Productos.Add(papitas);
        }

        public int validaProducto(string codigo) // este metodo es para validar si encontro si existe el ptoducto
        {
            int enc = -1; // para encontar el producto 



            for (int i = 0; i < this.Productos.Count; i++)
            {
                if (this.Productos[i].codigo == codigo)  
                {
                    enc = i;                            // si el producto existe me va devolver un valor mayor a -1
                }
            }


            return enc;
        }

        public bool agreProducto(Producto producto)  // esto es por si quieren agregar un nuevo producto lod admin de la maquina
        {
            int encon = this.validaProducto(producto.codigo);
            if (encon >= 0)
            {
                this.Productos[encon].SumarCantidad(producto.Cantidad);
            }
            else
            {
                this.Productos.Add(producto);
            }
            return true;

        }




    public double validarDinero(string[] dinero)
        {
            double total = 0;
            foreach (string item in dinero)
            {
                try
                {
                    total += double.Parse(item);

                }
                catch (Exception e) { }

            }

            return total;
        }

        // 200-100-50-25-10-5 dinero


        public Producto Vender(string codigo)
        {
            int encon = this.validaProducto(codigo);
            if (encon >= 0)
            {
                if (this.Productos[encon].Validarcantodad())
                {

                    string[] dinero = this.pago.Split('-');

                    double total = this.validarDinero(dinero);

                    if (this.Productos[encon].validarValor(total))
                    {
                        this.Productos[encon].restarProductos();
                        return this.Productos[encon];

                    }

                }
            }

            return null; //si no hay un producto entonces es nulo
        }

        public string listaProducto()
        {
            string lista = "";
            foreach (Producto item in this.Productos)
            {
                lista += item.codigo + " " + item.Nombre + " " +item.Categoria + " " + item.Cantidad + " " + item.Valor + "\n";
            }
            return lista;
        }


    }



