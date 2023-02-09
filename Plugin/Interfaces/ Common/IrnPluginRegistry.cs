namespace Common;
public interface IrnPluginRegistry {
    public IrnServiceRegistry Register(PluginMetadata metadata);
}