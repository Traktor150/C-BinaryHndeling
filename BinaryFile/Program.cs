using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace BinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Person> people;
            people = new List<Person>();

            for (int i = 0; i < 3; i++)
            {

                Console.WriteLine("Ange dit förnamn");
                string firstN = Console.ReadLine();
                Console.WriteLine("Ange dit efternamn");
                string lastN = Console.ReadLine();

                people.Add(new Person { firstname = firstN, lastname = lastN });
                Console.WriteLine();
            }

            FileStream fs = new FileStream("DataFile.bin", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, people);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            List<Person> SavedPeople = null;

            FileStream fs1 = new FileStream("DataFile.bin", FileMode.Open);
            try
            {
                BinaryFormatter formatter1 = new BinaryFormatter();

                SavedPeople = (List<Person>)formatter1.Deserialize(fs1);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                foreach (var p in SavedPeople)
                {
                    Console.WriteLine(p.firstname + " " + p.lastname);
                }
            }

        }
    }
    [Serializable]
    class Person
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}
