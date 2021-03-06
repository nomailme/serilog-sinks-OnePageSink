﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Serilog.Sinks.OnePageSink;
using Serilog.Sinks.OnePageSink.OnePageSinkMiddleware;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class OnePageSinkMiddlewareTests
    {
        [Fact]
        public async Task Invoke_IndexFile_FileContents()
        {
            // Arrange
            IOptions<OnePageSinkOptions> options = Options.Create(new OnePageSinkOptions());
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();

            var middleware = new OnePageSinkMiddleware(async (innerHttpContext) => await Task.Delay(0), options);

            context.Features.Get<IHttpResponseFeature>().Body = responseBodyStream;
            context.Request.Path = "/onepage/";

            //Act
            await middleware.Invoke(context);

            responseBodyStream.Position = 0;
            string result = new StreamReader(context.Response.Body).ReadToEnd();

            //Assert
            Assert.NotEmpty(result);
            Assert.True(true);
        }

        [Fact]
        public async Task Invoke_DefaultRoute_FileContents()
        {
            // Arrange
            IOptions<OnePageSinkOptions> options = Options.Create(new OnePageSinkOptions());
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();

            var middleware = new OnePageSinkMiddleware(async (innerHttpContext) => await Task.Delay(0), options);

            context.Features.Get<IHttpResponseFeature>().Body = responseBodyStream;
            context.Request.Path = "/onepage";

            //Act
            await middleware.Invoke(context);

            //Assert
            Assert.Equal(302, context.Response.StatusCode);
            Assert.True(true);
        }


        [Fact]
        public async Task Invoke_UnknownPath_FileContents()
        {
            // Arrange
            IOptions<OnePageSinkOptions> options = Options.Create(new OnePageSinkOptions());
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();

            var middleware = new OnePageSinkMiddleware(async (innerHttpContext) => await Task.Delay(0), options);

            context.Features.Get<IHttpResponseFeature>().Body = responseBodyStream;
            context.Request.Path = "";

            //Act
            await middleware.Invoke(context);

            //Assert
            Assert.True(true);
        }

        [Fact]
        public async Task Invoke_JavascriptFile_FileContents()
        {
            // Arrange
            IOptions<OnePageSinkOptions> options = Options.Create(new OnePageSinkOptions());
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();

            var middleware = new OnePageSinkMiddleware(async (innerHttpContext) => await Task.Delay(0), options);

            context.Features.Get<IHttpResponseFeature>().Body = responseBodyStream;
            context.Request.Path = "/onepage/~/js/build.js";

            //Act
            await middleware.Invoke(context);

            responseBodyStream.Position = 0;
            string result = new StreamReader(context.Response.Body).ReadToEnd();

            //Assert
            Assert.NotEmpty(result);
            Assert.True(true);
        }
    }
}
