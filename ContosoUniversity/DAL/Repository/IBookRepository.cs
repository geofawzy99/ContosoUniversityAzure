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
using System.Collections.Generic;

namespace ContosoUniversity.DAL.SalesModel.Repository
{
    public partial interface IBookRepository : IRepository<ContosoUniversity.DAL.SalesModel.Book>
    {
        ICollection<ContosoUniversity.DAL.SalesModel.Book> GetAll();
        ContosoUniversity.DAL.SalesModel.Book GetByKey(long _Id);
    }
}
