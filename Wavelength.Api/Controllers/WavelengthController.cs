using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wavelength.Api.Controllers
{
    public class WavelengthController : ControllerBase, IDisposable
    {

        protected readonly WavelengthDbContext DbContext;

        public WavelengthController()
        {
            DbContext = new WavelengthDbContext();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
