﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Devart Entity Developer tool using Entity Framework DbContext template.
// Code is generated on: 25/11/2018 12:35:48 م
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ContosoUniversity.DAL.SalesModel
{

    /// <summary>
    /// There are no comments for ContosoUniversity.DAL.SalesModel.Store in the schema.
    /// </summary>
    public partial class Store    {

        public Store()
        {
          this.Active = true;
            OnCreated();
        }


        #region Properties
    
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(200)]
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual string Name
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual int Id
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Active in the schema.
        /// </summary>
        public virtual bool? Active
        {
            get;
            set;
        }


        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// There are no comments for ItemStores in the schema.
        /// </summary>
        public virtual ICollection<ItemStore> ItemStores
        {
            get;
            set;
        }

        #endregion
    
        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion
    }

}