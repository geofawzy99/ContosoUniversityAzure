﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Devart Entity Developer tool using Data Transfer Object template.
// Code is generated on: 25/11/2018 12:35:51 م
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.DAL.SalesModel.Dto
{

    public static partial class MedicineConverter
    {

        public static MedicineDto ToDto(this ContosoUniversity.DAL.SalesModel.Medicine source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static MedicineDto ToDtoWithRelated(this ContosoUniversity.DAL.SalesModel.Medicine source, int level)
        {
            if (source == null)
              return null;

            var target = new MedicineDto();

            // Properties
            target.Price = source.Price;
            target.RowVersion = source.RowVersion;
            target.Category = source.Category;
            target.EnDescription = source.EnDescription;
            target.ArDescription = source.ArDescription;
            target.Id = source.Id;
            target.ScentificName = source.ScentificName;
            target.ExpireDate = source.ExpireDate;
            target.MedicalGroup = source.MedicalGroup;
            target.BatchNo = source.BatchNo;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static ContosoUniversity.DAL.SalesModel.Medicine ToEntity(this MedicineDto source)
        {
            if (source == null)
              return null;

            var target = new ContosoUniversity.DAL.SalesModel.Medicine();

            // Properties
            target.Price = source.Price;
            target.RowVersion = source.RowVersion;
            target.Category = source.Category;
            target.EnDescription = source.EnDescription;
            target.ArDescription = source.ArDescription;
            target.Id = source.Id;
            target.ScentificName = source.ScentificName;
            target.ExpireDate = source.ExpireDate;
            target.MedicalGroup = source.MedicalGroup;
            target.BatchNo = source.BatchNo;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<MedicineDto> ToDtos(this IEnumerable<ContosoUniversity.DAL.SalesModel.Medicine> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<MedicineDto> ToDtosWithRelated(this IEnumerable<ContosoUniversity.DAL.SalesModel.Medicine> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<ContosoUniversity.DAL.SalesModel.Medicine> ToEntities(this IEnumerable<MedicineDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(ContosoUniversity.DAL.SalesModel.Medicine source, MedicineDto target);

        static partial void OnEntityCreating(MedicineDto source, ContosoUniversity.DAL.SalesModel.Medicine target);

    }

}