using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{

    public class Role
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public IList<User> Users { get; set; }
    }
}