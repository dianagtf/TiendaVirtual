using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class CarritosController : Controller
    {
        Pedido pedido = new Pedido();
        // GET: Carrito
        public ActionResult Add(int id, Carrito cc)
        {
            TablaProductoEntities conn = new TablaProductoEntities();
   
            Producto prod = conn.Producto.Find(id);
            pedido.IdProducto = prod.Id;
            pedido.IdUsuario = 1;
            pedido.Precio = prod.Precio;
            pedido.Fecha = System.DateTime.Now;
            
            cc.Add(prod);
            prod.Cantidad = prod.Cantidad - 1;
            db.Pedido.Add(pedido);
          

            db.Entry(prod).State = EntityState.Modified;
    
            db.SaveChanges();

            //Cada vez que se pincha el carrito, nos mostrará lo que hay en el carrito de la compra.
            return View("Index", cc);
        }

        private TablaProductoEntities db = new TablaProductoEntities();

        // GET: Carritos
        public ActionResult Index()
        {
            return View(db.Producto.ToList());
        }

        // GET: Carritos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Carritos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Precio,Cantidad,Descripción,Imagen")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Carritos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Precio,Cantidad,Descripción,Imagen")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Carritos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
