﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.WebApi
{
    [Route("api/ProductService")]
    [ApiController]
    public class ProductServiceController : Controller
    {
        static List<Product> products = new List<Product>();
        static ProductServiceController()
        {
            products.Add(new Product { Id = 1, Name = "T430笔记本", Price = 8888, Description = "CPU i7标压版，1T硬盘" });
            products.Add(new Product { Id = 2, Name = "华为Mate10", Price = 3888, Description = "大猩猩屏幕，多点触摸" });
            products.Add(new Product { Id = 3, Name = "天梭手表", Price = 9888, Description = "瑞士经典款，可好了" });
        }

        [HttpGet]
        public IEnumerable<Product> Get() => products;

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var product = products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
            }
            return product;
        }

        [HttpPost]
        public void Add(Product model)
        {
            if (products.Any(p => p.Id == model.Id))
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;//通过状态码而非响应体报错，是restful风格
                return;
            }
            products.Add(model);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
        }


    }

    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
