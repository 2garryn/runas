using Common;
public interface IrnPluginFactory {
    public IrnPlugin GetPlugin(IrnPluginRegistry pluginRegistry);
}