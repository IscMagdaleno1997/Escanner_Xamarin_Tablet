using System;
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
    [Activity(Label = "Formulario_Activity")]
    public class Formulario_Activity : Activity
    {

        Button btnEscanear;
        Button btnAgregar;
        Button btnModificar;
        Button btnBorrar;
        Button btnBuscar;


        EditText edtId;
        EditText edtNom;
        EditText edtPrecioV;
        EditText edtPrecioC;
        string   categoria_Anterior;
       
        EditText edtFecha;

        private string Id;
        private string Nombre = "";
        private string Accion = "";


        Obj_Producto producto = new Obj_Producto();
        Database Db;


        Spinner spinner;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            Db = new Database(this);

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FormularioProductos);

             btnEscanear = FindViewById<Button>(Resource.Id.btnEscanearForm);
             btnAgregar = FindViewById<Button>(Resource.Id.btnAgregarForm);
             btnModificar = FindViewById<Button>(Resource.Id.btnModificarForm);
             btnBorrar = FindViewById<Button>(Resource.Id.btnBorrarForm);
             btnBuscar = FindViewById<Button>(Resource.Id.btnBuscarForm);


            edtId = FindViewById<EditText>(Resource.Id.edtId);
            edtNom = FindViewById<EditText>(Resource.Id.edtNom);
            edtPrecioV = FindViewById<EditText>(Resource.Id.edtPrecioV);
            edtPrecioC = FindViewById<EditText>(Resource.Id.edtPrecioC);
            edtFecha = FindViewById<EditText>(Resource.Id.edtFecha);
           
            Nombre = Intent.GetStringExtra("Nombre");
            Id = Intent.GetStringExtra("Id");
            Accion = Intent.GetStringExtra("Accion");

            spinner = FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Categoria, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;


      


            if (Accion == "Agregar" )
            {
                Toast.MakeText(this, "Agregar", ToastLength.Long).Show();
                btnModificar.Visibility = ViewStates.Invisible;
                btnBorrar.Visibility = ViewStates.Invisible;
                btnAgregar.Visibility = ViewStates.Visible;
                edtFecha.Visibility = ViewStates.Invisible;

            }
            else if (Accion == "Modificar")
            {
                Toast.MakeText(this, "Modificar "+ Nombre, ToastLength.Long).Show();
                MostrarDatos("Nombre", Nombre);

            }
            else if(Accion == "Escanner")
            {
                Toast.MakeText(this, "Escanner "+ Id, ToastLength.Long).Show();
                edtId.Text = Id;
                MostrarDatos("Id", Id);
                spinner.SetSelection(PosSpinner(categoria_Anterior));

            }

            btnBuscar.Click += delegate
            {
                edtId.Text = Id;
                MostrarDatos("Id", Id);
                spinner.SetSelection(PosSpinner(categoria_Anterior));
            };

            

            btnEscanear.Click += async delegate
            {

                MobileBarcodeScanner.Initialize(Application);


                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    edtId.Text = result.Text;
                    producto.Id = result.Text;

                    MostrarDatos("Id", result.Text);
                    spinner.SetSelection(PosSpinner(categoria_Anterior));
                }

            };


            btnAgregar.Click += delegate
            {
                DateTime Hoy = DateTime.Today;
                string fecha_actual = Hoy.ToString("dd-MM-yyyy");
                string IdAleatorio = Hoy.ToString("ddMMyyyyHHmmss");

                if (edtId.Text.Equals(""))
                {
                    edtId.Text = IdAleatorio;
                }

                producto.Id = edtId.Text;
                producto.Nombre = edtNom.Text;
                producto.PrecioC = "PC ->$"+edtPrecioC.Text;
                producto.PrecioV = "PV ->$"+edtPrecioV.Text;
                producto.Fecha = fecha_actual;
                producto.Categoria = categoria_Anterior;

                Db.AddDatos(producto);

                Toast.MakeText(this, "Agregado", ToastLength.Short).Show();


                edtId.Text = "";
                edtNom.Text = "";
                edtPrecioV.Text = "";
                edtPrecioC.Text = "";
                edtFecha.Text = "";



            };

            btnModificar.Click += delegate
            {
                producto.Id = edtId.Text;


                DateTime Hoy = DateTime.Today;
                string fecha_actual = Hoy.ToString("dd-MM-yyyy");

                Toast.MakeText(this, "Modificado" + categoria_Anterior, ToastLength.Short).Show();

                producto.Nombre = edtNom.Text;
                producto.PrecioC = edtPrecioC.Text;
                producto.PrecioV = edtPrecioV.Text;
                producto.Fecha = fecha_actual;
                producto.Categoria = categoria_Anterior;

                Db.Update(producto, producto.Id);

                Toast.MakeText(this, "Modificado" + categoria_Anterior, ToastLength.Short).Show();
            };


            btnBorrar.Click += delegate
            {
                Db.Delete(edtId.Text);

                Toast.MakeText(this, "Borrado", ToastLength.Long).Show();


                edtId.Text = "";
                edtNom.Text = "";
                edtPrecioV.Text = "";
                edtPrecioC.Text = "";
                edtFecha.Text = "";
            };
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            categoria_Anterior = string.Format(spinner.GetItemAtPosition(e.Position) +"");
        
        }



        private void MostrarDatos(string col,  string valorId)
        {
            ICursor c = Db.GetData(col, valorId);
            if (c.MoveToFirst() == false)
            {
                edtNom.Text = "";
                edtPrecioV.Text = "";
                edtPrecioC.Text = "";
                edtFecha.Text = "";

                btnModificar.Visibility = ViewStates.Invisible;
                btnBorrar.Visibility = ViewStates.Invisible;
                btnAgregar.Visibility = ViewStates.Visible;
                edtFecha.Visibility = ViewStates.Invisible;

                Toast.MakeText(this, "No encontrado", ToastLength.Short).Show();

            }
            else
            {
                do
                {
                    edtId.Text = c.GetString(0);
                    edtNom.Text= c.GetString(1);
                    edtPrecioC.Text = c.GetString(2);
                    edtPrecioV.Text = c.GetString(3);
                    edtFecha.Text = c.GetString(4);

                    categoria_Anterior = c.GetString(5);



                } while (c.MoveToNext());

                Toast.MakeText(this, PosSpinner(categoria_Anterior)+"", ToastLength.Short).Show();
                spinner.SetSelection(PosSpinner(categoria_Anterior)+1);

                btnAgregar.Visibility = ViewStates.Invisible;
                btnModificar.Visibility = ViewStates.Visible;
                btnBorrar.Visibility = ViewStates.Visible;
                edtFecha.Visibility = ViewStates.Visible;

            }
        }

        private int PosSpinner(string cat)
        {
            switch (cat)
            {
                case "Abarrotes":
                    return 0;
                case "Surtidores":
                    return 1;
                case "Dulceria":
                    return 2;
                case "Papeleria":
                    return 3;
                case "Frutas y Verduras":
                    return 4;
            }
            return 0;
        }
    }
}