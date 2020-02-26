﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataRequestActionFilterAttributeTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void DoesNotAdd_MetadataLevel_ForMetadataRequest()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/$metadata");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

            NameValueHeaderValue metadataParameter = httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

            Assert.Null(metadataParameter);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForApplicationXml()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");
            httpRequestMessage.Headers.Add("Accept", "application/xml");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.UnsupportedMediaType, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("415", odataErrorContent.Error.Code);
            Assert.Equal("A supported MIME type could not be found that matches the acceptable MIME types for the request. The supported type(s) 'application/json;odata.metadata=none, application/json;odata.metadata=minimal, application/json, text/plain' do not match any of the acceptable MIME types 'application/xml'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForFullMetadataLevel()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("odata.metadata 'full' is not supported by this service, please use 'none' or 'minimal'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForInvalidIsolationLevel()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");
            httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "ReadCommitted");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("If specified, the OData-Isolation must be 'Snapshot'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForInvalidMaxVersion()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");
            httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "3.0");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("If specified, the OData-MaxVersion header must be a valid OData version supported by this service between version 4.0 and 4.0.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForInvalidMetadataLevel()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=all");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("If specified, the odata.metadata value in the Accept header must be 'none', 'minimal' or 'full'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForInvalidVersion()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");
            httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "3.0");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("If specified, the OData-Version header must be a valid OData version supported by this service between version 4.0 and 4.0.", odataErrorContent.Error.Message);
        }

        public class WhenCalling_SendAsync_AndTheResponseHasNoContent
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public WhenCalling_SendAsync_AndTheResponseHasNoContent()
            {
                HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler(includeContentInResponse: false) });

                _httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsSetInTheResponse()
            {
                Assert.Null(_httpResponseMessage.Content);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsSetInTheResponse()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataVersion.MaxVersion.ToString(), _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCalling_SendAsync_WithAnODataUri_AndAllRequestOptionsInRequest
        {
            private readonly HttpResponseMessage _httpResponseMessage;
            private readonly ODataRequestOptions _odataRequestOptions;

            public WhenCalling_SendAsync_WithAnODataUri_AndAllRequestOptionsInRequest()
            {
                HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "Snapshot");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

                _httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

                httpRequestMessage.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions);
                _odataRequestOptions = odataRequestOptions as ODataRequestOptions;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_DataServiceRoot_IsSet()
            {
                Assert.Equal("https://services.odata.org/OData/", _odataRequestOptions.DataServiceRoot.ToString());
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_IsolationLevel_IsSetTo_Snapshot()
            {
                Assert.Equal(ODataIsolationLevel.Snapshot, _odataRequestOptions.IsolationLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_MetadataLevel_IsSetTo_None()
            {
                Assert.Equal(ODataMetadataLevel.None, _odataRequestOptions.MetadataLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_Version_IsSetTo_4_0()
            {
                Assert.Equal(ODataVersion.MaxVersion, _odataRequestOptions.Version);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void RequestParameters_ContainsODataRequestOptions()
            {
                Assert.NotNull(_odataRequestOptions);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsSetInTheResponse()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("none", metadataParameter.Value);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsSetInTheResponse()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataVersion.OData40.ToString(), _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCalling_SendAsync_WithAnODataUri_AndNoRequestOptionsInRequest
        {
            private readonly HttpResponseMessage _httpResponseMessage;
            private readonly ODataRequestOptions _odataRequestOptions;

            public WhenCalling_SendAsync_WithAnODataUri_AndNoRequestOptionsInRequest()
            {
                HttpRequestMessage httpRequestMessage = TestHelper.CreateHttpRequestMessage("/OData/Products");

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

                _httpResponseMessage = invoker.SendAsync(httpRequestMessage, CancellationToken.None).Result;

                httpRequestMessage.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions);
                _odataRequestOptions = odataRequestOptions as ODataRequestOptions;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_DataServiceRoot_IsSet()
            {
                Assert.Equal("https://services.odata.org/OData/", _odataRequestOptions.DataServiceRoot.ToString());
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_IsolationLevel_DefaultsTo_None()
            {
                Assert.Equal(ODataIsolationLevel.None, _odataRequestOptions.IsolationLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_MetadataLevel_DefaultsTo_Minimal()
            {
                Assert.Equal(ODataMetadataLevel.Minimal, _odataRequestOptions.MetadataLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_Version_DefaultsTo_MaxVersion()
            {
                Assert.Equal(ODataVersion.MaxVersion, _odataRequestOptions.Version);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void RequestParameters_ContainsODataRequestOptions()
            {
                Assert.NotNull(_odataRequestOptions);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsSetInTheResponse()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsSetInTheResponse()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataVersion.MaxVersion.ToString(), _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCalling_SendAsync_WithoutAnODataUri
        {
            private readonly HttpRequestMessage _httpRequestMessage;
            private readonly HttpResponseMessage _httpResponseMessage;

            public WhenCalling_SendAsync_WithoutAnODataUri()
            {
                _httpRequestMessage = TestHelper.CreateHttpRequestMessage("/api/Products");

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler { InnerHandler = new MockHttpMessageHandler() });

                _httpResponseMessage = invoker.SendAsync(_httpRequestMessage, CancellationToken.None).Result;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void RequestParameters_DoesNotContainsODataRequestOptions()
            {
                Assert.False(_httpRequestMessage.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions));
                Assert.Null(odataRequestOptions);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsNotSet()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.Null(metadataParameter);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsNotSet()
            {
                Assert.False(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
            }
        }

        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly bool _includeContentInResponse;

            public MockHttpMessageHandler(bool includeContentInResponse = true)
            {
                _includeContentInResponse = includeContentInResponse;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (_includeContentInResponse)
                {
                    return Task.FromResult(new HttpResponseMessage { Content = new StringContent("data") });
                }

                return Task.FromResult(new HttpResponseMessage());
            }
        }
    }
}
