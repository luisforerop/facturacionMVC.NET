using System;
using System.Net.Mail;
using MVCfacturacion.Models;


namespace MVCfacturacion.Services
{
    public class enviarEmail 
    {
        //string emailOrigen = "soyelpruebas@gmail.com";
        //string emailDestino = "soyelpruebas@gmail.com";
        //string key = "entrarsoyelpruebas2020";
        //string asunto = "pruebita";
        //string mensaje = "this is the mensageichon"; //podemos cargar html
        //string emailOrigen, string emailDestino, string key, string asunto, string mensaje

        public void envioCorreo(infoEmail email)
        {
            /*
            //var datos = new infoEmail();

            //string emailOrigen = "soyelpruebas@gmail.com" ;
            //string emailDestino = "soyelpruebas@gmail.com";
            //string key = "entrarsoyelpruebas2020";
            //string asunto = "pruebita";
            //string mensaje = "this is the mensageichon"; //podemos cargar html

            MailMessage oMailMessage = new MailMessage(email.emailOrigen, email.emailDestino, email.asunto, email.mensaje);   //Creamos un objeto de tipo MailMessage y lo instanciamos
            //Habilitamos opción de envío de html
            oMailMessage.IsBodyHtml = true;

            //Creamos un objeto SMTP encargado del transporte de información, en el cual declaramos el servicio host
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            //Inicializamos certificado SSL
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false; //Recomendable para el caso de gmail
            //oSmtpClient.Host = "smtp.gmail.com";
            oSmtpClient.Port = 587; //Puerto abierto de gmail
            oSmtpClient.Credentials = new System.Net.NetworkCredential(email.emailOrigen, email.key);//Establecemos las credenciales de envío
            oSmtpClient.Send(oMailMessage);*/
            
        }
        public void sendIt(string i)
            {

            //enviarEmail.oSmtpClient.Send(oMailMessage); //configuramos el envío

            }

        public string plantillaEmail()
        {
            string cuerpoCorreo = "Este es una prueba para verificar que inyecte bien" ;
            
            return cuerpoCorreo;
        }
    }
}