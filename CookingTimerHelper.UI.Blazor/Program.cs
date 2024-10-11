using CookingTimerHelper.Dal;
using CookingTimerHelper.Dal.DataAccessLayer;
using CookingTimerHelper.UI.Blazor.Components;
using Csla.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Builder;


namespace CookingTimerHelper.UI.Blazor
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddControllers();

            //// Add services to the container.
            //builder.Services.AddRazorComponents()
            //    .AddInteractiveServerComponents();


            //builder.Services.AddCascadingAuthenticationState();

            //builder.Services.AddHttpContextAccessor();

            //builder.Services.AddCsla(o => o
            //    .AddAspNetCore()
            //    .AddServerSideBlazor(o => o.UseInMemoryApplicationContextManager = false));

            //builder.Services.AddTransient(typeof(IFoodMenuDal), typeof(InMemoryFoodMenuRepository));



            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseWebAssemblyDebugging();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error", createScopeForErrors: true);
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();

            //app.UseStaticFiles();
            //app.UseAntiforgery();

            //app.MapControllers();

            //app.MapRazorComponents<App>()
            //    .AddInteractiveServerRenderMode();

            //app.Run();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddCascadingAuthenticationState();

            // CSLA requires AddHttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            // Add CSLA
            builder.Services.AddCsla(o => o
              .AddAspNetCore()
              .AddServerSideBlazor(o => o.UseInMemoryApplicationContextManager = false)
              .Security(so => so.FlowSecurityPrincipalFromClient = true)
              .DataPortal(dpo => dpo
                .AddServerSideDataPortal()
                .ClientSideDataPortal(co => co
                  .UseLocalProxy())));

            // configure DAL services for EF or Mock:

            //for EF Db
            //builder.Services.AddTransient(typeof(DataAccess.IPersonDal), typeof(DataAccess.EF.PersonEFDal));
            //builder.Services.AddDbContext<DataAccess.EF.PersonDbContext>(
            //options => options.UseSqlServer("Server=servername;Database=personDB;User ID=sa; Password=pass;Trusted_Connection=True;MultipleActiveResultSets=true"));

            // for Mock Db
            builder.Services.AddTransient(typeof(IFoodMenuDal), typeof(InMemoryFoodMenuRepository));

            // Required by CSLA data portal controller. If using Kestrel:
            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // Required by CSLA data portal controller. If using IIS:
            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapControllers();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode();

            await app.RunAsync();
        }
    }
}