using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezGranits
{
    class DB
    {
        private static BezGranitsEntities _context;

        public static BezGranitsEntities GetContext()
        {
            if (_context == null)
                _context = new BezGranitsEntities();
            return _context;
        }
    }
}
