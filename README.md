# Idle Game Dev Log / ReadMe

## Note

> I’m not sure why my Git commit doesn't show up in the GitHub commit section, even though it’s properly logged on my local PC. Sorry for the confusion.

---

## ✅ Core Gameplay

### `GameManager.cs`

* Contains the singleton setup:

  ```csharp
  public static GameManager Instance;
  public double AutoMultiplier;
  public double TapMultiplier;
  public double OfflineMultiplier;
  ```
* Used the **singleton pattern** to access this class globally without needing a reference.

### Event-Driven Architecture

* Used the **Observer Pattern** to handle upgrades cleanly:

  ```csharp
  ShopMenu.autoClickerUpgradeEvent += UpdateAutoMultiplier;
  ShopMenu.TapMultiplierUpgradeEvent += UpdateTapMultiplier;
  ShopMenu.OfflineMultiplierUpgradeEvent += UpdateOfflineMultiplier;
  ```

---

## 📈 Dynamic Upgrade Costs

* Used **ScriptableObjects** to define upgrade details.
* Upgrade prices scale dynamically using multipliers:

  ```csharp
  public Sprite UpgradeIcon;
  public string UpgradeName;
  public string UpgradeDescription;
  public double UpgradePrice;
  public double UpgradeLevel;
  public double UpgradeMultiplier;
  public bool IsPurchased;
  public bool CanBeHidden;
  public UpgradeType UpgradeType;
  ```

---

## 💰 Currency Formatting

To prevent overflow issues and improve UI clarity, I created a static utility:

```csharp
public static string FormatMoney(double value)
{
    if (value >= 1_000_000_000)
        return (value / 1_000_000_000d).ToString("0.##") + "B";
    else if (value >= 1_000_000)
        return (value / 1_000_000d).ToString("0.##") + "M";
    else if (value >= 1_000)
        return (value / 1_000d).ToString("0.##") + "K";
    else
        return value.ToString("0");
}
```

---

## 🎨 UI/UX & Animation

* Built the UI using **Unity GUI**.
* Integrated **DoTween** for smooth animations and transitions.

---

## 💾 Data & Persistence

### Saving

Data is saved in JSON format:

```csharp
string json = JsonConvert.SerializeObject(save);
string path = Path.Combine(Application.persistentDataPath, "save.json");
File.WriteAllText(path, json);
```

### Currently Saved Fields:

```csharp
autoMultiplier = 1.0;
tapMultiplier = 1.0;
current_Currency = 0;
offlineMultiplier = 0;
lastSaveTime = DateTime.Now.Ticks;
```

> Note: Upgrade levels are not saved yet — this feature is pending.

### ⏱️ Offline Earnings

On game start, the current time is compared to `lastSaveTime`. The difference is used to calculate offline earnings:

```csharp
long timeDiff = DateTime.Now.Ticks - save.lastSaveTime;
double secondsOffline = TimeSpan.FromTicks(timeDiff).TotalSeconds;
PlayerManager.Instance.current_Currency += secondsOffline * GameManager.Instance.offlineMultiplier;
```

---

## 🧠 Observer Pattern Usage

Events used for upgrades:

```csharp
public static Action<double> autoClickerUpgradeEvent;
public static Action<double> TapMultiplierUpgradeEvent;
public static Action OfflineMultiplierUpgradeEvent;
```

---

## 🧱 Upgrade Workflow Summary

1. Define upgrades using ScriptableObjects.
2. Display upgrades in UI dynamically.
3. On upgrade button click, update values and invoke relevant event.
4. GameManager listens to events and applies multipliers.

---

## 🔄 Resetting Data

> I didn’t get time to build a custom inspector for reset functionality, but...

I added a **Context Menu** on the Save class, which allows you to reset data via the Hierarchy:

* Select the `SaveAndLoad` object
* Use the context menu to reset the data

---

## ⚠️ Final Notes

I know some features like mock data and Addressables are still pending. These would require approximately one more day to implement.
