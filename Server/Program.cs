using BlazorWebAssemblySignalRApp.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
      opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
         new[] { "application/octet-stream" });
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseResponseCompression();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapHub<ChatHub>("/chathub");
app.Run();
