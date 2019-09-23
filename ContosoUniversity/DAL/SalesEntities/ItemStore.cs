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
    /// There are no comments for ContosoUniversity.DAL.SalesModel.ItemStore in the schema.
    /// </summary>
    public partial class ItemStore    {

        public ItemStore()
        {
          this.MinLimit = 0;
          this.MaxLimit = 0;
          this.Quantity = 0;
            OnCreated();
        }


        #region Properties
    
        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual long Id
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for MinLimit in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual int MinLimit
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for MaxLimit in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual int MaxLimit
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for Quantity in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual int Quantity
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for ItemId in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual long ItemId
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for StoreId in the schema.
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required()]
        public virtual int StoreId
        {
            get;
            set;
        }


        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// There are no comments for Item in the schema.
        /// </summary>
        public virtual Item Item
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Store in the schema.
        /// </summary>
        public virtual Store Store
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
