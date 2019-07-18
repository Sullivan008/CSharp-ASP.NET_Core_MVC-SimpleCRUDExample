using System;

namespace Core.Handlers.BackEndExceptionHandler
{
    /// <summary>
    /// <para>
    ///     Generikus Back End Exception Osztály.
    ///     A T paraméterben vár egy Exception típust amelynek megkötése
    ///     hogy az exception osztályból kell származnia.
    /// </para>
    /// <para>
    ///     Exception kezeléshez használjuk, ezt az osztályt. Ez alatt azt értjük
    ///     hogy a paraméterben átadott Exception-t megjelnítsük a Console-on.
    /// </para>
    /// </summary>
    /// <typeparam name="T">
    ///     Az Exception típusa. Megkötése, hogy az Exception osztályból
    ///     kell származnia.
    /// </typeparam>
    public class BackEndException<T> where T : Exception
    {
        /// <summary>
        ///     T típusú Generikus Exception.
        /// </summary>
        private T Exception;

        /// <summary>
        ///     Konstans Exception üzenet. Ez fog a Console-on megjelenni.
        /// </summary> 
        private const string ExCEPTION_MESSAGE = "\nERROR - Hiba! {0}!\n\n" +
            "Exception Message: {1}\n\n" +
            "InnerException Message {2}\n\n" +
            "Stack Trace: {3}";

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        /// <param name="Exception">
        ///     A paraméterben átadott T típusú Generikus Exception, amelynek
        ///     az üzeneteit akarjuk kiírni/ megjeleníteni/ letárolni.
        /// </param>
        public BackEndException(T Exception)
        {
            this.Exception = Exception;
        }

        public void ExceptionOperations(string additionalMessage)
        {
            /// Behelyettesítjük a Konstans hibaüzenetbe, az Exception által küldött Message-ket,
            /// továbbá a programozó által megadott egyedi hibaüzenet leírást.
            string finalExceptionMessage = FillExceptionMessage(additionalMessage);

            /// A hibaüzenet CONSOLE-ra való írása.
            System.Diagnostics.Debug.WriteLine($"\n+++++ {finalExceptionMessage}");
        }

        #region Helpers
        /// <summary>
        ///     Az osztályváltozóban megadott Konstans Hibaüzenetbe behellyettesíti
        ///     az Exception által tartalmazott Exception.Message, InnerException.Message
        ///     továbbá a StackTrace által tartalmazott hibaüzeneteket, illetve
        ///     a paraméterben átadott egyedi hibaüzenetet.
        /// </summary>
        /// <param name="additionalMessage">A programozó által megadott egyedi hibaüzenet leírása</param>
        /// <returns>
        ///     Az osztályváltozóban tárolt konstans hibaüzenet, teljes kitöltött változata.
        ///     Így a visszatérési érték, a teljes értékű hibaüzenet leírás.
        /// </returns>
        private string FillExceptionMessage(string additionalMessage)
        {
            return string.Format(ExCEPTION_MESSAGE, additionalMessage,
                Exception.Message,
                Exception.InnerException?.Message,
                Exception.StackTrace);
        }
        #endregion
    }
}
