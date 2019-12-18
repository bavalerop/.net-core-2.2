using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace API_ENTERPRISE.Models
{
    public class RijndaelAlgorithm
    {
        const string L1 = "b/";
        const string L2 = "rY";
        const string L3 = "liMONE";
        const string L4 = "6u";
        const string L5 = "0Q";
        const string L6 = "gV";
        const string L7 = "WTUNJA";
        const string L8 = "==";
        const string V1 = "MiZiFu";
        const string V2 = "5l";
        const string V3 = "zS";
        const string V4 = "cN";
        const string V5 = "Nk";
        const string V6 = "zY";
        const string V7 = "TyOSNA";

        private byte[] ArregloLlaveEncripccion;
        private byte[] ArregloVectorInicializacion;
        private byte[] TextoDesencriptado;
        private byte[] TextoEncriptado;

        RijndaelManaged ManejadorEncripccion = new RijndaelManaged();

        /// <summary>
        /// Constructor de la Clase de Encripccion con Rijndael
        /// </summary>
        /// <param>   /// Llave de Encripccion para Rijndael   /// </param>
        /// <param>   ///Vector de Inicializacion para Rijndael   ///</param>

        public RijndaelAlgorithm()
        {
            ArregloLlaveEncripccion = Convert.FromBase64String(L1 + L2 + L4 + L3 + L5 + L6 + L7 + L8);
            ArregloVectorInicializacion = Convert.FromBase64String(V1 + V2 + V3 + V4 + V5 + V6 + V7 + L8);
        }

        ///Parámetro key debe tener minimo 4 caracteres, surge para cifrado en Consulta Web 
        ///y el reseteo de la contraseña para pacientes y médicos 
        public RijndaelAlgorithm(string key)
        {
            if (key.Length == 4)
                key = key + "AB";

            ArregloLlaveEncripccion = Convert.FromBase64String(L1 + L2 + L4 + key + L5 + L6 + L7 + L8);
            ArregloVectorInicializacion = Convert.FromBase64String(V1 + V2 + V3 + V4 + V5 + V6 + V7 + L8);
        }

        /// <summary>
        /// Metodo que encripta un Texto usando Rinjdael
        /// </summary>
        /// <param>Texto a Encriptar</param>
        /// <returns>Texto Encriptado</returns>
        public string Encripta(string Texto, bool genKey)
        {
            if (genKey)
            {
                ManejadorEncripccion.GenerateKey();
                ManejadorEncripccion.GenerateIV();

                ArregloLlaveEncripccion = ManejadorEncripccion.Key;
                ArregloVectorInicializacion = ManejadorEncripccion.IV;
            }

            ///Instanciamos un codificador ASCII
            ASCIIEncoding CodificadorASCII = new ASCIIEncoding();

            /*Creamos un Transformador Criptografico, Stream de Memoria     / y Stream de Criptografia*/
            ICryptoTransform TransformadorCriptografico = ManejadorEncripccion.CreateEncryptor(ArregloLlaveEncripccion, ArregloVectorInicializacion);
            MemoryStream StreamMemoria = new MemoryStream();
            CryptoStream SteamCriptografico = new CryptoStream(StreamMemoria, TransformadorCriptografico, CryptoStreamMode.Write);

            //Obtenemos los bytes del texto a encriptar
            TextoEncriptado = CodificadorASCII.GetBytes(Texto);

            /*Comenzamos la encriptacion transformando el arreglo de bytes 
            / y escribiendo en el stream de memoria*/
            SteamCriptografico.Write(TextoEncriptado, 0, TextoEncriptado.Length);
            SteamCriptografico.FlushFinalBlock();

            //Recuperamos los bytes del stream de memoria
            TextoEncriptado = StreamMemoria.ToArray();

            //Convertimos los bytes a un string de 64 bytes y lo regresamos
            return Convert.ToBase64String(TextoEncriptado);
        }

        /// <summary>
        /// Metodo que desencripta un Texto usando Rinjdael
        /// </summary>
        /// <param>Texto a Desencriptar</param>
        /// <returns>Texto Desencriptado</returns>
        public string Desencripta(string Texto)
        {
            //Se controla al desencriptar un password no válido.
            try
            {
                //Convertimos el texto encryptado a Bytes
                TextoEncriptado = Convert.FromBase64String(Texto);

                //Instanciamos un codificador ASCII
                ASCIIEncoding CodificadorASCII = new ASCIIEncoding();

                //Creamos un Transformador Criptografico, Stream de Memoria      / y Stream de Criptografia*/
                ICryptoTransform TransformadorCriptografico = ManejadorEncripccion.CreateDecryptor(ArregloLlaveEncripccion, ArregloVectorInicializacion); MemoryStream StreamMemoria = new MemoryStream(TextoEncriptado); CryptoStream StreamCriptografico = new CryptoStream(StreamMemoria, TransformadorCriptografico, CryptoStreamMode.Read);

                //Creamos un arreglo de bytes para guardar los bytes desencriptados
                TextoDesencriptado = new byte[TextoEncriptado.Length];

                //Escribimos en el arreglo los bytes desencriptados
                StreamCriptografico.Read(TextoDesencriptado, 0, TextoDesencriptado.Length);

                //Convertimos los bytes desencriptados a un string y lo regresamos
                return CodificadorASCII.GetString(TextoDesencriptado, 0, TextoDesencriptado.Length).TrimEnd().Trim('\0');
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Metodo que desencripta un Texto usando Rinjdael
        /// </summary>
        /// <param>Texto a Desencriptar</param>
        /// <returns>Texto Desencriptado</returns>
        public string Desencripta(string Texto, string key, string ValorIV)
        {
            ArregloLlaveEncripccion = Convert.FromBase64String(key);
            ArregloVectorInicializacion = Convert.FromBase64String(ValorIV);
            return Desencripta(Texto);
        }

        public string getKey
        {
            get { return Convert.ToBase64String(this.ArregloLlaveEncripccion); }
        }

        public string getIV
        {
            get { return Convert.ToBase64String(this.ArregloVectorInicializacion); }
        }
    }
}
