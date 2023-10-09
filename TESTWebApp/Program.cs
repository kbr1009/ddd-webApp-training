using TESTWebApp;
using Microsoft.EntityFrameworkCore;
using TESTWebApp.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//#if DEBUG
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseInMemoryDatabase("inMemoryDB"));
//#else
// �ʂ̃A�Z���u����DBContext������ꍇ�̃f�[�^�}�C�O���[�V����
// https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBConnection"),
        x => x.MigrationsAssembly("TESTWebApp.Infrastructure")));
//#endif

// �ˑ����̒���(DI)
SetUpDependencyInjections.SetUp(builder);

// �Z�b�V�������p�̂��߈ȉ���ǉ�
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

// �Z�b�V�����̗��p
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
