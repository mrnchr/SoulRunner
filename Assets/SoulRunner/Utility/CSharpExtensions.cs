using System.Collections.Generic;

namespace SoulRunner.Utility
{
  public static class CSharpExtensions
  {
    public static List<T> AddItem<T>(this List<T> list, T item)
    {
      list.Add(item);
      return list;
    }
  }
}