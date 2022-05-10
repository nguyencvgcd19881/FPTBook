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
    //[Authorize(Roles ="Seller")]
    public class OrderDetailsController : Controller
    {
        private readonly FPTBookContext _context;
        private readonly UserManager<FPTBookUser> _userManager;

        public OrderDetailsController(FPTBookContext context, UserManager<FPTBookUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: OrderDetails
        [Authorize(Roles = " Customer")]
        public async Task<IActionResult> Index()
        {
            string thisUsers = _userManager.GetUserId(HttpContext.User);
            var fPTBookContext = _context.OrderDetail.Where(o => o.Order.UId == thisUsers).Include(o => o.Book).Include(o => o.Order).Include(o => o.Order.User).Include(o => o.Book.Store);
            return View(await fPTBookContext.ToListAsync());
        }
        [Authorize(Roles = " Seller")]
        public async Task<IActionResult> managerOrder()
        {
            var managerOrder = _context.OrderDetail.Include(o => o.Book).Include(o => o.Order).Include(o => o.Order.User).Include(o => o.Book.Store);
            return View(await managerOrder.ToListAsync());
        }
    }
}
        // GET: OrderDetails/Details/5
        