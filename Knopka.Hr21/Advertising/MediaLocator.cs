using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Knopka.Hr21.Advertising
{
    public class MediaLocator
    {
        // В конструктор передаются данные о рекламоносителях и локациях.
        // ===== пример данных =====
        // Яндекс.Директ:/ru
        // Бегущая строка в троллейбусах Екатеринбурга:/ru/svrd/ekb
        // Быстрый курьер:/ru/svrd/ekb
        // Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
        // Газета уральских москвичей:/ru/msk,/ru/permobl/,/ru/chelobl
        // ===== конец примера данных =====
        // inputStream будет уничтожен после вызова конструктора.

        /// <summary>
        /// Словарь соответствия локаций и рекламоносителей. Ключ - рекламоноситель, Значение - локации"
        /// </summary>
        private readonly Dictionary<string, string> locationMediaDict;


        public MediaLocator(Stream inputStream)
        {
            locationMediaDict = new Dictionary<string, string>();
            foreach (string line in ReadAllLines(inputStream))
            {
                string[] splitLine = line.Split(":");
                if (splitLine.Count() == 2)
                    locationMediaDict[splitLine[0]] = splitLine[1];
            }
           
            //throw new NotImplementedException("Напиши реализацию");
        }

        // В метод передаётся локация.
        // Надо вернуть все рекламоносители, которые действуют в этой локации.
        // Например, GetMediasForLocation("/ru/svrd/pervik") должен вернуть две строки:
        // "Яндекс.Директ", "Ревдинский рабочий"
        // Порядок строк не имеет значения.
        public IEnumerable<string> GetMediasForLocation(string location)
        {
            foreach(KeyValuePair<string,string> entry in locationMediaDict)
            {
                if (entry.Value.Split(',').Any(x=> location.StartsWith(x)))
                    yield return entry.Key;
            }
        }

        public IEnumerable<string> ReadAllLines(Stream stream)
        {
            using(StreamReader reader = new StreamReader(stream))
            {
                string currentLine;
                while((currentLine = reader.ReadLine()) != null)
                {
                    yield return currentLine;
                }

            }
        }
    }
}