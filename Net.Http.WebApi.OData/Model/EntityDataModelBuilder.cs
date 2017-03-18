﻿// -----------------------------------------------------------------------
// <copyright file="EntityDataModelBuilder.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Model
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class which builds the <see cref="EntityDataModel"/>.
    /// </summary>
    public sealed class EntityDataModelBuilder
    {
        private readonly Dictionary<string, EdmComplexType> collections = new Dictionary<string, EdmComplexType>();

        /// <summary>
        /// Builds the Entity Data Model containing the collections registered.
        /// </summary>
        /// <returns>The Entity Data Model.</returns>
        public EntityDataModel BuildModel()
        {
            EntityDataModel.Current = new EntityDataModel(this.collections);

            return EntityDataModel.Current;
        }

        /// <summary>
        /// Registers the type as a collection in the Entity Data Model with the specified collection name.
        /// </summary>
        /// <typeparam name="T">The type exposed by the collection.</typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        public void RegisterCollection<T>(string collectionName)
        {
            var edmType = (EdmComplexType)EdmTypeCache.FromClrType(
                typeof(T),
                clrType =>
                {
                    var clrTypeProperties = clrType.GetProperties().OrderBy(p => p.Name);

                    var edmProperties = new List<EdmProperty>();
                    var edmComplexType = new EdmComplexType(clrType.FullName, clrType, edmProperties);

                    edmProperties.AddRange(
                        clrTypeProperties.Select(
                            p => new EdmProperty(p.Name, EdmTypeCache.FromClrType(p.PropertyType), edmComplexType)));

                    return edmComplexType;
                });

            this.collections.Add(collectionName, edmType);
        }
    }
}