using Core.Handlers.BackEndExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Common.Utils
{
    public static class PropertyMapper
    {
        /// <summary>
        ///     Property értékek összemappelése a forrás és a cél objektum között.
        /// </summary>
        /// <typeparam name="T">A Forrás objektum típusa</typeparam>
        /// <typeparam name="U">A cél objektum típusa</typeparam>
        /// <param name="source">A cél objektum</param>
        /// <param name="destination">A forrás objektum</param>

        public static void MapProperties<T, U>(T source, U destination) where T : class, new() where U : class, new()
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

            /// Reflection segítségével manipuláljuk a property-k értékeit.
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.GetType() == sourceProperty.GetType())
                {
                    try
                    {
                        /// Property nevek mappelése a cél és a forrás között
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException ex)
                    {
                        new BackEndException<ArgumentException>(ex).
                            ExceptionOperations("Hiba a Property-k mappelése közben!");
                    }
                }
            }
        }
    }
}
