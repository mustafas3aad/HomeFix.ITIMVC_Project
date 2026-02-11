using BulkyWebRazor_temp.Data;
using BulkyWebRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext db;
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                 Category = db.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid==true)
            {
                db.Categories.Update(Category);
                db.SaveChanges();
                TempData["Success"] = "Category Updated Sucessfully";
                return RedirectToPage(nameof(Index));
            }
            return Page();   
        }
    }
}
