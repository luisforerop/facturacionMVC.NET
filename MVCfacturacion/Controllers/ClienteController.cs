
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCfacturacion.Models;
using MVCfacturacion.Services;


namespace MVCfacturacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Esta línea se usa para asignar el acceso 
    public class ClienteController : ControllerBase
    {
        //Creamos un atributo u objeto (traemos la clase desde Service) que será la representación del servicio que acabamos de crear
        public ClienteService _clienteService;
        //Vamos a utilizar el servicio inyectándolo

        public ClienteController(ClienteService clienteService) //Obtenemos clienteService inyectado en clienteService 
        {
            _clienteService = clienteService; //esta "variable" recibe todos los atributos de clienteService porque lo inyectamos para tener acceso a ese servicio en nuestro contructor 
        }

        [HttpGet("{empresa}")]

        public ActionResult<List<Cliente>> Get(string empresa) //El método ActionResult va a devolver una Lista con el método get del módelo cliente
        {
            var cliente = _clienteService.Get(empresa);
            return cliente;         }

        [HttpGet]

        //Creamos un método que devuelva un ActionResult del tipo <Factura> (modelo. Por eso agregamos la carpeta Models) Nombramos el método como Get()
        public ActionResult<List<Cliente>> Get() 
        {
            return _clienteService.Get(); 
        }

        [HttpPost]

        public ActionResult<Cliente> Create(Cliente cliente) 
        {
            _clienteService.Create(cliente); 
            return Ok(cliente);
        }

        [HttpPut]

        public ActionResult Update(Cliente cliente) 
        {
            _clienteService.Update(cliente.Id, cliente);

            return Ok("Actualizado correctamente " + cliente.empresa);
        }

        [HttpDelete] //Si queremos enviar la petición por el path, es decir por el mismo navegador, usamos: [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            _clienteService.Delete(id);
            return Ok(id + " Eliminado correctamente"); //Toca revisarlo porque no está funcionando
        }

    }
}
