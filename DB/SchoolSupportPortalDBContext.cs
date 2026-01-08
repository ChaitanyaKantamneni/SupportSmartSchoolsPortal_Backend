using Microsoft.EntityFrameworkCore;
using SchoolSupportPortal.Models;

namespace SchoolSupportPortal.DB
{
    public class SchoolSupportPortalDBContext:DbContext
    {
        public SchoolSupportPortalDBContext(DbContextOptions<SchoolSupportPortalDBContext> options) : base(options) { }

        public DbSet<SchoolDetails> Tbl_SchoolDetails { get; set; }
    }
}
