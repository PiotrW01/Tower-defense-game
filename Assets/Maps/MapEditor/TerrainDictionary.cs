using System.Collections.Generic;
using UnityEngine;

public class TerrainDictionary : MonoBehaviour
{
    public static Dictionary<Terrain, Sprite> Sprites;
    public Sprite Asphalt;
    public Sprite BeachRocks;
    public Sprite Concrete;
    public Sprite CrackedSoil;
    public Sprite Gravel;
    public Sprite MixedSnow;
    public Sprite RockyGround;
    public Sprite RockySand;
    public Sprite RoughRock;
    public Sprite RustyMetal;
    public Sprite SnowyRock;
    void Awake()
    {
        Sprites = new Dictionary<Terrain, Sprite>()
        {
            {Terrain.Gravel, Gravel},
            {Terrain.Asphalt, Asphalt},
            {Terrain.BeachRocks, BeachRocks},
            {Terrain.Concrete, Concrete},
            {Terrain.CrackedSoil, CrackedSoil},
            {Terrain.MixedSnow, MixedSnow},
            {Terrain.RockyGround, RockyGround},
            {Terrain.RockySand, RockySand},
            {Terrain.RoughRock, RoughRock},
            {Terrain.RustyMetal, RustyMetal},
            {Terrain.SnowyRock, SnowyRock},
        };
    }
}

public enum Terrain
{
    Asphalt,
    BeachRocks,
    Concrete,
    CrackedSoil,
    Gravel,
    MixedSnow,
    RockyGround,
    RockySand,
    RoughRock,
    RustyMetal,
    SnowyRock
}