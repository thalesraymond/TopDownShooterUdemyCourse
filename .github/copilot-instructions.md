# Copilot Instructions — TopDownShooterUdemyCourse

## Project Overview

This is a **Top-Down Shooter** built with **Unity 6 (6000.0.60f1)** using **C# 9.0** and the **Universal Render Pipeline (URP)**. It uses the **New Input System**, **Animation Rigging**, and a **hierarchical concurrent state machine architecture** for both player and weapon behavior.

## Architecture

### State Machine Pattern

The project uses a custom **concurrent state machine** where multiple states can be active simultaneously (e.g., aiming + walking + falling). Each state declares `ConflictingStates` to prevent incompatible combinations.

- `PlayerStateMachine` / `WeaponStateMachine` — manage a `HashSet` of active states
- `PlayerState` / `WeaponState` — abstract base classes with `CanActivate()`, `Enter()`, `Update()`, `Exit()`
- States are activated/deactivated each frame based on conditions and conflict resolution

When adding new states:

1. Inherit from `PlayerState` or `WeaponState`
2. Define `ConflictingStates` to declare mutual exclusions
3. Implement `CanActivate()` with the entry condition
4. Register the new state in `Player.cs` or `Weapon.cs` constructors

### Entity Hierarchy

`Entity` (MonoBehaviour) → `Player`, `Weapon`

### Data-Driven Design

Weapon stats are defined via **ScriptableObjects** (`WeaponData` extends `ItemData`). New weapons are authored as `.asset` files under `Assets/Scripts/ScriptableObjects/WeaponDatas/`. Always prefer ScriptableObjects for configuration data over hardcoded values.

### Input Management

`InputManager` is a **singleton** wrapping the auto-generated `Controls` class from the New Input System. All input queries go through `InputManager.Instance`. Never read input directly from `UnityEngine.Input` — always use the InputManager.

## Project Structure

```
Assets/
  Scripts/
    Entity.cs                          # Base entity class
    Inputs/
      InputManager.cs                  # Singleton input manager
      Controls.cs                      # Auto-generated (DO NOT EDIT)
      Controls.inputactions            # Input action asset
    PlayerStates/
      Player.cs                        # Player controller + state init
      PlayerStateMachine.cs            # Concurrent state machine
      PlayerState.cs                   # Abstract state base
      PlayerIdleState.cs
      PlayerWalkState.cs
      PlayerRunningState.cs
      PlayerAimingState.cs
      PlayerShootingState.cs
      PlayerReloadingState.cs
      PlayerFallingState.cs
    WeaponStates/
      Weapon.cs                        # Weapon controller
      WeaponStateMachine.cs            # Weapon state machine
      WeaponState.cs                   # Abstract weapon state base
      WeaponIdleState.cs
      WeaponFiringState.cs
      WeaponReloadState.cs
    ScriptableObjects/
      ItemData.cs                      # Base item SO
      WeaponData.cs                    # Weapon config SO
      WeaponDatas/                     # Weapon asset instances
  Animations/
    Controllers/                       # Animator controllers
    MainCharacter/                     # Character model + clips
    Weapons/                           # Weapon model + clips
  Prefabs/                             # Prefab instances
  Scenes/
    SampleScene.unity                  # Main gameplay scene
  Textures/
  PrototypeMaterials/
```

## Coding Conventions

### Naming

- **PascalCase** for public fields, properties, methods, and classes
- **camelCase** for private/local variables
- **Prefix private fields** with lowercase (e.g., `player`, `weapon`) — no underscore prefix used in this project
- **State classes** follow `Player<StateName>State` / `Weapon<StateName>State` pattern
- **ScriptableObjects** follow `<Type>Data` pattern (e.g., `WeaponData`, `ItemData`)

### Code Style

- Use `[SerializeField]` for inspector-exposed private fields; avoid public fields
- Use C# properties with `{ get; private set; }` for read-only external access
- Cache component references in `Awake()` or via constructor injection
- Pre-hash animator parameter strings using `Animator.StringToHash()` for performance
- Use `[Header("Section")]` attributes to organize inspector fields
- Prefer coroutines (`IEnumerator` + `StartCoroutine`) for time-delayed gameplay logic (e.g., fire rate)

### Unity Patterns

- **One MonoBehaviour per file**, file name matches class name
- Components are retrieved via `GetComponent<T>()` / `GetComponentInChildren<T>()` in `Awake()`
- Gravity is applied manually via `CharacterController.Move()` — not using Rigidbody physics
- Animation layers are used for weapon-specific upper body animations with masks
- `Animation Rigging` (`Rig` component) is used for procedural aiming; weight is toggled during reload

## Unity & Game Dev Best Practices

### Performance

- **Avoid `Find()` calls at runtime** — cache references in `Awake()` or `Start()`, or assign via Inspector
- **Use object pooling** for bullets, particles, and frequently spawned/despawned objects instead of `Instantiate`/`Destroy`
- **Minimize per-frame allocations** — avoid `new` in `Update()`, use structs, pre-allocate collections
- **Use `CompareTag("Tag")`** instead of `gameObject.tag == "Tag"` for tag comparisons
- **Hash animator parameters** with `Animator.StringToHash()` — never pass raw strings per frame
- **Use `TryGetComponent<T>()`** over `GetComponent<T>()` when the component may not exist
- **Avoid LINQ in hot paths** (`Update`, `FixedUpdate`) — use loops instead
- **Physics raycasts**: use `LayerMask` filtering and `maxDistance` to limit cost
- **Use `NonAlloc` physics APIs** (`RaycastNonAlloc`, `OverlapSphereNonAlloc`) for zero-allocation queries

### Architecture

- **Prefer composition over inheritance** — attach behavior via components, not deep class hierarchies
- **Use ScriptableObjects for data** — weapon stats, enemy configs, level settings, ability definitions
- **Separate data from logic** — SO holds values, MonoBehaviour holds behavior
- **Use events / delegates for decoupling** — `System.Action`, `UnityEvent`, or C# events to notify systems without hard references
- **Keep `Update()` lightweight** — delegate logic to state machines, managers, or coroutines
- **Single Responsibility** — each script should do one thing well
- **Avoid singletons when possible** — if needed, use the pattern sparingly and only for true global services (InputManager, AudioManager)

### State Machines

- When a state grows complex, consider sub-states or nested state machines
- Always handle edge cases in `CanActivate()` — e.g., can't shoot while reloading
- Keep `Enter()` / `Exit()` symmetric — anything enabled in `Enter()` must be disabled in `Exit()`
- Log state transitions during development for debugging: `Debug.Log($"Enter: {GetType().Name}")`

### Input System

- **Never modify `Controls.cs`** — it is auto-generated from `Controls.inputactions`
- Add new actions in the `.inputactions` asset, then regenerate
- Bind input reading in the InputManager; states check InputManager properties

### Animation

- Use **Animator Layers** with **Avatar Masks** for blending upper/lower body
- Keep blend trees clean — use 2D Freeform Directional for 8-direction movement
- Use **Animation Events** or **StateMachineBehaviours** to trigger gameplay logic at precise animation frames
- Avoid hard-coding animation clip lengths — query `AnimatorStateInfo.normalizedTime` or use events

### Physics & Movement

- Use `CharacterController` for player movement in top-down games — simpler than Rigidbody for this genre
- Apply gravity manually each frame when using CharacterController
- Use `FixedUpdate()` for Rigidbody physics, `Update()` for CharacterController movement
- Multiply movement by `Time.deltaTime` for frame-rate independence

### ScriptableObjects

- Create via `[CreateAssetMenu(fileName = "...", menuName = "...")]`
- Store them in organized folders (e.g., `WeaponDatas/`, `EnemyDatas/`)
- Never store runtime mutable state in SOs during play — they persist changes in the editor
- Use SOs for shared config: damage tables, loot tables, dialog data, audio configs

### Scene Management

- Keep scenes modular — separate gameplay, UI, and environment into additive scenes when the project grows
- Use prefabs for reusable objects; avoid duplicating GameObjects across scenes
- Organize hierarchy with empty GameObjects as folders (e.g., `--- Environment ---`, `--- UI ---`)

### Version Control (Git)

- Never commit `Library/`, `Temp/`, `Logs/`, or `obj/` folders (already gitignored)
- Commit `.meta` files — they track asset GUIDs and import settings
- Use **Visible Meta Files** and **Force Text** serialization (already configured)
- Avoid merging `.unity` scene files — coordinate scene ownership or use additive scenes

### Debugging & Testing

- Use `Debug.DrawRay()` and `Debug.DrawLine()` to visualize raycasts and directions in Scene view
- Use `#if UNITY_EDITOR` guards for editor-only debug code
- Implement `OnDrawGizmos()` / `OnDrawGizmosSelected()` for visual debugging of ranges, spawn zones, etc.
- Consider Play Mode tests for core systems (state machine transitions, weapon firing logic)

### Common Pitfalls

- **Don't use `Destroy()` on pooled objects** — deactivate with `SetActive(false)` instead
- **Don't access destroyed objects** — null-check or use the `?` operator (`component?.Method()`)
- **Beware of execution order** — use `Awake()` for self-init, `Start()` for cross-references, or set Script Execution Order
- **Don't put heavy logic in constructors** — Unity MonoBehaviours should initialize in `Awake()` / `Start()`
- **Coroutines stop when the GameObject is disabled** — use `Invoke` or manage lifecycle carefully
- **String comparisons are slow** — cache hashes, use enums, or use `CompareTag()`

## File Generation Guidelines

When generating new files for this project:

- Place scripts in the appropriate subfolder under `Assets/Scripts/`
- New state classes go under `PlayerStates/` or `WeaponStates/`
- New ScriptableObjects go under `ScriptableObjects/`
- Follow existing namespace-free convention (this project does not use namespaces)
- Include `using UnityEngine;` and other relevant usings
- Match the existing code style: no XML doc comments, minimal inline comments, clean formatting
