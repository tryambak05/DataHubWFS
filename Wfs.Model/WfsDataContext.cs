using System;
using Microsoft.EntityFrameworkCore;
using Wfs.Model.Helper;
using Wfs.Model.Models;

namespace Wfs.Model
{
    public interface IWfsDataContext
    {
        DbSet<WfsMessageTransmit> WfsMessageTransmit { get; set; }
        int SaveChanges();

    }

    public class WfsDataContext : IWfsDataContext
    {
        internal const string AppSchemaName = "WfsDataHub";

        public WfsDataContext(IApplicationConfiguration applicationConfiguration)
        {
            this._ApplicationConfiguration = applicationConfiguration;
        }

        public DbSet<WfsMessageTransmit> WfsMessageTransmit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private IApplicationConfiguration _ApplicationConfiguration { get; set; }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
