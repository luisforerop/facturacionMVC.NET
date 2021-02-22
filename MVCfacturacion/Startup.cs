using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MVCfacturacion.Models;
using MVCfacturacion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCfacturacion
{
    public class Startup
    {
        //Configuración para con angular. 
        readonly string myCors = "myCors"; //Ahora agregamos el cors en services
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Agregar el Cors
            services.AddCors(options => {
                options.AddPolicy(name: myCors,  //Agregamos el string que creamos arriba
                builder =>
                {
                    builder.WithOrigins("*");   //Especificamos desde donde vamos a permitir la conexión de datos. Con el "*" permitimos peticiones desde cualquier dirección. Si solo queremos permitir la conxión desde un sitio determinado, debemos especificar la url.
                    builder.WithHeaders("*"); //Con esta instrucción aceptamos todas las peticiones enviadas por post para inserción de datos.
                    builder.WithMethods("*"); //Con esta instrucción aceptamos las peticiones de tipo put
                }); 
            }); //=============> PASAMOS A CONFIGURAR "configure", más abajo

            //MODELOS
            //Inyección de configuación del servidor de factura
            services.Configure<FacturaSettings>(Configuration.GetSection(nameof(FacturaSettings)));
            services.AddSingleton<IFacturaSettings>(d => d.GetRequiredService<IOptions<FacturaSettings>>().Value); //Cuando usamos singleton inyectamos para todo el proyecto. Quiere decir que en todas las solicitudes que se procese es el mismo objeto.
            
            
            
            //SERVICIOS
            //Inyección de servicio para obtener los datos de la colección factura
            services.AddSingleton<FacturaService>();
            //Inyectamos enviarEmail
            services.AddSingleton<enviarEmail>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(myCors);
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
