using MongoDB.Driver;
using System;
using MVCfacturacion.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Para el correo electrónico

using System.Net.Mail;

namespace MVCfacturacion.Services
{
    public class FacturaService
    {
        private IMongoCollection<Factura> _facturas;  //Acá pasamos la entidad models que creamos que representa la estructura en la colección, o sea Factura
        //Creamos un constructor que va a estar inyectado
        //public enviarEmail _enviarEmail;
        public ClienteService _clienteService;
        
        public FacturaService(IFacturaSettings settings, ClienteService clienteService) //Pasamos los parámetro de la interface y usando settings indicamos que nuestra configuración se debe inyectar en settings
        {
            var cliente = new MongoClient(settings.server); //Lo que tenemos inyectado settings.nombreDelServidor donde server va a tener el valor de "server" en el archivo appsettings.json, que lo obtuvimos en Startup.cs con Configuration
            //vamos al servidor
            var database = cliente.GetDatabase(settings.database); //(Recibimos de settings el campo database) Lo mismo que hicimos antes pero con el valor de database Va a obtener una base de datos el parametro de settings que se llama database, que están en startup.json
            //obtenemos la base de datos
            _facturas = database.GetCollection<Factura>(settings.collection); //Usamos nuestro atributo privado de tipo IMongoCollection del modelo Facturas. <tipo> = Factura y que obtenga collection de lo que inyectamos.
            //y por último obtenemos la colección que queremos, que es Factura, para la cual usamos el atributo privado

            _clienteService = clienteService; //Inyectamos los servicios de ClienteService

        }



        //Quiero obtener la lista de un elemento del tipo Facturas con el método get
        public List<Factura> Get()
        {
            //Con nuestra función flecha en true vamos a obtener todos los elementos
            return _facturas.Find(d => true).ToList(); //Si queremos un dato específico podemos programar una condición d=>d.pago == false
            //Con esto obtenemos en una lista todos los datos desde mongoDb

        }

        //Creamos un método para obtener facturas según el nombre de la empresa
        public List<Factura> Get(string nombre)
        {
            return _facturas.Find(f => f.empresa == nombre).ToList(); //Configuramos el controlador para que obtenga el valor por la URL
        }

        //Creamos el servicio para insertar, es decir, un método que reciba algo
        public Factura Create(Factura factura) //Lo que vamos a recibir, que es un modelo Factura, espacio como vamos a llamar a lo que vamos a recibir.
        {
            _facturas.InsertOne(factura); //Insertamos lo que vamos a recibir
            return factura; //Aquí regresa la factura ya creada.
        } //Ahora implementamos este método en controller

        //Método para actualizar
        public void Update(string id, Factura factura) //El void para que no tenga que regresar nada
        {
            _facturas.ReplaceOne(factura => factura.Id == id, factura); //Comparamos aquel que tenga el id que envíamos 

            
            //Ejecutamos el proceso de envío
            enviar(factura);
        }

        //Método para eliminar
        public void Delete(string id)
        {
            _facturas.DeleteOne(d => d.Id == id);
        }
        public List<Cliente> GetClientes(string nombreEmpresa)
        {
            var cliente = _clienteService.Get(nombreEmpresa);
            
            //var ejemplo2 = ejemplo.empresa;
            return cliente;
            
        }/**/

        ///Método de envío de correos electrónicos =======================================
        ///Este metodo se puede refactorizar creando otro servicio que se dedique exclusivamente al envío de correos electrónicos y haciendo una segunda petición (al segundo servidor) desde el cliente de angular
        public void enviar(Factura factura)
        {
            Console.WriteLine(factura.empresa);
            var cliente = GetClientes(factura.empresa); //Obtenemos un lista con todos los clientes que tengan el nombre de la empresa
            var email = cliente[0].email; //De la lista cliente que obtuvimos del metodo GetCliente, elegimos el atributo email del primer item
            //ESTAMOS INTENTANDO ACCEDER A LOS DATOS OBTENIDOS POR EL SERVICIO DE CLIENTE
            //List<Cliente> cliente = _clienteService.Get(factura.empresa);
            //Console.WriteLine(cliente);
            

            string emailOrigen = "soyelpruebas@gmail.com";
            string emailDestino = email; //Agregando un campo adicional en los documentos de la base de datos se podría incluir el correo electrónico del cliente para que el email se envíe en función de la empresa y sus datos respectivos.
            string key = "entrarsoyelpruebas2020";
            string asunto = factura.estado + " para el pago.";
            string mensaje = "Cordial saludo " + factura.empresa + " <br>Este es el " + factura.estado + " para que pague su factura con id" + factura.Id + "por un total de " + factura.total; //Se podría cargar un HTML con la estructura de la factura
                       

            MailMessage oMailMessage = new MailMessage(emailOrigen, emailDestino, asunto, mensaje);   //Creamos un objeto de tipo MailMessage y lo instanciamos
            //Habilitamos opción de envío de html
            oMailMessage.IsBodyHtml = true;

            //Creamos un objeto SMTP encargado del transporte de información, en el cual declaramos el servicio host
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            //Inicializamos certificado SSL
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false; //Recomendable para el caso de gmail
            //oSmtpClient.Host = "smtp.gmail.com";
            oSmtpClient.Port = 587; //Puerto abierto de gmail
            oSmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, key);//Establecemos las credenciales de envío
            oSmtpClient.Send(oMailMessage);
        }


    }
}
