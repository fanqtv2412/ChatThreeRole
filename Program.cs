using ChatThreeRole.Models;
using Microsoft.EntityFrameworkCore;
using ChatThreeRole.Hubs;
using ChatThreeRole.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<GroupService>();
builder.Services.AddTransient<MessageService>();
builder.Services.AddTransient<AccountService>();
builder.Services.AddTransient<ChatHub>();
builder.Services.AddDbContext<MyDBContext>(options=>
    options.UseSqlServer(builder.Configuration["SqlServerConnection"])
);
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options=>{
    options.Cookie.Name = "cookie-auth";
    options.LoginPath = "/Home/Login";
    options.LogoutPath = "/Home/Logout";
}).AddCookie("CookieAdmin", options=>{
    options.Cookie.Name = "cookie-admin";
    options.LoginPath = "/Admin/Login";
    options.LogoutPath = "/Admin/Logout";
});
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.MapHub<ChatHub>("/ChatHub");

app.Run();
