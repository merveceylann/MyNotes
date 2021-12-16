﻿using Notlarim102.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {

        //tek bir yerden tek bir nesne olustur ve tek bir yerden dagit.
        //singlten teknigi

        private static NotlarimContext _db;

        private static object _lockSync = new object();

        public RepositoryBase()
        {

        }

        public static NotlarimContext CreateContext()
        {
            if (_db==null)
            {
                lock (_lockSync)
                {
                    if (_db==null)
                    {
                        _db = new NotlarimContext();
                    }
                }
            }
            return _db;
        }
    }
}
