using System.Reflection;

namespace TinyDependencyInjectionContainer {
    public class TinyDependencyInjectionContainer {
        public Dictionary<Type,Type> KnownAssociations { get; }
        public TinyDependencyInjectionContainer(string configPath) {
            string[] lines;
            try {
                lines = File.ReadAllLines(configPath);
            }
            catch (Exception e) {
                Console.WriteLine("Getting the config file was impossible:"+e);
                throw;
            }
            KnownAssociations = new Dictionary<Type, Type>();
            foreach (var line in lines) {
                if (String.IsNullOrEmpty(line)||line.StartsWith('#'))
                    continue;
                var parts = line.Split('*');
                if (parts.Length != 4)
                    throw new FormatException(
                        $"A line in the config file has {parts.Length} parts instead of the required 4");
                var interfaceType = GetType(parts[0], parts[1]);
                if (!interfaceType.IsInterface)
                    throw new ArgumentException($"Type {parts[1]} is not an interface");
                var implementationType = GetType(parts[2], parts[3]);
                if (!implementationType.IsClass)
                    throw new ArgumentException($"Type {parts[3]} is not a class");
                if (!interfaceType.IsAssignableFrom(implementationType))
                    throw new ArgumentException($"Class {parts[3]} does not implement interface {parts[1]}");
                if(!KnownAssociations.TryAdd(interfaceType, implementationType)) throw new ArgumentException();
            }
            Type GetType(string path, string typeName) {
                Assembly a;
                try {
                    a = Assembly.LoadFrom(path);
                }
                catch (Exception e) {
                    Console.WriteLine("no Assembly at path: "+path+e);
                    throw;
                }
                var t = a.GetType(typeName);
                if (null==t)
                    throw new TypeLoadException($"the type {t} does not exist in assembly {path}");
                return t;
            }

        }
        public T? Instantiate<T>() where T:class {
            if (!KnownAssociations.TryGetValue(typeof(T), out var implementingType))
                return null;
            return (T?)Activator.CreateInstance(implementingType);
        }
    }
}
