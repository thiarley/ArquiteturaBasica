using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArquiteturaBasica.Comuns.Utils.Enum
{
    /// <summary>
    /// Enumeradores dos possíveis erros do paciente.
    /// </summary>
    public enum PessoaErrors
    {
        NameNull, BirthDateNull, BirthDateNotValid, NameLength, CpfAlreadyExists,
        GenderNullOrInvalid, ZipCodeInvalid, PhotoWithoutExtension, EmailInvalid,
        UserBlocked
    }
}
