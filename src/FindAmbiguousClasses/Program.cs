using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FindAmbiguousClasses
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Dictionary<string, string> types = new Dictionary<string, string>();
            
            // Get all classes in assembly
            Assembly myAssembly = Assembly.GetEntryAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                types.Add(type.FullName, type.Name);
            }

            // All classes full name, and just name
            Console.WriteLine("---- All classes and namespaces ----");
            foreach (var item in types)
                Console.WriteLine($"{item.Key} : {item.Value}");

            // Get classes that have duplicates
            var classesWithDupes = types.GroupBy(gb => gb.Value)
                .Where(w => w.Count() > 1)
                .Select(s => new
                {
                    DupeName = s.Key,
                    CountOfDupes = s.Count()
                });

            // Print classes that have duplicates and their number of occurences
            Console.WriteLine("---- Duplicate class names ----");
            foreach (var item in classesWithDupes)
                Console.WriteLine($"{item.DupeName} occurs {item.CountOfDupes} times");

            // Print namespaces/classes that have dupes
            Console.WriteLine("---- Duplicate class names and their namespaces ----");
            foreach (var item in classesWithDupes)
            {
                var namespaces = types
                    .Where(w => w.Value == item.DupeName);

                foreach (var fullName in namespaces)
                    Console.WriteLine(fullName.Key);
            }
        }
    }
}
