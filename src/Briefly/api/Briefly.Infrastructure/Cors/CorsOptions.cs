using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefly.Infrastructure.Cors
{
   public class CorsOptions
   {
      public CorsOptions()
      {
         AllowedOrigins = [];
      }

      public Collection<string> AllowedOrigins { get; }
   }
}
