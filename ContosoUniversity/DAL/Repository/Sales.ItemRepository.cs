﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using the template for generating Repositories and a Unit of Work for Entity Framework model.
// Code is generated on: 25/11/2018 12:35:49 م
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace ContosoUniversity.DAL.SalesModel.Repository
{
    public partial class ItemRepository : EntityFrameworkRepository<ContosoUniversity.DAL.SalesModel.Item>, IItemRepository
    {
        public ItemRepository(ContosoUniversity.DAL.SalesModel.SalesContext context) : base(context)
        {
        }

        public virtual ICollection<ContosoUniversity.DAL.SalesModel.Item> GetAll()
        {
            return objectSet.ToList();
        }

        public virtual ContosoUniversity.DAL.SalesModel.Item GetByKey(long _Id)
        {
            return objectSet.SingleOrDefault(e => e.Id == _Id);
        }

        public new ContosoUniversity.DAL.SalesModel.SalesContext Context 
        {
            get 
            {
                return (ContosoUniversity.DAL.SalesModel.SalesContext)base.Context;
            }
        }
    }
}
