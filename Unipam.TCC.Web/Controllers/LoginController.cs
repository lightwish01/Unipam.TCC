﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unipam.TCC.BLL.ImplementationBLL;
using Unipam.TCC.BLL.InterfacesBLL;
using Unipam.TCC.DAL.Entity;

namespace Unipam.TCC.Web.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioBLL usuarioBLL;

        public LoginController()
        {
            usuarioBLL = new UsuarioBLL();
        }

       [HttpGet]
        public ActionResult Index()
        {
            if (Session["NomeUsuario"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario usuario)
        {
            Usuario usuarioValido = usuarioBLL.BuscarUsuario(usuario);

            if (usuarioValido == null)
            {
                return View();
            }
            else
            {
                Session["NomeUsuario"] = usuario.NomeUsuario;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}