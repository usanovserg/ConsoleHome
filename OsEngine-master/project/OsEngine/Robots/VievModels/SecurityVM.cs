using OsEngine.Entity;
using System;

namespace OsEngine.ViewModels
{
    public class SecurityVM
    {
        public SecurityVM(Security security)
        {
            _security = security;
        }

        Security _security;

        public string Name
        {
            get => _security.Name;
        }

        public DateTime Expiration
        {
            get => _security.Expiration;
        }

        public decimal Lot
        {
            get => _security.Lot;
        }

        public decimal Go
        {
            get => _security.Go;
        }

        public Security GetSecurity()
        {
            return _security;
        }
    }
}