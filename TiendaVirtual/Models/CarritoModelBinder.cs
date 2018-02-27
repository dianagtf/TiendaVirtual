using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Models
{
    public class CarritoModelBinder : IModelBinder   
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Carrito carrito = (Carrito)controllerContext
                                 .HttpContext
                                 .Session["key_cc"];

            if (carrito == null)
            {
                carrito = new Carrito();
                controllerContext.HttpContext.Session["key_cc"] = carrito;
            }
            return carrito;
        }
    }
}