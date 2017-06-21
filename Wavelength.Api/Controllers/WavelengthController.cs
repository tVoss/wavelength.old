using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wavelength.Api.Controllers
{
    public class WavelengthController : ControllerBase
    {
        protected readonly FacebookApi FacebookApi;
        protected readonly WavelengthDbContext DbContext;   

        public WavelengthController(WavelengthDbContext context, FacebookApi api)
        {
            FacebookApi = api;
            DbContext = context;
        }
    }
}
