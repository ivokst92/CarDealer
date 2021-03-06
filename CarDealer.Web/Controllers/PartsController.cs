﻿namespace CarDealer.Web.Controllers
{
    using CarDealer.Services.Interfaces;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private const int PageSize = 25;
        private readonly IPartService partsService;
        private readonly ISupplierService supplierService;

        public PartsController(IPartService partsService, ISupplierService supplierService)
        {
            this.partsService = partsService;
            this.supplierService = supplierService;
        }

        public IActionResult Create()
        => View(new PartFormModel()
        {
            Suppliers = this.GetAllSuppliers()
        });

        [HttpPost]
        public IActionResult Create(PartFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Suppliers = this.GetAllSuppliers();
                return View(model);
            }

            this.partsService.Create(model.Name,
                                    model.Price,
                                    model.Quantity,
                                    model.SupplierId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int Id)
        {
            var part = this.partsService.ById(Id);
            if (part == null)
            {
                return NotFound();
            }

            return View(new PartFormModel()
            {
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity,
                SupplierId = part.SupplierId,
                IsEdit = true,
            });
        }

        [HttpPost]
        public IActionResult Edit(int Id, PartFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.IsEdit = true;
                return View(model);
            }

            this.partsService.Edit(Id,
                                model.Price,
                                model.Quantity);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int Id)
        => View(Id);

        public IActionResult Destroy(int Id)
        {
            this.partsService.Delete(Id);
            return RedirectToAction(nameof(All));
        }
        public IActionResult All(int page = 1)
        => View(new PartPageListingModel
        {
            Parts = this.partsService.All(page, PageSize),
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(this.partsService.Total() / (double)PageSize)
        });

        private IEnumerable<SelectListItem> GetAllSuppliers()
        => this.supplierService.All()
            .Select(s =>
            new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
    }
}