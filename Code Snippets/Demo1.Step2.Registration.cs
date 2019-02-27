// Registration
public class InfinityAppComponent:Composer {
    public void Compose(Composition composition){
        composition.ContentApps().Append<InfinityAppComponent>();
    }
}