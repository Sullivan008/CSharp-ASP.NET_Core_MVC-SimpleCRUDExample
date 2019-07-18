using EmitMapper;

namespace Core.Utils.EntityToVMMapper
{
    public class EmitMapper<T, U>
    {
        /// <summary>
        ///     A mappelési konfigurációt tartalmazó objektum.
        /// </summary> 
        private ObjectsMapper<T, U> mapper = ObjectMapperManager.DefaultInstance.GetMapper<T, U>();

        /// <summary>
        ///     A T típusú Generikus Entitást tartalmazó Forrás objektum
        /// </summary> 
        private T sourceEntity;

        /// <summary>
        ///     Konstruktor
        ///     
        ///     A forrás objektumnak megfelelően beállítja a konfigurációs beállításokat.
        /// </summary>
        /// <param name="source">A T Típusú Generikus Forrás objektum</param>
        public EmitMapper(T source)
        {
            sourceEntity = source;
        }

        /// <summary>
        ///     A Mappelést végrehajtó metódus.
        ///     
        ///     A T típusú Generikus Entitást átkonvertálja az U Típusú Generikus Entitássá, ha a
        ///     Mappalési feltételeknek megfelel.
        /// </summary>
        /// <returns>
        ///     Az átkonvertált U Típusú Generikus Cél Entitás
        /// </returns>
        public U Map()
        {
            /// A Mapper konfigurációja

            /// Mappalés végrehajtása, amely egy U Típusú Generikus Cél Objektumot állít elő.
            return mapper.Map(sourceEntity);
        }
    }
}
