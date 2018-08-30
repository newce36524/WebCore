using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
using Swashbuckle.AspNetCore.Swagger;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
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

            #region ���� Swagger API�ĵ�

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Web Api �ӿ��ĵ�",
                    Description = "Swagger WebApi����Ӧ��",
                    TermsOfService = "None"
                });

                //Set the comments path for the swagger json and ui.
                //var basePath = System.AppContext.BaseDirectory;
                //var xmlPath = Path.Combine(basePath, "DapperSwaggerAutofac.xml");
                //c.IncludeXmlComments(xmlPath);

                //c.OperationFilter<HttpHeaderOperation>(); // ���httpHeader����
            });

            #endregion

            #region Ĭ�������滻Autofac

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            return new AutofacServiceProvider(containerBuilder.Build());

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region ������������ҳ����

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            #endregion

            #region MVC ·�� https�ض��� ״̬ҳ���������

            app.UseRouter(routeBuilder =>
            {
                //routeBuilder.MapRoute(name: "default1", template: "test");
                //routeBuilder.MapGet("", context => Task.CompletedTask);//ע�ⲻҪ�������ã��������վ���ʲ�����Ԥ�ڣ��޷���ʾ��ҳ
                //MapGet MapPost MapPut MapDelete  ������������һ����  ������������·�ɶ��ƻ�
            })
            .UseStatusCodePages()//����״̬��ҳ��
            .UseHttpsRedirection()//http=>https ���ض���
            .UseCookiePolicy()
            .UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            })
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

            #endregion

            #region �����ļ�ϵͳ�ļ�����ͼ

            app.UseStaticFiles()//Ĭ���������wwwroot�ļ���
            .UseDefaultFiles(new DefaultFilesOptions()
            {//UseDefaultFiles ������ UseStaticFiles ֮ǰ���á�UseDefaultFiles ֻ����д�� URL������������ṩ������һ���ļ�������뿪����̬�ļ��м����UseStaticFiles�����ṩ����ļ���
                DefaultFileNames = new List<string>() { "test" }
            })
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
            });

            var option = new FileServerOptions()//��Ŀ¼���ʹ��ܣ�Ҳ�ܷ����ļ�
            {
                EnableDirectoryBrowsing = true,//���þ�̬�ļ���Ĭ���ļ���Ŀ¼�������
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticSource", "Images")),
                RequestPath = new PathString("/downStaticFiles")//���þ�̬�ļ��ķ���·�� ����https://localhost:44320/staticfiles/a.jpg
            };

            app.UseFileServer()//���þ�̬�ļ���Ĭ���ļ�����������ֱ�ӷ���Ŀ¼
             .UseFileServer(option);//���þ�̬�ļ��ķ��ʷ�ʽ����̬�ļ��ķ��ʲ����м�������������Ҳ���·�ɣ���Ϊ��ֻʶ��·����������·�ɵ������Action

            #endregion

            #region ����Swagger�м��

            app
            .UseSwagger()//ע�� ������û������SwaggerOptions��������һ�����Ǳ����
            //.UseSwagger(options =>
            //{
            //    options.RouteTemplate = "/swagger/v1/swagger.json";
            //    options.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
            //})
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger �ӿ� V1.0");
            });

            #endregion

            #region �����Զ����м��

            app.UseWebSockets()
            .UseMiddleware<AddEndpointMiddleware>("")
            .UseMiddleware<WebSocketMiddleware>()//ʹ�� WebSocket �м��
            .UseMiddleware<SampleMiddleware>();//ʹ���Զ����м��������ڲ��ṩ���Ĭ���м����Ҳ��ͨ�����ַ�ʽ��ӵģ�Ҳ����ͨ������IApplicationBuilder����չ��������ע��

            #endregion

            #region Map Use Run

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

            #endregion
        }
    }

    public class Rootobject
    {
        public string Param1 { get; set; }
        public string Param2 { get; set; }
    }

}
