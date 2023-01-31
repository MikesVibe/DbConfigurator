using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public class RecipientRepository : GenericRepository<Recipient, DbConfiguratorDbContext>, IRecipientRepository
    {
        public RecipientRepository(DbConfiguratorDbContext context) 
            : base(context)
        {
        }


        //public override async Task<Recipient> GetByIDAsync(int id)
        //{
        //    //return await Context.Recipients
        //}


    }
}
