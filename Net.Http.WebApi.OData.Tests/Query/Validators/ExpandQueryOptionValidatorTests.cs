﻿namespace Net.Http.WebApi.OData.Tests.Query.Validators
{
    using System.Net;
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Model;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class ExpandQueryValidatorTests
    {
        public class WhenTheExpandQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            public WhenTheExpandQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$expand=Category"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ExpandQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal("The query option $expand is not implemented by this service", exception.Message);
            }
        }

        public class WhenTheExpandQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Expand
            };

            public WhenTheExpandQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$expand=Category"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                ExpandQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }
    }
}