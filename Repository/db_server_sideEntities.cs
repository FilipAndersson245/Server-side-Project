using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository
{
    public partial class db_server_sideEntities
    {
        static db_server_sideEntities()
        {
            var foo = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        }
    }
}