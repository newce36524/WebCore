using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using WebCore.Fileters;
using WebCore.Middleware;

namespace WebCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.AddDirectoryBrowser();

            var config = Configuration.GetSection("RootobjectSection").Get<Rootobject>();

            services.AddRouting();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(options =>
            {
                var o = options;
                //options.Filters.Add(typeof(ActionFilterAttribute)); // ȫ��ע�������
                options.Filters.Add(typeof(AjaxRequestFilterAttribute));
                //options.Filters.Add(typeof(MyResultFilterAttribute));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseRouter(routeBuilder =>
            {
                //routeBuilder.MapRoute(name: "default1", template: "test");
                //routeBuilder.MapGet("", context => Task.CompletedTask);//ע�ⲻҪ�������ã��������վ���ʲ�����Ԥ�ڣ��޷���ʾ��ҳ
                //MapGet MapPost MapPut MapDelete  ������������һ����  ������������·�ɶ��ƻ�
            })

             .UseStatusCodePages()//����״̬��ҳ��
             .UseHttpsRedirection()//http=>https ���ض���
             .UseDefaultFiles(new DefaultFilesOptions()
             {//UseDefaultFiles ������ UseStaticFiles ֮ǰ���á�UseDefaultFiles ֻ����д�� URL������������ṩ������һ���ļ�������뿪����̬�ļ��м����UseStaticFiles�����ṩ����ļ���
                 DefaultFileNames = new List<string>() { "test" }
             })
             .UseCookiePolicy()

             //*********************************************
             .UseStaticFiles()//Ĭ���������wwwroot�ļ���
             .UseStaticFiles(new StaticFileOptions()//
             {
                 ServeUnknownFileTypes = true,//�а�ȫ����  Ĭ�Ϲر� false
                 FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticSource", "Images")), //ָ����̬�ļ���Ŀ¼λ��
                 RequestPath = new PathString("/StaticFiles"),//���þ�̬�ļ��ķ���·�� ����https://localhost:44320/staticfiles/a.jpg
                 ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>()
                    {
                        { ".xxx","application/xxx"}//������չ��ӳ��
                    })
             })
             .UseStaticFiles(new StaticFileOptions()//
             {
                 ServeUnknownFileTypes = false,//�а�ȫ����  Ĭ�Ϲر� false
                 FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticSource", "Css")), //ָ����̬�ļ���Ŀ¼λ��
                 RequestPath = new PathString("/Style"),//���þ�̬�ļ��ķ���·�� ����https://localhost:44320/Style/styleSheet.css
                 ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>()
                    {
                        { ".css","text/css"}//������չ��ӳ��
                    })
             })
             .UseDirectoryBrowser(new DirectoryBrowserOptions()//ֻ��Ŀ¼���ʹ��ܣ����ܷ����ļ�
             {//Ĭ�Ͻ��ã�����Ŀ¼���ʹ���
                 FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory())),//ָ������Ŀ¼
                 RequestPath = new PathString("/app")//ָ������·�� ����https://localhost:44320/app/
             })

             .UseDirectoryBrowser(new DirectoryBrowserOptions()//ֻ��Ŀ¼���ʹ��ܣ����ܷ����ļ�
             {//Ĭ�Ͻ��ã�����Ŀ¼���ʹ���
                 FileProvider = new PhysicalFileProvider(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)),//ָ������Ŀ¼
                 RequestPath = new PathString("/Desktop")//ָ������·�� ����https://localhost:44320/Desktop/
             })

             //*********************************************
             .UseWebSockets()
             .UseMiddleware<AddEndpointMiddleware>("")//ʹ�� DoorChain �м��
             .UseMiddleware<WebSocketMiddleware>()//ʹ�� WebSocket �м��
             .UseMiddleware<SampleMiddleware>()//ʹ���Զ����м��������ڲ��ṩ���Ĭ���м����Ҳ��ͨ�����ַ�ʽ��ӵģ�Ҳ����ͨ������IApplicationBuilder����չ��������ע��
             .UseMvc()
             .UseMvc(routes =>
             {//����Ĭ��·��
                 routes.MapRoute(
                     name: "default",
                     template: "{controller=home}/{action=index}/{id?}");
                 routes.MapRoute(
                    name: "test",
                    template: "test");
             });

            var option = new FileServerOptions()//��Ŀ¼���ʹ��ܣ�Ҳ�ܷ����ļ�
            {
                EnableDirectoryBrowsing = true,//���þ�̬�ļ���Ĭ���ļ���Ŀ¼�������
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticSource", "Images")),
                RequestPath = new PathString("/downStaticFiles")//���þ�̬�ļ��ķ���·�� ����https://localhost:44320/staticfiles/a.jpg
            };

            app.UseFileServer()//���þ�̬�ļ���Ĭ���ļ�����������ֱ�ӷ���Ŀ¼
             .UseFileServer(option);//���þ�̬�ļ��ķ��ʷ�ʽ����̬�ļ��ķ��ʲ����м�������������Ҳ���·�ɣ���Ϊ��ֻʶ��·����������·�ɵ������Action

            app.MapWhen(httpcontext => httpcontext.Request.Path != "/ws", application =>
            {//ֻ�г�������ʱ�Ż�ִ��

            });
            app.Map("/path", _app =>
            {//ͨ��ָ��·���ַ��ܵ�
                _app.Run(async context =>
                {
                    await context.Response.WriteAsync("��ǰ·�ɲ�����");
                });
            });


            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.Use(async (context, next) =>
            {
                //ͨ��Use  �����м��
                //��һ���ܵ�ִ��ǰ
                await next();//��ʾִ����һ���ܵ�
                //��һ���ܵ�ִ�к�
            });


            app.Run(async context =>
            {//�������쳣�м��������ִ��֮��Ĺܵ�
                if (context.Request.Query.ContainsKey("throw"))
                {
                    await context.Response.WriteAsync("are you ok?");
                }
            });
        }
    }

    public class Rootobject
    {
        public string Param1 { get; set; }
        public string Param2 { get; set; }
    }

}
