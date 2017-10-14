using DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace BusinessLogic.Common
{
    public sealed class DatabaseResourceSet : ResourceSet
    {
        private readonly CultureInfo _culture;
        //private IAuditLogDataAccess _auditLogDataAccess;
        private static readonly Dictionary<string, Hashtable> CachedResources = new Dictionary<string, Hashtable>();

        public DatabaseResourceSet(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException("culture");

            this._culture = culture;
            ReadResources();
        }

        protected override void ReadResources()
        {
            if (CachedResources.ContainsKey(_culture.Name))
            // retrieve cached resource set
            {
                Table = CachedResources[_culture.Name];
                return;
            }

            //using (ERPSettingDataAccess csmContext = new ERPSettingDataAccess())
            //{
            //    _auditLogDataAccess = new AuditLogDataAccess(csmContext);
            //    var query = _auditLogDataAccess.GetMessages(_culture.Name);

            //    foreach (var msg in query)
            //    {
            //        Table.Add(msg.MessageKey, msg.MessageValue);
            //    }
            //}

            CachedResources[_culture.Name] = Table;
        }
    }
}
