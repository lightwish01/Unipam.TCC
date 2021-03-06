﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Unipam.TCC.DAL.Entity;
using Unipam.TCC.BLL.ImplementationBLL;
using Unipam.TCC.BLL.InterfacesBLL;
using Unipam.TCC.BLL.InterfaceBLL;

namespace Unipam.TCC.Web.Controllers
{
    public class EntregaController : Controller
    {
        private TCCModel db = new TCCModel();
        private IEntregaBLL entregaBLL = new EntregaBLL();
        private IProjetoBLL projetoBLL = new ProjetoBLL();
        private IEtapaBLL etapaBLL = new EtapaBLL();

        // GET: Entrega
        public ActionResult Index()
        {
            var entregas = entregaBLL.Todas();
            return View(entregas.ToList());
        }

        // GET: Entrega/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrega entrega = entregaBLL.ObterPorId(id);
            if (entrega == null)
            {
                return HttpNotFound();
            }
            return View(entrega);
        }

        // GET: Entrega/Create
        public ActionResult Create()
        {
            ViewBag.IdEtapa = new SelectList(etapaBLL.Todas(), "IdEtapa", "IdEtapa");
            ViewBag.IdProjeto = new SelectList(projetoBLL.Todos(), "IdProjeto", "NomeProjeto");
            return View();
        }

        // POST: Entrega/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEntrega,IdProjeto,IdEtapa,Data")] Entrega entrega)
        {
            if (ModelState.IsValid)
            {
                entregaBLL.Salvar(entrega);
                return RedirectToAction("Index");
            }

            ViewBag.IdEtapa = new SelectList(db.Etapas, "IdEtapa", "IdEtapa", entrega.IdEtapa);
            ViewBag.IdProjeto = new SelectList(db.Projetoes, "IdProjeto", "NomeProjeto", entrega.IdProjeto);
            return View(entrega);
        }

        // GET: Entrega/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrega entrega = entregaBLL.ObterPorId(id);
            if (entrega == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEtapa = new SelectList(etapaBLL.Todas(), "IdEtapa", "IdEtapa", entrega.IdEtapa);
            ViewBag.IdProjeto = new SelectList(projetoBLL.Todos(), "IdProjeto", "NomeProjeto", entrega.IdProjeto);
            return View(entrega);
        }

        // POST: Entrega/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEntrega,IdProjeto,IdEtapa,Data")] Entrega entrega)
        {
            if (ModelState.IsValid)
            {
                entregaBLL.Salvar(entrega);
                return RedirectToAction("Index");
            }
            ViewBag.IdEtapa = new SelectList(etapaBLL.Todas(), "IdEtapa", "IdEtapa", entrega.IdEtapa);
            ViewBag.IdProjeto = new SelectList(projetoBLL.Todos(), "IdProjeto", "NomeProjeto", entrega.IdProjeto);
            return View(entrega);
        }

        // GET: Entrega/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrega entrega = entregaBLL.ObterPorId(id);
            if (entrega == null)
            {
                return HttpNotFound();
            }
            return View(entrega);
        }

        // POST: Entrega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            entregaBLL.Excluir(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entregaBLL.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
