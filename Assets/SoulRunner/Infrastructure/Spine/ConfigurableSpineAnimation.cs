using System;
using Spine.Unity;

namespace SoulRunner.Infrastructure.Spine
{
  [Serializable]
  public class ConfigurableSpineAnimation<TEnum>
    where TEnum : Enum
  {
    public TEnum Name;
    public AnimationReferenceAsset Asset;
    public bool IsLoop;
  }
}