using MongoDB.Driver;
using MVCfacturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCfacturacion.Services
{
    public class ClienteService
    {
        private IMongoCollection<Cliente> _clientes;  //Acá pasamos la entidad models que creamos que representa la estructura en la colección, o sea Factura
                                                      //Creamos un constructor que va a estar inyectado

        public ClienteService(IClienteSettings settings) //Pasamos los parámetro de la interface y usando settings indicamos que nuestra configuración se debe inyectar en settings
        {
            var client = new MongoClient(settings.server); //Lo que tenemos inyectado settings.nombreDelServidor donde server va a tener el valor de "server" en el archivo appsettings.json.
            //vamos al servidor
            var database = client.GetDatabase(settings.database); //(Recibimos de settings el campo database) 
            //obtenemos la base de datos
            _clientes = database.GetCollection<Cliente>(settings.collection); //Usamos nuestro atributo privado de tipo IMongoCollection del modelo Cliente. <tipo> = Cliente y que obtenga collection de lo que inyectamos.
            //y por último obtenemos la colección que queremos, que es Cliente, para la cual usamos el atributo privado
        }

        public List<Cliente> Get(string nombre)
        {
            return _clientes.Find(f => f.empresa == nombre).ToList(); //Configuramos el controlador para que obtenga el valor por la URL
        }

        public Cliente Create(Cliente cliente) //Lo que vamos a recibir, que es un modelo Factura, espacio como vamos a llamar a lo que vamos a recibir.
        {
            _clientes.InsertOne(cliente); //Insertamos lo que vamos a recibir
            return cliente; //Aquí regresa la factura ya creada.
        }

        public void Update(string id, Cliente cliente) //El void para que no tenga que regresar nada
        {
            _clientes.ReplaceOne(cliente => cliente.Id == id, cliente); //Comparamos aquel que tenga el id que envíamos 

        }

        public void Delete(string id)
        {
            _clientes.DeleteOne(d => d.Id == id);
        }



    }
}
