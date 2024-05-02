using System.Collections.Generic;

namespace SaveSystem.Core
{
    public interface ISaveState
    {
        List<Dictionary<string, string>> CaptureState();
        void RestoreState(List<Dictionary<string, string>> loadedData);
    }
}
