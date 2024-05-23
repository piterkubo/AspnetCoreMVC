using Projeto.Data;
using Projeto.helper;
using Projeto.Repositorio;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<BancoContext>
(options => options.UseMySql("server=localhost;initial catalog=teste;uid=root;pwd=senha",
ServerVersion.Parse("6.0.0-mysql")));





builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();




builder.Services.AddScoped<IUsuarioAdmRepositorio, UsuarioAdmRepositorio>();




builder.Services.AddScoped<IUsuarioPJRepositorio, UsuarioPJRepositorio>();





builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddScoped<ISessao, Sessao>();



builder.Services.AddScoped<IEmail, Email>();



builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});


var app = builder.Build();


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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
