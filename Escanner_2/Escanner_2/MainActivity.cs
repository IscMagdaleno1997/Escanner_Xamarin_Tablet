using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections;
using ZXing.Mobile;

namespace Escanner_2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
      
        Database db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
        
            db = new Database(this);


            Button btnMenu = FindViewById<Button>(Resource.Id.btnMenu);
            Button btnTablas = FindViewById<Button>(Resource.Id.btnTablas);
            ImageButton imgButton = FindViewById<ImageButton>(Resource.Id.imgBtnPrincipal);
            imgButton.SetImageResource(Resource.Drawable.scannerLogo);

            btnMenu.Click += delegate
            {
                Intent intento = new Intent(this, typeof(Menu_Activity));
                this.StartActivity(intento);
            };

            btnTablas.Click += delegate
            {
                Intent intento = new Intent(this, typeof(Tablas_Activity));
                this.StartActivity(intento);
            };

            imgButton.Click += async delegate
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

