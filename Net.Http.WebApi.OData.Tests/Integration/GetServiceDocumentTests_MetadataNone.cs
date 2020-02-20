﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Integration
{
    public class GetServiceDocumentTests_MetadataNone : IntegrationTest
    {
        private readonly HttpResponseMessage _httpResponseMessage;

        public GetServiceDocumentTests_MetadataNone()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://server/odata");
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            _httpResponseMessage = HttpClient.SendAsync(httpRequestMessage).Result;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_ApplicationJson()
        {
            Assert.Equal("application/json", _httpResponseMessage.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_Parameter_ODataMetadata()
        {
            Assert.Equal("none", _httpResponseMessage.Content.Headers.ContentType.Parameters.Single(x => x.Name == ODataMetadataLevelExtensions.HeaderName).Value);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ODataVersion()
        {
            Assert.Equal(ODataVersion.OData40.ToString(), _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Contains_ODataResponseContent()
        {
            Assert.NotNull(_httpResponseMessage.Content);

            string result = await _httpResponseMessage.Content.ReadAsStringAsync();

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"http://server/odata/Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"http://server/odata/Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"http://server/odata/Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"http://server/odata/Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"http://server/odata/Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"http://server/odata/Products\"}]}",
                result);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void StatusCode_OK()
        {
            Assert.Equal(HttpStatusCode.OK, _httpResponseMessage.StatusCode);
        }
    }
}