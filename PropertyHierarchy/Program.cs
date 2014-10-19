using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace PropertyHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultProps = new List<PropertyHierarchy.Property>
                {
                    new PropertyHierarchy.Property(FontInfo.FontFamily, new FontFamily("Segoe UI")),
                    new PropertyHierarchy.Property(FontInfo.FontWeight, FontWeights.Bold),
                    new PropertyHierarchy.Property(FontInfo.FontSize, 12)
                };

            var propertyHierarchy = new PropertyHierarchy(defaultProps)
                {
                    {FontInfo.FontFamily, new FontFamily("Times New Roman")}
                };

            var x = propertyHierarchy[FontInfo.FontFamily];

            var propertyHierarchy2 = new PropertyHierarchy(propertyHierarchy)
                {
                    {FontInfo.FontSize, 16}
                };

            propertyHierarchy2.Remove(FontInfo.FontSize);

            foreach (var property in propertyHierarchy2)
            {
                Console.WriteLine(property.Key + ": " + property.Value + (property.IsOverriden ? " (Overriden)" : string.Empty));
            }

            Console.ReadLine();
        }
    }
}
