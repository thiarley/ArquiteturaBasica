using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ArquiteturaBasica.Exceptions
{

    /// <summary>
    /// Representa todos os erros em que haja problema ao acessar a base de dados.
    /// </summary>
    public class AcessoDadosException : Exception
    {
        /// <summary>
        /// Construtor principal.
        /// </summary>
        /// <param name="message">messagem da Exception</param>
        public AcessoDadosException(string defaultMessage)
            : base(defaultMessage)
        {
        }

        public AcessoDadosException(Exception inner)
            : base(null, inner)
        { }

        /// <summary>
        /// Verifica se uma Exception é um erro de acesso a base de dados.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Quando verdadeiro será um erro de acesso a base de dados.</returns>
        public static bool IsDataAccessException(Exception ex)
        {
            return ex != null && ex.GetType() == typeof(SqlException) && ((SqlException)ex).Number == 1042;
        }
    }
}
