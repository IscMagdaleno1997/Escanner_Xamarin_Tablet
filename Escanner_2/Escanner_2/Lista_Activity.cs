using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Escanner_2
{
    [Activity(Label = "Lista_Activity")]
    public class Lista_Activity : Activity
    {
        TextView titulo;
        ListView ListaProductos;
        EditText edtBuscar;

        string id;
        string tabla;

        Database Db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListaProductos);

            tabla = Intent.GetStringExtra("Tabla");

            titulo = FindViewById<TextView>(Resource.Id.txtTitulo);
            edtBuscar = FindViewById<EditText>(Resource.Id.edeBuscador);

            ListaProductos = FindViewById<ListView>(Resource.Id.ListaProductos);
            ListaProductos.ItemClick += (object sender, AdapterView.ItemClickEventArgs args)
                => ListItemClick(sender, args);

            Db = new Database(this);

            if (tabla == "Todos")
            {
                MostrarTodo();
            }
            else
            {
                MostrarTabla(tabla);

            }

            edtBuscar.TextChanged += delegate
            {
                MostrarBuscador(edtBuscar.Text);
            };
           

        }

        private void MostrarBuscador(string id)
        {
            ArrayList lst = new ArrayList();
            ArrayAdapter<string> adapter = null;
            string nombre = "";
            ICursor c = Db.Buscar("Nombre",id);
            if (c.MoveToFirst() == false)
            {
                ListaProductos.Adapter = adapter;
            }
            else
            {
                do
                {
                    id = c.GetString(0);
                    nombre = c.GetString(1);
                    lst.Add(nombre);
                } while (c.MoveToNext());
                string[] arr = (String[])lst.ToArray(typeof(string));
                adapter =
                    new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                ListaProductos.Adapter = adapter;

            }
        }


        private void MostrarTodo()
        {
            ArrayList lst = new ArrayList();
            ArrayAdapter<string> adapter = null;
            string nombre = "";
            ICursor c = Db.getDatos();
            if (c.MoveToFirst() == false)
            {
                ListaProductos.Adapter = adapter;
            }
            else
            {
                do
                {
                    id = c.GetString(0);
                    nombre = c.GetString(1);
                    lst.Add(nombre);
                } while (c.MoveToNext());
                string[] arr = (String[])lst.ToArray(typeof(string));
                adapter =
                    new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                ListaProductos.Adapter = adapter;

            }
        }

        private void MostrarTabla(string Tabla)
        {
            ArrayList lst = new ArrayList();
            ArrayAdapter<string> adapter = null;
            string nombre = "";

            ICursor c = Db.GetData("Categoria", tabla);
            if (c.MoveToFirst() == false)
            {
                // listData.Adapter = adapter;
                Toast.MakeText(this, "no resultados", ToastLength.Short).Show();
 

            }
            else
            {
                do
                {
                    id = c.GetString(0);
                    nombre = c.GetString(1);
                    lst.Add(nombre);
                } while (c.MoveToNext());
                string[] arr = (String[])lst.ToArray(typeof(string));
                adapter =
                    new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                ListaProductos.Adapter = adapter;

            }
        }


        public void ListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = this.ListaProductos.GetItemAtPosition(e.Position);

            string dato = Convert.ToString(item);

            Intent intent = new Intent(this, typeof(Formulario_Activity));
            intent.PutExtra("Accion", "Modificar");
            intent.PutExtra("Nombre", dato);
            this.StartActivity(intent);
        }
    }
}