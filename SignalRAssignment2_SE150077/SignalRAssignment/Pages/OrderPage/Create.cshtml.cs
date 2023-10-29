using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.OrderPage
{
    public class CreateModel : PageModel
    {
        public List<Customer> listCus;
        [BindProperty]
        public List<ProductDTO> products { get; set; }
        private readonly SignalRAssignment.Data.PRN221_As02Context _context;

        public CreateModel(SignalRAssignment.Data.PRN221_As02Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            listCus = _context.Customers.ToList();
            products = _context.Products
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Select(x => new ProductDTO()
                {
                    Category = x.Category.CategoryName,
                    ProductId = x.ProductId,
                    ProductImage = x.ProductImage,
                    ProductName = x.ProductName,
                    QuantityPerUnit = x.QuantityPerUnit,
                    Supplier = x.Supplier.CompanyName,
                    UnitPrice = x.UnitPrice
                }).ToList();

            return Page();
        }

        [BindProperty]
        public Order Orders { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            listCus = _context.Customers.ToList();
            var cus = listCus.FirstOrDefault(x => x.CustomerId == Orders.CustomerId);
            Orders.Customer = cus;
            _context.Orders.Add(Orders);

            await _context.SaveChangesAsync();

        
            foreach(var item in products)
            {
                if (item.BuyQuantity > 0)
                {
                    var product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductId); 
                    OrderDetail detail = new OrderDetail()
                    {
                        Order = Orders,
                        ProductId = product.ProductId,
                        Quantity = item.BuyQuantity,
                        UnitPrice = product.UnitPrice
                    };
                    product.QuantityPerUnit -= item.BuyQuantity;
                    _context.OrderDetails.Add(detail);                   
                    _context.Products.Update(product);
                   

                }
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public String Supplier { get; set; }
        public String Category { get; set; }
        public int QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string? ProductImage { get; set; }
        public int BuyQuantity { get; set; }    

    }
}
