using AMS.Plugins.DapperSQL;
using AMS.Plugins.InMemory;
using AMS.UseCases.PluginInterfaces;
using AMS.UseCases.Reports;
using AMS.UseCases.Transactions;
using AMS.UseCases.Transactions.Interfaces;
using AMS.WebApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuditManagement"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AccountDbContext>();





builder.Services.AddDbContextFactory<AMSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuditManagement"));
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddTransient<ITransactionRepository, TransactionDapperRepository>();

builder.Services.AddTransient<IViewTransactionsByBranchNameUseCase, ViewTransactionsByBranchNameUseCase>();
builder.Services.AddTransient<IAddTransactionUseCase,AddTransactionUseCase>();
builder.Services.AddTransient<IEditTransactionUseCase,EditTransactionUseCase>();
builder.Services.AddTransient<IViewTransactionsByIdUseCase, ViewTransactionsByIdUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase,SearchTransactionsUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
