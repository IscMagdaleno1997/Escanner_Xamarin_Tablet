using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Escanner_2
{
    class Database : SQLiteOpenHelper
    {

        public static string Id = "Id";
        public static string Nombre = "Nombre";
        public static string PrecioC = "PrecioC";
        public static string PrecioV = "PrecioV";
        public static string Fecha = "Fecha";
        public static string Categoria = "Categoria";


        private static string DB = "Tienda";
        private static string Table = "Productos";


        public Database(Context context) : base(context, DB, null, 1)
        {


        }



        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("CREATE TABLE " + Table + " (Id TEXT PRIMARY KEY, Nombre TEXT, PrecioC DECIMAL(5,2), " +
                " PrecioV DECIMAL(5,2), Fecha TEXT, Categoria TEXT )");
        }

        public ICursor Buscar(string column, string condicion)
        {
            string[] columnas = { Id, Nombre, PrecioC, PrecioV, Fecha, Categoria };
            string[] args = new string[] { condicion + "%" };
            ICursor c = this.ReadableDatabase.Query(Table, columnas, column + " like ?", args, null, null, null);
            return c;

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS " + Table);
            OnCreate(db);
        }

        public void AddDatos(Obj_Producto producto)
        {

            ContentValues valores = new ContentValues();
            valores.Put(Id, producto.Id);
            valores.Put(Nombre, producto.Nombre);
            valores.Put(PrecioC, producto.PrecioC);
            valores.Put(PrecioV, producto.PrecioV);
            valores.Put(Fecha, producto.Fecha);
            valores.Put(Categoria, producto.Categoria);
            this.WritableDatabase.Insert(Table, null, valores);


        }



        public ICursor getDatos()
        {
            string[] columnas = { Id, Nombre, PrecioC, PrecioV, Fecha, Categoria };
            ICursor c = this.ReadableDatabase.Query(Table, columnas, null, null, null, null, null);
            
            return c;


        }

        public ICursor GetData(string column, string condicion)
        {
            string[] columnas = { Id, Nombre, PrecioC, PrecioV, Fecha, Categoria };
            string[] args = new string[] { condicion };
            ICursor c = this.ReadableDatabase.Query(Table, columnas, column + "=?", args, null, null, null);
            return c;
        }


        public void Update(Obj_Producto producto, string condicion )
        {
            string[] args = { condicion };
            ContentValues valores = new ContentValues();
            valores.Put(Id, producto.Id);
            valores.Put(Nombre, producto.Nombre);
            valores.Put(PrecioC, producto.PrecioC);
            valores.Put(PrecioV, producto.PrecioV);
            valores.Put(Fecha, producto.Fecha);
            valores.Put(Categoria, producto.Categoria);
            this.WritableDatabase.Update(Table, valores, Id + "=?", args);

        }


        public void Delete(string condicion)
        {
            string[] args = { condicion };
            this.WritableDatabase.Delete(Table, Id + "=?", args);
        }


    }
}