using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Tp_Cuatrimestral_Equipo1A.Helpers
{
    public static class NormalizarStrings
    {
        public static string quitarAcentos(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            texto = texto.Trim().ToLower();


            var textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in textoNormalizado)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string NormalizarParaBusqueda(this string text)
        {
            return text.quitarAcentos().ToLower();
        }
    }
}