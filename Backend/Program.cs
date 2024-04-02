using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();

var app = builder.Build();






app.MapPost("/login", async (HttpContext context) => {
    // Read username and password from request
    var username = context.Request.Form["username"];
    var password = context.Request.Form["password"];
    
    User? user = Database.GetUser(username,password);

    // Check if username and password are valid
    if (user is not null) {
        // Generate and add session token
        var sessionToken = Guid.NewGuid();
        SessionManager.AddSession(sessionToken);
        

        // Return session token in response
        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsync(sessionToken.ToString());
    } else {
        // Unauthorized if username or password is incorrect
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
});

// Endpoint to access protected resource using session token
app.MapGet("/protected", async (HttpContext context) => {
    // Check if session token is provided in request header
    context.Request.Headers.TryGetValue("Authorization", out var sessionToken);
    if (!string.IsNullOrEmpty(sessionToken) && SessionManager.IsTokenSessionValid(sessionToken)) {
        // Authorized, return the protected resource
        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsync("Welcome to the protected resource!");
    } else {
        // Unauthorized if session token is missing or invalid
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
});

app.MapGet("/", () => "Hello World!");






app.Run();
