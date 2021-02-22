using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCfacturacion.Models;
using MVCfacturacion.Services;


namespace MVCfacturacion.Controllers
{
    [Route("api/[controller]")] //Esta línea se usa para asignar el acceso 
    [ApiController]
    public class FacturaController : ControllerBase
    {
        //Creamos un atributo u objeto (traemos la clase desde Service) que será la representación del servicio que acabamos de crear
        public FacturaService _facturaService;
        //Vamos a utilizar el servicio inyectándolo
        //Inyectamos enviarEmail
        //public enviarEmail _enviarEmail;
        public FacturaController(FacturaService facturaService) //Quiero obtener FacturaService inyectado en facturaService (en el anterior lo inyectamos en settings)
        {
            _facturaService = facturaService; //lo inyectamos para ya tener hecho ese servicio en nuestro contructor Para ello debemos inyectarlo en Startup
        }
        //Para diferenciar entre el tipo de acceso a los métodos, configuramos para que el acceso al próximo método sea por medio del protocolo http
        [HttpGet]

        //Creamos un método que devuelva un ActionResult del tipo <Factura> (modelo. Por eso agregamos la carpeta Models) Nombramos el método como Get()
        public ActionResult<List<Factura>> Get() //El método ActionResult va a devolver una Lista con el método get del módelo factura
        {
        //Tenemos que hacer que esta función regrese información através del método _facturaService
            return _facturaService.Get(); //Get es el método que creamos en FacturaService llamado Get, y que se lo pasamos a _facturaService y lo que hacemos con esta línea es return lo que pase en ese método
        }

        //Método para obtener por URL
        [HttpGet("{empresa}")]

        public ActionResult<List<Factura>> Get(string empresa)
        {
            var factura = _facturaService.Get(empresa);
            return factura;
        }

        //Método para cargar datos en nuestra base de datos entrando por una petición post
        [HttpPost]

        public ActionResult<Factura> Create(Factura factura) //Este método va a regresar una Factura tal y como está en Models Factura
        {
            _facturaService.Create(factura); //Ejecutamos el método create de facturaService que está "alojado" en _facturaService, al cual le pasamos nuestra variable factura que es un modelo de Factura
            return Ok(factura);
        }

        [HttpPut]
        public ActionResult Update(Factura factura)
        {
            
            _facturaService.Update(factura.Id, factura );

            //Ejecutamos envío de correo
            //_enviarEmail.envioCorreo();
            return Ok("Actualizado correctamente " + factura.estado);
        }

        [HttpDelete] //Si queremos enviar la petición por el path, es decir por el mismo navegador, usamos: [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            _facturaService.Delete(id);
            return Ok(id + " Eliminado correctamente"); //Toca revisarlo porque no está funcionando
        }
    }
}
