using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Escanner_2
{
    class Obj_Producto
    {
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string PrecioC { get; set; }

        public string PrecioV { get; set; }

        public string Fecha { get; set; }

        public string Categoria { get; set; }
    }
}