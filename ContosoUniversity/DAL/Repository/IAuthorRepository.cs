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
    public partial interface IAuthorRepository : IRepository<ContosoUniversity.DAL.SalesModel.Author>
    {
        ICollection<ContosoUniversity.DAL.SalesModel.Author> GetAll();
        ContosoUniversity.DAL.SalesModel.Author GetByKey(int _Id);
    }
}
