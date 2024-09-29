/*
 use spr_211
go
INSERT INTO Products (Name, Price, Description) VALUES ('Apple iPhone 14', 799.99, 'The latest iPhone model with 5G capability, A15 Bionic chip, and improved camera system.');
INSERT INTO Products (Name, Price, Description) VALUES ('Samsung Galaxy S23', 999.99, 'Flagship smartphone from Samsung featuring a dynamic AMOLED display and top-notch performance.');
INSERT INTO Products (Name, Price, Description) VALUES ('Sony WH-1000XM5', 399.99, 'Industry-leading noise-canceling wireless headphones with premium sound quality.');
INSERT INTO Products (Name, Price, Description) VALUES ('Dell XPS 13 Laptop', 1199.99, '13-inch ultra-thin laptop with InfinityEdge display and powerful Intel i7 processor.');
INSERT INTO Products (Name, Price, Description) VALUES ('Apple MacBook Pro 16"', 2499.99, 'High-performance laptop with M1 Pro chip, Retina display, and long battery life.');
INSERT INTO Products (Name, Price, Description) VALUES ('Google Pixel 7', 599.99, 'The latest Google phone with an advanced AI-powered camera and seamless integration with Google services.');
INSERT INTO Products (Name, Price, Description) VALUES ('Sony PlayStation 5', 499.99, 'Next-generation gaming console with 4K gaming and ultra-fast SSD for load times.');
INSERT INTO Products (Name, Price, Description) VALUES ('Microsoft Xbox Series X', 499.99, 'Powerful gaming console with 12 teraflops of processing power and 4K gameplay.');
INSERT INTO Products (Name, Price, Description) VALUES ('Bose QuietComfort Earbuds II', 299.99, 'Premium true wireless earbuds with advanced noise-canceling technology and crisp sound.');
INSERT INTO Products (Name, Price, Description) VALUES ('Apple AirPods Pro 2', 249.99, 'Second-generation wireless earbuds with active noise cancellation and transparency mode.');
INSERT INTO Products (Name, Price, Description) VALUES ('Fitbit Charge 5', 179.99, 'Advanced health and fitness tracker with built-in GPS, heart rate monitor, and stress tracking.');
INSERT INTO Products (Name, Price, Description) VALUES ('Nikon Z7 II Camera', 2999.99, 'Full-frame mirrorless camera with 45.7 MP resolution, dual processors, and 4K video recording.');
INSERT INTO Products (Name, Price, Description) VALUES ('Canon EOS R6', 2499.99, 'Mirrorless camera with 20 MP resolution, advanced autofocus system, and 4K video support.');
INSERT INTO Products (Name, Price, Description) VALUES ('Dyson V15 Detect Vacuum', 749.99, 'Powerful cordless vacuum cleaner with laser dust detection and advanced filtration system.');
INSERT INTO Products (Name, Price, Description) VALUES ('Instant Pot Duo 7-in-1', 99.99, 'Multifunctional pressure cooker with seven cooking modes, including slow cooking and steaming.');
INSERT INTO Products (Name, Price, Description) VALUES ('KitchenAid Stand Mixer', 499.99, 'Classic stand mixer with 10 speeds and durable build, perfect for baking and cooking tasks.');
INSERT INTO Products (Name, Price, Description) VALUES ('GoPro HERO11 Black', 499.99, 'Waterproof action camera with 5.3K video recording, 27 MP photos, and HyperSmooth stabilization.');
INSERT INTO Products (Name, Price, Description) VALUES ('Nintendo Switch OLED', 349.99, 'Hybrid gaming console with a 7-inch OLED screen, detachable controllers, and extensive game library.');
INSERT INTO Products (Name, Price, Description) VALUES ('Logitech MX Master 3', 99.99, 'Wireless ergonomic mouse with precision scrolling, customizable buttons, and multi-device support.');
INSERT INTO Products (Name, Price, Description) VALUES ('Razer DeathAdder V2', 69.99, 'High-precision gaming mouse with 20K DPI sensor, optical switches, and ergonomic design.');
 */
using Microsoft.EntityFrameworkCore;
using mvc_app.Models;

namespace mvc_app.Services
{
    public interface IServiceProducts
    {
        public ProductContext? _productContext { get; set; }
        public Product? Create(Product? product);
        public IEnumerable<Product>? Read();
        public Product? GetById(int id);
        public Product? Update(int id, Product? product);
        public bool Delete(int id);
        
    }

    public class ServiceProducts : IServiceProducts
    {
        public ProductContext? _productContext { get; set; }

        public Product? Create(Product? product)
        {
            _productContext?.Products.Add(product);
            _productContext?.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            Product? product = _productContext?.Products
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return false;
            } 
            else
            {
                _productContext?.Products.Remove(product);
                _productContext?.SaveChanges();
                return true;
            }
        }

        public Product? GetById(int id)
        {
            return _productContext?.Products
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product>? Read()
        {
            return _productContext?.Products.ToList();
        }

        public Product? Update(int id, Product? product)
        {
            if(id != product?.Id)
            {
                return null;
            } 
            else
            {
                try
                {
                    _productContext?.Products.Update(product);
                    _productContext?.SaveChanges();
                    return product;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }
            }
        }
    }
}
