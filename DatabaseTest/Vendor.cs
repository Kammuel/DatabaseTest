using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest
{
    /// <summary>
    /// Represents a single vendor
    /// </summary>
    class Vendor
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string City { get; set; }

    }
}
