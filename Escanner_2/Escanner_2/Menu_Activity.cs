using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using ZXing.Mobile;

namespace Escanner_2
{
    [Activity(Label = "Menu_Activity")]
    public class Menu_Activity : Activity
    {

        Button btnEscanner;
        Button btnAgregar;
        Button btnBuscar;
        Button btnModifocar;
        Button btnVerProductos;

        Database Db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Menu);

            btnAgregar = FindViewById<Button>(Resource.Id.btnAgregarMenu);
            btnVerProductos = FindViewById<Button>(Resource.Id.btnVerProductosMenu);
            btnEscanner = FindViewById<Button>(Resource.Id.btnEscanerMenu);
            btnModifocar = FindViewById<Button>(Resource.Id.btnModificarMenu);
            btnBuscar = FindViewById<Button>(Resource.Id.btnBuscarMenu);

            Db = new Database(this);

            btnAgregar.Click += delegate 
            {

                Intent intento2 = new Intent(this, typeof(Formulario_Activity));
                intento2.PutExtra("Accion", "Agregar");
                this.StartActivity(intento2);

            };


            btnBuscar.Click += delegate
            {

                Intent intento2 = new Intent(this, typeof(Detalles_Activity));
                this.StartActivity(intento2);

            };


            btnVerProductos.Click += delegate
            {

                Intent intento = new Intent(this, typeof(Tablas_Activity));
                this.StartActivity(intento);

            };
            
            btnEscanner.Click += async delegate 
            {

                MobileBarcodeScanner.Initialize(Application);


                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    Intent intento = new Intent(this, typeof(Formulario_Activity));
                    intento.PutExtra("Accion", "Escanner");
                    intento.PutExtra("Id", result.Text);
                    this.StartActivity(intento);
                }

                
            };

            btnModifocar.Click += async delegate
            {

                MobileBarcodeScanner.Initialize(Application);


                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    Intent intento = new Intent(this, typeof(Formulario_Activity));
                    intento.PutExtra("Accion", "Escanner");
                    intento.PutExtra("Id", result.Text);
                    this.StartActivity(intento);
                }


            };



        }
     



    }
}