#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FPTBook.Areas.Identity.Data;

namespace FPTBook.Controllers
{
    public class OrdersController : Controller
    {
        private readonly FPTBookContext _context;
        private readonly UserManager<FPTBookUser> _userManager;
        public OrdersController(FPTBookContext context, UserManager<FPTBookUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            string thisUserId = _userManager.GetUserId(HttpContext.User);
            var userContext = _context.Order.Where(o => o.UId == thisUserId).Include(o => o.User);
            return View(await userContext.ToListAsync());
        }
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> sellHistoryOrder()
        {
            FPTBookUser thisUser = await _userManager.GetUserAsync(HttpContext.User);
            Store thisStore = await _context.Store.FirstOrDefaultAsync(s => s.UId == thisUser.Id);
            OrderDetail thisOrderDetail = _context.OrderDetail.FirstOrDefault(od => od.Book.StoreId == thisStore.Id);
            var userContext = _context.Order.Where(o=>o.Id == thisOrderDetail.OrderId).Include(o => o.User);
            return View(await userContext.ToListAsync());
        }

    }
}
