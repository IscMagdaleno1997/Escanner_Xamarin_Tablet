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
using ZXing.Mobile;

namespace Escanner_2
{
    [Activity(Label = "Detalles_Activity")]
    public class Detalles_Activity : Activity
    {
        Button btnEscaner;
        EditText edtBuscar;
        EditText edtNombre;
        EditText edtPrecioC;
        EditText edtPrecioV;

        ListView listaProductos;

        Database Db;

        public object ListaProductos { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Db = new Database(this);

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Detalles);


            btnEscaner = FindViewById<Button>(Resource.Id.btnEscannearDetalels);
            edtBuscar = FindViewById<EditText>(Resource.Id.edtBuscarDetalles);
            listaProductos = FindViewById<ListView>(Resource.Id.lsvBusqueda);

            edtNombre = FindViewById<EditText>(Resource.Id.edtNombreDetalles);
            edtPrecioC = FindViewById<EditText>(Resource.Id.edtPrecioCDetalles);
            edtPrecioV = FindViewById<EditText>(Resource.Id.edtPrecioVDetalles);

            listaProductos.ItemClick += (object sender, AdapterView.ItemClickEventArgs args)
                => ListItemClick(sender, args);
            MostrarTodo();

            btnEscaner.Click += async delegate
            {

                MobileBarcodeScanner.Initialize(Application);


                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {

                    MostrarDatos("Id", result.Text);
                }

            };

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
            ICursor c = Db.Buscar("Nombre", id);
            if (c.MoveToFirst() == false)
            {
                listaProductos.Adapter = adapter;
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
                listaProductos.Adapter = adapter;

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
                listaProductos.Adapter = adapter;
            }
            else
            {
                do
                {
                    nombre = c.GetString(1);
                    lst.Add(nombre);
                } while (c.MoveToNext());
                string[] arr = (String[])lst.ToArray(typeof(string));
                adapter =
                    new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                listaProductos.Adapter = adapter;

            }
        }

        private void MostrarDatos(string col, string valorId)
        {
            ICursor c = Db.GetData(col, valorId);
            if (c.MoveToFirst() == false)
            {

                Toast.MakeText(this, "No encontrado", ToastLength.Short).Show();

            }
            else
            {
                do
                {
                    edtNombre.Text = c.GetString(1);
                    edtPrecioC.Text = c.GetString(2);
                    edtPrecioV.Text = c.GetString(3);

                } while (c.MoveToNext());

               
            }
        }

        public void ListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = this.listaProductos.GetItemAtPosition(e.Position);

            string dato = Convert.ToString(item);

            MostrarDatos("Nombre", dato);
        }

    }
}