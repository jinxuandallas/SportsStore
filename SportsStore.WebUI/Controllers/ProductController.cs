﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        
        public ViewResult List(string category, int page=1)
        {
            //return View(repository.Products);
            ProductsListViewModel model = new Models.ProductsListViewModel
            {
                Products = repository.Products.Where(p=>category==null||p.Category==category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new Models.PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                },
                CurrentCategory=category
            };
            return View(model);
            //return View(repository.Products.OrderBy(p=>p.ProductID).Skip((page-1)*PageSize).Take(PageSize));
        }
    }
}