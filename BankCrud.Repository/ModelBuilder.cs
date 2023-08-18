
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BankCrud.Repository ;

public static partial class Extensions
{
    public static void NeedToRegisterMappingConfig(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("BankCrud.Repository", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x =>
                    x.IsGenericType
                    && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

        foreach (var item in typesToRegister)
        {
            dynamic service = Activator.CreateInstance(item);

            modelBuilder.ApplyConfiguration(service);
        }
    }

    public static void NeedToRegisterEntitiesConfig<T>(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("BankCrud.Domain", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x => x == typeof(T)))
            .ToList();

        foreach (var item in typesToRegister)
            modelBuilder.Entity(item);
    }
}


public static class AssemblyScanner
{
    private static readonly Dictionary<string, List<Type>> AllLoadedTypes = new();

    public static List<Type> AllTypes(string nameSpace, string pattern)
    {
        var theKey = nameSpace + "--" + pattern;

        if (AllLoadedTypes.ContainsKey(theKey) && AllLoadedTypes[theKey] != null)
            return AllLoadedTypes[theKey];

        //LogManager.GetLogger("AssemblyScanner").Info($"Loading assemblies for first time. nameSpace ={nameSpace}, pattern= {pattern} , infrastructure, preparations");

        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, $"{nameSpace}*.dll")
            .Where(path => new Regex($"{nameSpace.Replace(".", "\\.")}(.*){pattern}(.*)\\.dll$").IsMatch(path))
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

        if (AllLoadedTypes.ContainsKey(theKey))
            AllLoadedTypes[theKey] = assemblies.ToArray().SelectMany(x => x.GetTypes()).ToList();
        else

            AllLoadedTypes.Add(theKey, assemblies.ToArray().SelectMany(x => x.GetTypes()).ToList());

        return AllLoadedTypes[theKey];
    }
}
