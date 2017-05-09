﻿// -----------------------------------------------------------------------
// <copyright file="MetadataLevelExtensions.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData
{
    using System;
    using System.Net.Http.Headers;

    internal static class MetadataLevelExtensions
    {
        internal const string HeaderName = "odata";
        private static readonly NameValueHeaderValue MetadataLevelFull = new NameValueHeaderValue(HeaderName, "verbose");
        private static readonly NameValueHeaderValue MetadataLevelMinimal = new NameValueHeaderValue(HeaderName, "minimalmetadata");
        private static readonly NameValueHeaderValue MetadataLevelNone = new NameValueHeaderValue(HeaderName, "nometadata");

        internal static NameValueHeaderValue ToNameValueHeaderValue(this MetadataLevel metadataLevel)
        {
            switch (metadataLevel)
            {
                case MetadataLevel.Minimal:
                    return MetadataLevelMinimal;

                case MetadataLevel.None:
                    return MetadataLevelNone;

                case MetadataLevel.Verbose:
                    return MetadataLevelFull;

                default:
                    throw new NotSupportedException(metadataLevel.ToString());
            }
        }
    }
}