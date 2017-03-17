// -----------------------------------------------------------------------
// <copyright file="EdmPrimitiveType.cs" company="Project Contributors">
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
    using System;

    /// <summary>
    /// Represents a primitive type in the Entity Data Model.
    /// </summary>
    /// <seealso cref="Net.Http.WebApi.OData.Model.EdmType" />
    [System.Diagnostics.DebuggerDisplay("{Name}: {ClrType}")]
    internal sealed class EdmPrimitiveType : EdmType
    {
        private EdmPrimitiveType(string name, Type clrType)
            : base(name, clrType)
        {
        }

        /// <summary>
        /// Gets the EdmType which represent fixed- or variable- length binary data.
        /// </summary>
        internal static EdmType Binary { get; } = new EdmPrimitiveType("Edm.Binary", typeof(byte[]));

        /// <summary>
        /// Gets the EdmType which represents the mathematical concept of binary-valued logic.
        /// </summary>
        internal static EdmType Boolean { get; } = new EdmPrimitiveType("Edm.Boolean", typeof(bool));

        /// <summary>
        /// Gets the EdmType which represents an unsigned 8-bit integer value.
        /// </summary>
        internal static EdmType Byte { get; } = new EdmPrimitiveType("Edm.Byte", typeof(byte));

        /// <summary>
        /// Gets the EdmType which represents date and time with values ranging from 12:00:00 midnight; January 1; 1753 A.D. through 11:59:59 P.M; December 9999 A.D.
        /// </summary>
        internal static EdmType DateTime { get; } = new EdmPrimitiveType("Edm.DateTime", typeof(DateTime));

        /// <summary>
        /// Gets the EdmType which represents date and time as an Offset in minutes from GMT; with values ranging from 12:00:00 midnight; January 1; 1753 A.D. through 11:59:59 P.M; December 9999 A.D.
        /// </summary>
        internal static EdmType DateTimeOffset { get; } = new EdmPrimitiveType("Edm.DateTimeOffset", typeof(DateTimeOffset));

        /// <summary>
        /// Gets the EdmType which represents numeric values with fixed precision and scale. This type can describe a numeric value ranging from negative 10^255 + 1 to positive 10^255 -1
        /// </summary>
        internal static EdmType Decimal { get; } = new EdmPrimitiveType("Edm.Decimal", typeof(decimal));

        /// <summary>
        /// Gets the EdmType which represents a floating point number with 15 digits precision that can represent values with approximate range of ± 2.23e -308 through ± 1.79e +308
        /// </summary>
        internal static EdmType Double { get; } = new EdmPrimitiveType("Edm.Double", typeof(double));

        /// <summary>
        /// Gets the EdmType which represents a 16-byte (128-bit) unique identifier value.
        /// </summary>
        internal static EdmType Guid { get; } = new EdmPrimitiveType("Edm.Guid", typeof(Guid));

        /// <summary>
        /// Gets the EdmType which represents a signed 16-bit integer value.
        /// </summary>
        internal static EdmType Int16 { get; } = new EdmPrimitiveType("Edm.Int16", typeof(short));

        /// <summary>
        /// Gets the EdmType which represents a signed 32-bit integer value.
        /// </summary>
        internal static EdmType Int32 { get; } = new EdmPrimitiveType("Edm.Int32", typeof(int));

        /// <summary>
        /// Gets the EdmType which represents a signed 64-bit integer value.
        /// </summary>
        internal static EdmType Int64 { get; } = new EdmPrimitiveType("Edm.Int64", typeof(long));

        /// <summary>
        /// Gets the EdmType which represents a signed 8-bit integer value.
        /// </summary>
        internal static EdmType SByte { get; } = new EdmPrimitiveType("Edm.SByte", typeof(sbyte));

        /// <summary>
        /// Gets the EdmType which represents a floating point number with 7 digits precision that can represent values with approximate range of ± 1.18e -38 through ± 3.40e +38
        /// </summary>
        internal static EdmType Single { get; } = new EdmPrimitiveType("Edm.Single", typeof(float));

        /// <summary>
        /// Gets the EdmType which represents fixed- or variable-length character data.
        /// </summary>
        internal static EdmType String { get; } = new EdmPrimitiveType("Edm.String", typeof(string));

        /// <summary>
        /// Gets the EdmType which represents the time of day with values ranging from 0:00:00.x to 23:59:59.y; where x and y depend upon the precision.
        /// </summary>
        internal static EdmType Time { get; } = new EdmPrimitiveType("Edm.Time", typeof(TimeSpan));
    }
}