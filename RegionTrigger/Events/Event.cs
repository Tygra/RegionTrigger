using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;
using TShockAPI.Hooks;
namespace RegionTrigger.Events {
    public abstract class BaseEvent {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract void OnRegionEntered(RegionHooks.RegionEnteredEventArgs args);
        public abstract void OnRegionLeft(RegionHooks.RegionLeftEventArgs args);

        public static BaseEvent FindEvent(string name) {
            Type type = Assembly.GetExecutingAssembly().ExportedTypes
                .Where(t => !t.IsAbstract && t.IsPublic && t.IsSubclassOf(typeof (BaseEvent)))
                .SingleOrDefault(t => t.Name == name);
            if (type == null)
                return null;
            return (BaseEvent)Activator.CreateInstance(type);
        }
    }

    public class MsgEvent:BaseEvent {
        public override string Name => "Message";
        public override string Description => "Send player a specific message in region.";
        public override void OnRegionEntered(RegionHooks.RegionEnteredEventArgs args) {
            throw new NotImplementedException();
        }

        public override void OnRegionLeft(RegionHooks.RegionLeftEventArgs args) {
            throw new NotImplementedException();
        }
    }

    public static class Test {
        public static void OutputAvailableEvents() {
            Assembly.GetExecutingAssembly().ExportedTypes
                .Where(t => !t.IsAbstract && t.IsPublic && t.IsSubclassOf(typeof(BaseEvent)))
                .Select(t => (BaseEvent)Activator.CreateInstance(t))
                .Select(e => e.Name)
                .ForEach(Console.WriteLine);
        }
    }
}
