using DishoutOLO.Helpers.Provider;
using DishoutOLO.Repo;
using DishoutOLO.Repo.Interface;
using DishoutOLO.Service;
using DishoutOLO.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddAutoMapper(typeof(DishoutOLO.MapperConfiguration));
builder.Services.AddScoped<IitemService, ItemService>();
builder.Services.AddScoped<IItemRepositrory, ItemRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();    
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<LoggerProvider>();


//var connectionString = builder.Configuration.GetConnectionString("ConnectionStrings:ConnectDB");
builder.Services.AddDbContext<DishoutOLOContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectDB"]));
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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Index}/{id?}");

app.Run();
