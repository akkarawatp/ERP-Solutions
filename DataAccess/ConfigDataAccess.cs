using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utilities;
using Entity;
using log4net;

namespace DataAccess
{
    public class ConfigDataAccess
    {
        private readonly ERPSettingDataContext _context;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ConfigDataAccess));

        public ConfigDataAccess(ERPSettingDataContext context)
        {
            _context = context;
            _context.Configuration.ValidateOnSaveEnabled = false;
            _context.Database.CommandTimeout = Constants.CommandTimeout;
        }
    }
}
