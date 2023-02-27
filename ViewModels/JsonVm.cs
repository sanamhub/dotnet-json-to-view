namespace MvcApp.ViewModels;

public class JsonVm {
    public Service Service { get; set; }
    public Provider Provider { get; set; }
    public string BeginTime { get; set; }
    public string EndTime { get; set; }
}

public class Service {
    public string Name { get; set; }
}

public class Provider {
    public int ProviderId { get; set; }
    public string Name { get; set; }
}
