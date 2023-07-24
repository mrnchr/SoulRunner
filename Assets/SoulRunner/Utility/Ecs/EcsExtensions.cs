using System;
using System.Runtime.CompilerServices;
using Leopotam.EcsLite;

namespace SoulRunner.Utility.Ecs
{
  public static class EcsExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Add<T>(this EcsWorld world, int entity)
      where T : struct =>
      ref world.GetPool<T>().Add(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Get<T>(this EcsWorld world, int entity)
      where T : struct =>
      ref world.GetPool<T>().Get(entity);

    public static bool Has<T>(this EcsWorld world, int entity)
      where T : struct =>
      world.GetPool<T>().Has(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Del<T>(this EcsWorld world, int entity)
      where T : struct
    {
      world.GetPool<T>().Del(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Unpack(this EcsWorld world, EcsPackedEntity packed)
    {
      packed.Unpack(world, out int entity);
      return entity;
    }

    public static ref T Assign<T>(this ref T obj, Func<T, T> action)
      where T : struct
    {
      if (action != null)
        obj = action(obj);

      return ref obj;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T AddSoftly<T>(this EcsWorld world, int entity)
      where T : struct =>
      ref world.Has<T>(entity)
        ? ref world.Get<T>(entity)
        : ref world.Add<T>(entity);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DelSoftly<T>(this EcsWorld world, int entity)
      where T : struct
    {
      if(world.Has<T>(entity))
        world.Del<T>(entity);
    }
  }
}