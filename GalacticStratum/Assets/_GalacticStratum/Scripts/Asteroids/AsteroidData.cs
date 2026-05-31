using System.Collections.Generic;
using UnityEngine;

public class AsteroidData
{
    public enum ResourceType
    {
        Nothing,
        Water,
        Beskarium,
        Peridot,
        Lechatelierite,
        Elaliite,
        Count
    }

    public enum AsteroidType
    {
        Small,
        Medium,
        Large,
        Massive
    }

    public struct AsteroidSize
    {
        public int MinSize { get; private set; }
        public int MaxSize { get; private set; }
        public AsteroidType Type { get; private set; }
        public int MaxResources { get; private set; }

        private AsteroidSize(int minSize, int maxSize, AsteroidType type, int maxResources)
        {
            MinSize = minSize;
            MaxSize = maxSize;
            Type = type;
            MaxResources = maxResources;
        }

        public static readonly AsteroidSize Small = new AsteroidSize(1, 3, AsteroidType.Small, 30);
        public static readonly AsteroidSize Medium = new AsteroidSize(4, 6, AsteroidType.Medium, 100);
        public static readonly AsteroidSize Large = new AsteroidSize(7, 11, AsteroidType.Large, 200);
        public static readonly AsteroidSize Massive = new AsteroidSize(15, 21, AsteroidType.Massive, 500);
    }

    public AsteroidSize Size { get; private set; }
    public int ResourcesQuantity { get; private set; }
    public int WaterAmount { get; private set; }
    public int BeskariumAmount { get; private set; }
    public int PeridotAmount { get; private set; }
    public int LechatelieriteAmount { get; private set; }
    public int ElaliiteAmount { get; private set; }
    public Asteroid Asteroid { get; set; }

    private readonly Dictionary<ResourceType, int> resources = new()
    {
        { ResourceType.Nothing, 30 },
        { ResourceType.Water, 5 },
        { ResourceType.Beskarium, 16 },
        { ResourceType.Peridot, 16 },
        { ResourceType.Lechatelierite, 16 },
        { ResourceType.Elaliite, 16 }
    };

    private readonly Dictionary<AsteroidSize, int> sizes = new()
    {
        { AsteroidSize.Small, 30 },
        { AsteroidSize.Medium, 50 },
        { AsteroidSize.Large, 20 },
        { AsteroidSize.Massive, 1 }
    };

    public AsteroidData()
    {
        Size = RandomPickSize();

        while (ResourcesQuantity < Size.MaxResources)
        {
            ResourceType resourceType = RandomPickResource();
            AddResource(resourceType);
        }

        ResourcesQuantity = WaterAmount + BeskariumAmount + PeridotAmount + LechatelieriteAmount + ElaliiteAmount;
    }

    private ResourceType RandomPickResource()
    {
        int totalWeight = 0;
        foreach (var weight in resources.Values)
        {
            totalWeight += weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        foreach (KeyValuePair<ResourceType, int> item in resources)
        {
            cumulativeWeight += item.Value;
            if (randomValue < cumulativeWeight)
            {
                return item.Key;
            }
        }

        return ResourceType.Nothing;
    }
    private AsteroidSize RandomPickSize()
    {
        int totalWeight = 0;
        foreach (var weight in sizes.Values)
        {
            totalWeight += weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        foreach (KeyValuePair<AsteroidSize, int> item in sizes)
        {
            cumulativeWeight += item.Value;
            if (randomValue < cumulativeWeight)
            {
                return item.Key;
            }
        }

        return AsteroidSize.Medium;
    }
    private void AddResource(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Water:
                WaterAmount++;
                ResourcesQuantity++;
                break;
            case ResourceType.Beskarium:
                BeskariumAmount++;
                ResourcesQuantity++;
                break;
            case ResourceType.Peridot:
                PeridotAmount++;
                ResourcesQuantity++;
                break;
            case ResourceType.Lechatelierite:
                LechatelieriteAmount++;
                ResourcesQuantity++;
                break;
            case ResourceType.Elaliite:
                ElaliiteAmount++;
                ResourcesQuantity++;
                break;
            case ResourceType.Nothing:
                ResourcesQuantity++;
                break;
            default:
                break;
        }
    }
    public void TakeResource(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Water:
                if (WaterAmount > 0)
                {
                    WaterAmount--;
                    ResourcesQuantity--;
                }
                break;
            case ResourceType.Beskarium:
                if (BeskariumAmount > 0)
                {
                    BeskariumAmount--;
                    ResourcesQuantity--;
                }
                break;
            case ResourceType.Peridot:
                if (PeridotAmount > 0)
                {
                    PeridotAmount--;
                    ResourcesQuantity--;
                }
                break;
            case ResourceType.Lechatelierite:
                if (LechatelieriteAmount > 0)
                {
                    LechatelieriteAmount--;
                    ResourcesQuantity--;
                }
                break;
            case ResourceType.Elaliite:
                if (ElaliiteAmount > 0)
                {
                    ElaliiteAmount--;
                    ResourcesQuantity--;
                }
                break;
            case ResourceType.Nothing:
                break;
            default:
                break;
        }
    }

    public bool CheckIfResourceDepleted(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Water:
                return WaterAmount <= 0;
            case ResourceType.Beskarium:
                return BeskariumAmount <= 0;
            case ResourceType.Peridot:
                return PeridotAmount <= 0;
            case ResourceType.Lechatelierite:
                return LechatelieriteAmount <= 0;
            case ResourceType.Elaliite:
                return ElaliiteAmount <= 0;
            case ResourceType.Nothing:
                return true;
            default:
                return true;
        }
    }

    public int GetResourceAmount(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Water:
                return WaterAmount;
            case ResourceType.Beskarium:
                return BeskariumAmount;
            case ResourceType.Peridot:
                return PeridotAmount;
            case ResourceType.Lechatelierite:
                return LechatelieriteAmount;
            case ResourceType.Elaliite:
                return ElaliiteAmount;
            default:
                return 0;
        }
    }
}
