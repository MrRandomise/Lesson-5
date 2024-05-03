using System.Collections.Generic;

namespace SaveSystem.Core
{
    public interface ISaveState
    {
        abstract List<Dictionary<string, string>> CaptureState();
        abstract void RestoreState(List<Dictionary<string, string>> loadedData);
    }
}
