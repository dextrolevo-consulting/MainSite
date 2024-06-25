using AspNetStatic;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();




builder.Services.AddSingleton<IStaticResourcesInfoProvider>(
    new StaticResourcesInfoProvider(
        [
            new PageResource("/"),
            new PageResource("/privacy"),
           // new PageResource("/blog/articles/posts/1") { OutFile = "blog/post-1.html" },
           // new PageResource("/blog/articles/posts/2") { OutFile = "blog/post-2-dark.html", Query = "?theme=dark" },
            new CssResource("/bootstrap/bootstrap.min.css") { OptimizerType = OptimizerType.None },
            new CssResource("/site.css"),
            new JsResource("/site.js"),
            new BinResource("/favicon.png")
        ]));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//app.UseStaticPageFallback();     // re-route to the static file (page resources only)
//app.UseStaticFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//var outputPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
Console.WriteLine($"------------------------------{app.Environment.WebRootPath}------------------------------");

app.GenerateStaticContent(
    $"{app.Environment.WebRootPath}/staticSite",
    exitWhenDone: true,
    alwaysDefaultFile: false,
    dontUpdateLinks: false);
//app.GenerateStaticContent(@$"{outputPath}/staticSite");

app.Run();
