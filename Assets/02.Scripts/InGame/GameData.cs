﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    [SerializeField] public Mesh SkullKnight;

    public static string EnemyName;

    public static GameData data;
    
    [SerializeField] Transform Map;
    
    public TileInfo[] Tiles;
    public List<UnitInfo> Units;

    [SerializeField] public Color enemyColor;
    [SerializeField] public Color teamColor;
    [SerializeField] public Color CanUseColor;

    [SerializeField] public int bitinium;
    [SerializeField] public int Maxbitinium;
    [SerializeField] public int LeaderShip;
    [SerializeField] public int MaxLeaderShip;
    
    [SerializeField] public int EndTurnValue;

    //초기화
    void Awake () {
        data = this;
        
        Tiles = new TileInfo[Map.childCount];
        Units = new List<UnitInfo>();

        //타일 정보 불러오기
        for (int i = 0; i < Map.childCount; i++)
        {
            Tiles[i] = Map.GetChild(i).GetComponent<TileInfo>();
        }
	}


    //유닛 데이터에 추가
    public void AddUnit(UnitInfo unit)
    {
        Units.Add(unit);
    }
    //유닛 데이터에서 삭제
    public void DelUnit(UnitInfo unit)
    {
        if(unit.Kinds== "Dokugawa" || unit.Kinds == "Winvelt" || unit.Kinds == "Brownbeard" || unit.Kinds == "Deathknight" || unit.Kinds == "Mars" || unit.Kinds == "Ragnarr")
        {
            if (unit.photonView.isMine)
            {
                EndUI.UI._myScore = 0;
            }
            else
            {
                EndUI.UI._enemyScore = 0;
            }
            EndUI.UI.SetRemainTurn(0);
        }
        Units.Remove(unit);
    }

    //좌표로 유닛찾기
    public UnitInfo FindUnit(int x, int y)
    {
        foreach(UnitInfo unit in Units)
        {
            if (unit.x == x && unit.y == y) return unit;
        }
        return null;
    }

    //좌표로 타일찾기
    public TileInfo FindTile(int x, int y)
    {
        foreach(TileInfo tile in Tiles)
        {
            if (tile.x == x && tile.y == y) return tile;
        }
        return null;
    }

    //비티늄 셋팅
    public void SetBitinium(int point)
    {
        bitinium += point;
        if (bitinium > Maxbitinium) bitinium = Maxbitinium;

        //비티늄 하단바 설정
        InfoBar.bar.SetBit(bitinium);
    }
}