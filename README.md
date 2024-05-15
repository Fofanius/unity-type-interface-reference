# Interface Reference by Fofanius

The `Interface Reference` package provides an additional type that allows you to reference interfaces in a scene.

## Installation

Just add `"com.fofanius.unity-type-interface-reference":"https://github.com/Fofanius/unity-type-interface-reference.git#VERSION"` line to your project `manifest.json` file.

Example with version `1.0.0`:

```json
{
  "dependencies": {
    "com.fofanius.observables": "https://github.com/Fofanius/unity-type-interface-reference.git#1.0.0"
  }
}
```

## Usage

With provided [wrapper type](Runtime/InterfaceReference.cs) you can easily reference to interface instance in your
project.

### Referencing Limitations

| Instance to Usage         | MonoBehaviour | ScriptableObject |
|---------------------------|---------------|------------------|
| Scene Component           | +             | -                |
| Project Prefab            | +             | +                |
| Project Scriptable Object | +             | +                |

### Usage Example

```csharp
public class Example : MonoBehaviour
{
    [SerializeField] private InterfaceReference<ICoolInterface> _abstractReference;

    private void Start()
    {
        PerformCoolProcess(_abstractReference?.GetValue());
    }
    
    private void PerformCoolProcess(ICoolInterface dependency)
    {
        if (dependency == null) throw new ArgumentNullException(nameof(dependency));
        
        // ...
    }
    
}
```