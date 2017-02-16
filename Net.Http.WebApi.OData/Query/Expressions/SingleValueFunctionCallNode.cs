﻿// -----------------------------------------------------------------------
// <copyright file="SingleValueFunctionCallNode.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Expressions
{
    using System.Collections.Generic;

    /// <summary>
    /// A QueryNode which represents a function call.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public sealed class SingleValueFunctionCallNode : SingleValueNode
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SingleValueFunctionCallNode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SingleValueFunctionCallNode(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the kind of query node.
        /// </summary>
        public override QueryNodeKind Kind { get; } = QueryNodeKind.SingleValueFunctionCall;

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets the parameters for the function call.
        /// </summary>
        public IList<QueryNode> Parameters { get; } = new List<QueryNode>();
    }
}
