
using Academic_Blog_App.Services;
using Academic_Blog_App.Services.ClientAjax;
using Academic_Blog_App.Services.Helper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddScoped<ApiHelper>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CenterHub>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.InjectService();
builder.Services.InjectInfracstucture(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSession();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
//app.MapRazorPages();
//app.MapHub<CenterHub>("/CenterHub");
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapHub<CenterHub>("/CenterHub");
});

app.Run();
