using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace Escanner_2
{
    [Activity(Label = "Tablas_Activity")]
    public class Tablas_Activity : Activity
    {

        Button btnTodo;
        Button btnAbarrotes;
        Button btnDulceria;
        Button btnSurtidores;
        Button btnPapeleria;
        Button btnFrutas;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tablas);

            btnTodo = FindViewById<Button>(Resource.Id.btnTodo);
            btnAbarrotes = FindViewById<Button>(Resource.Id.btnAbarrotes);
            btnDulceria = FindViewById<Button>(Resource.Id.btnDulceria);
            btnSurtidores = FindViewById<Button>(Resource.Id.btnSurtidores);
            btnPapeleria = FindViewById<Button>(Resource.Id.btnPapeleria);
            btnFrutas = FindViewById<Button>(Resource.Id.btnFrutVerd);

            btnTodo.Click += delegate
            {
                Intent intento2 = new Intent(this, typeof(Lista_Activity));
                intento2.PutExtra("Tabla", "Todos");
                this.StartActivity(intento2);

            };

            btnAbarrotes.Click += delegate
            {
                Intent intento2 = new Intent(this, typeof(Lista_Activity));
                intento2.PutExtra("Tabla", "Abarrotes");
                this.StartActivity(intento2);

            };

            btnDulceria.Click += delegate
            {
                Intent intento2 = new Intent(this, typeof(Lista_Activity));
                intento2.PutExtra("Tabla", "Dulceria");
                this.StartActivity(intento2);

            };

            btnSurtidores.Click += delegate
            {
                Intent intento2 = new Intent(this, typeof(Lista_Activity));
                intento2.PutExtra("Tabla", "Surtidores");
                this.StartActivity(intento2);

            };

            btnFrutas.Click += delegate
            {
                Intent intento2 = new Intent(this, typeof(Lista_Activity));
                intento2.PutExtra("Tabla", "Frutas y Verduras");
                this.StartActivity(intento2);

            };

            btnPapeleria.Click += delegate
            {
                Intent intento2 = new Intent(this, typeof(Lista_Activity));
                intento2.PutExtra("Tabla", "Papeleria");
                this.StartActivity(intento2);

            };


        }
     

    }
}