using Degerlendirme.Model;

namespace Degerlendirme.Data
{
    public class Seed
    {
        public static void Test(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            if(context != null)
            {
               context.SaveChanges();
            }
        }
    }
}
