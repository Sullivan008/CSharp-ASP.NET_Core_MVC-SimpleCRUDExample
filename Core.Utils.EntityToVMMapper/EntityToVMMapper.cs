using Core.Handlers.BackEndExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utils.EntityToVMMapper
{
    /// <summary>
    ///     A DTOMapper osztály megvalósítja, hogy a Source Entitás Property-jeinek az értékeit
    ///     átmásolja a Destination Entitás Propertyjeinek az értékeibe
    /// </summary>
    /// <typeparam name="T">A Source Generikus Entitás Típusa</typeparam>
    /// <typeparam name="U">A Destination Generikus Entitás Típusa</typeparam>
    public static class EntityToVMMapper<T, U>
    {
        /// <summary>
        ///     A Map metódus megvalósítja, hogy a paraméterben átadott T Típusú Generikus Entitás (Source)
        ///     listát bejárva, a Propertyjeinek az értékeit átmásolja egy U Típusú Generikus Entitás (Destination)
        ///     Property-jeibe, majd azokat egy U Típusú Generikus Entitás Listába összegyűjtve visszatéríti.
        /// </summary>
        /// <param name="source">
        ///     A T Típusú Generikus Entitás Lista amelynek az elemeit át kell "Konvertálni" az U Típusú Cél objektumba
        /// </param>
        /// <returns>
        ///     U Típusú Generikus Entitás Lista, amely tartalmazza az átalakított T Típusú Generikus Entitás Lista Forrás Objektumainak a Propertyjeinek az értékeit
        /// </returns>
        public static List<U> Map(IEnumerable<T> source)
        {
            try
            {
                /// Vizsgálat hogy a source értéke NULL értéket képvisel-e.
                if (source == null)
                {
                    throw new NullReferenceException("Az EntityToDTOMapper-ben, a \"IEnumerable<T> source\" paraméter értéke nem lehet NULL!");
                }

                /// U Típusú Generikus Entitás Lista, amelyben az átkonvertált T Típusú Generikus Source Entitás objektumokat fogjuk tárolni
                List<U> dataAccessDestinationList = new List<U>();

                /// Bejárjuk a Forrás listát.
                foreach (var item in source.ToList())
                {
                    if (item != null)
                    {
                        /// Példányosítunk egy új EmitMapper objektumot, amely a Mappelést fogja végrehajtani.
                        EmitMapper<T, U> map = new EmitMapper<T, U>(item);

                        /// A Cél objektum listához, hozzáadjuk az új elkészített átkonvertált Destination objektumot.
                        /// Ehhez meghívjuk az EmitMapper Map metódusát, amely ha sikeresen végrehajtja a konvertálást,
                        /// akkor visszatéríti a Cél objektumot a Forrás objektum Propertyjeivel feltöltve.
                        dataAccessDestinationList.Add(map.Map());
                    }
                }

                /// Visszaadjuk a Cél objektum listát.
                return dataAccessDestinationList;
            }
            catch (NullReferenceException ex)
            {
                new BackEndException<NullReferenceException>(ex).
                    ExceptionOperations($"Hiba a Property-k Mappalése közben! \tDestination Type: {typeof(T)}.");

                return null;
            }
            catch (Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations($"Hiba a Property-k Mappalése közben! \tDestination Type: {typeof(T)}.");

                return null;
            }
        }

        /// <summary>
        ///     A Map metódus megvalósítja, hogy a paraméterben átadott T Típusú Generikus Entitás (Source)
        ///     Propertyjeinek az értékeit átmásolja egy U Típusú Generikus Entitás (Destination)
        ///     Property-jeibe, majd azt egy U Típusú Geneirkus Entitás adja vissza
        /// </summary>
        /// <param name="source">
        ///     A T Típusú Generikus Entitás amelynek a propertyjeit át kell "Konvertálni" az U Típusú Cél objektumba
        /// </param>
        /// <returns>
        ///     U Típusú Generikus Entitás amely tartalmazza az átalakított T Típusú Generikus Entitás Propertyjeinek az értékeit
        /// </returns>
        public static U Map(T source)
        {
            try
            {
                return new EmitMapper<T, U>(source).Map();
            }
            catch (Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations($"Hiba a Property-k Mappalése közben! \tDestination Type: {typeof(T)}.");

                return default(U);
            }
        }
    }
}
