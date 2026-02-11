using BulkyWebRazor_temp.Data;
using BulkyWebRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        
        private readonly ApplicationDbContext db;
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
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
            
                Category = db.Categories.Find(Category.Id);

                if (Category == null)
                    return NotFound();

                db.Categories.Remove(Category);
                db.SaveChanges();
            TempData["Success"] = "Category Deleted Sucessfully";
            return RedirectToPage(nameof(Index));
            
            
        }
    }
}
