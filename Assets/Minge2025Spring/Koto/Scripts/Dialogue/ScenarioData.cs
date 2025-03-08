using System;

namespace Dialogue
{
    [Serializable]
    public class Scenario
    {
        public string id;
        public string name;
        public string sentence;
        public string background;
    }

    [Serializable]
    public class ScenarioData
    {
        public Scenario[] scenes;
    }
}