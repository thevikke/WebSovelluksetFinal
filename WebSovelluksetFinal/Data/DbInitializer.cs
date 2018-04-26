using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSovelluksetFinal.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
