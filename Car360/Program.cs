using Car360.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyContext>(Options => Options.UseSqlServer(
    builder.Configuration.GetConnectionString("conn")));

builder.Services.AddMvc();

builder.Services.AddSession(Options => Options.IdleTimeout = TimeSpan.FromMinutes(20));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sign}/{action=Index}"

    );

app.UseSession();

app.Run();
