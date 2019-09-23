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
using System.Data.Entity.ModelConfiguration;

namespace ContosoUniversity.DAL.SalesModel.Mapping
{

    public partial class ItemConfiguration : EntityTypeConfiguration<Item>
    {

        public ItemConfiguration()
        {
            this
                .HasKey(p => p.Id)
                // Table Per Type (TPT) inheritance root class
                .ToTable("Item", "dbo");
            // Properties:
            this
                .Property(p => p.Id)
                    .IsRequired()
                    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                    .HasColumnType("numeric");
            this
                .Property(p => p.ArDescription)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar");
            this
                .Property(p => p.EnDescription)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("varchar");
            this
                .Property(p => p.Category)
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
            this
                .Property(p => p.RowVersion)
                    .HasColumnType("datetime");
            this
                .Property(p => p.Price)
                    .HasColumnType("decimal");
            // Association:
            this
                .HasMany(p => p.ItemStores)
                    .WithRequired(c => c.Item)
                .HasForeignKey(p => p.ItemId)
                    .WillCascadeOnDelete(false);
            OnCreated();
        }

        partial void OnCreated();

    }
}
