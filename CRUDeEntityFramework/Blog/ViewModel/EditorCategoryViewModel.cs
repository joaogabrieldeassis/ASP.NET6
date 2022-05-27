using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel
{
    public class EditorCategoryViewModel
    {
        [Required]
        public string Name{ get; set; }

        [Required]
        public string Slug{ get; set; }
    }
}
