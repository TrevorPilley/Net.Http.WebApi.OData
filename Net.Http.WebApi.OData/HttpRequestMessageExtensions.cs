﻿// -----------------------------------------------------------------------
// <copyright file="HttpRequestMessageExtensions.cs" company="Project Contributors">
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
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// Extensions for the <see cref="HttpRequestMessage"/> class
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response message.</typeparam>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="value">The content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        public static HttpResponseMessage CreateODataResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value)
        {
            var response = request.CreateResponse(statusCode, value);
            response.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");

            return response;
        }
    }
}