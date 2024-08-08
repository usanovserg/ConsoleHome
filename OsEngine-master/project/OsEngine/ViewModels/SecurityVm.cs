using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using OsEngine.Entity;

namespace OsEngine.ViewModels
{
    public class SecurityVm
    {      
        public SecurityVm(Security security)
        {
            _security = security;
        }

        Security _security;

        public string Name => _security.Name;
        public string NameClass => _security.NameClass;
        public DateTime Expiration => _security.Expiration;
        public decimal Lot => _security.Lot;
        public decimal Go => _security.Go;

        public Security GetSecurity()
        {
            return _security;
        }
    }
}
