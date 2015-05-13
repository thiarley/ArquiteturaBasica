using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace ArquiteturaBasica.Exceptions
{
    /// <summary>
    /// Classe que representa uma Exception na camada de dados
    /// </summary>
    [Serializable]
    public class NegocioException : Exception
    {
        private List<object> _errors = new List<object>();

        /// <summary>
        /// Construtor
        /// </summary>
        public NegocioException()
        { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">messagem da Exception</param>
        public NegocioException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">messagem da Exception</param>
        public NegocioException(string message, Exception innerException)
            : base(message,innerException)
        {
        }
    }
}
