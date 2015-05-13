using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace ArquiteturaBasica.Exceptions
{

    /// <summary>
    /// Classe que representa uma Exception ocorrida ao validar algo
    /// </summary>
    public class ValidacaoException : NegocioException
    {
        private List<object> _errors = new List<object>();

        /// <summary>
        /// Mensagens de Erros de validação.
        /// </summary>
        public List<object> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">messagem da Exception</param>
        public ValidacaoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message">messagem da Exception</param>
        public ValidacaoException(List<object> errors)
        {
            _errors = errors;
        }
    }
}
