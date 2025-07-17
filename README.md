# Interface Reference by Fofanius

The `Interface Reference` package provides an additional type that allows you to reference interfaces in your project.

## Usage

With provided [InterfaceReference<>](Runtime/InterfaceReference.cs) type you can easily reference to interface instance in your
project.

```csharp
public class Example : MonoBehaviour
{
    [SerializeField] private InterfaceReference<ICoolInterface> _abstractReference;
    [SerializeField] private InterfaceReference<ICoolInterface>[] _abstractReferences;

    private void Start()
    {
        PerformCoolProcess(_abstractReference.Resolve());
        
        if (_abstractReferences.FirstOrDefault().TryResolve(out var output))
        {
            PerformCoolProcess(output);
        }
    }
    
    private void PerformCoolProcess(ICoolInterface dependency)
    {
        // ...
        dependency.DoCoolStuff();
        // ...
    }
    
}
```

When using [`InterfaceReferenceResolver.Resolve/TryResolve`](Runtime/InterfaceReferenceResolver.cs) you don't have to null-check `InterfaceReference<>` - this is checked inside the method for you :)

### Referencing Limitations

| Instance to Usage         | MonoBehaviour | ScriptableObject |
|---------------------------|---------------|------------------|
| Scene Component           | +             | -                |
| Project Prefab            | + (*)         | + (*)            |
| Project Scriptable Object | +             | +                |

*(\*) attention - you are referring directly to the prefab, **not its instance.***

## Installation

Just add `"com.fofanius.unity-type-interface-reference":"https://github.com/Fofanius/unity-type-interface-reference.git#VERSION"` line to your project `manifest.json` file.

Example with version `1.1.0`:

```json
{
  "dependencies": {
    "com.fofanius.unity-type-interface-reference": "https://github.com/Fofanius/unity-type-interface-reference.git#1.1.0"
  }
}
```