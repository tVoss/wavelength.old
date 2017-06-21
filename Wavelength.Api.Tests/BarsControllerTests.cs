using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Wavelength.Api.Controllers;

namespace Wavelength.Api.Tests
{
    [TestClass]
    public class BarsControllerTests
    {

        public void Idk()
        {
            var ops = new DbContextOptionsBuilder().UseMemoryCache(new MemoryCache(null)).Options;
            WavelengthDbContext ctx = new WavelengthDbContext(ops);
            var ctrl = new BarsController(ctx, null);
        }
    }
}
