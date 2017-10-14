﻿using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace BusinessLogic.Common
{
    public sealed class DatabaseResourceManager : ResourceManager
    {
        #region Singleton pattern http://msdn.microsoft.com/en-us/library/ff650316.aspx
        private static volatile DatabaseResourceManager instance;
        private static object syncRoot = new Object();

        private DatabaseResourceManager() : base() { }

        public static DatabaseResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DatabaseResourceManager();
                    }
                }

                return instance;
            }
        }
        #endregion

        protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            if (culture == null)
                culture = Thread.CurrentThread.CurrentCulture;

            return new DatabaseResourceSet(culture);
        }
    }
}
