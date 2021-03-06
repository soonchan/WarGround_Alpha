﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {

    public static Calculator Calc;
	// Use this for initialization
	void Awake () {
        Calc = this;
	}

    //이동 행동력계산,path 반환
    public List<TileInfo> Move(TileInfo SP, TileInfo EP, int Act)
    {
        int s=0, e=0, minAct=Act+1,minActIndex=0;

        TileInfo tile, temp;
        List<TileInfo> TileQueue = new List<TileInfo>();
        List<int> ActQueue = new List<int>();
        List<int> PathQueue = new List<int>();
        int[,] check = new int[15,15];

        //초기화
        for(int i = 0; i < 15; i++)
        {
            for(int j = 0; j < 15; j++)
            {
                check[i, j] = 999;
            }
        }
        TileQueue.Add(SP);
        ActQueue.Add(0);
        PathQueue.Add(0);
        

        while (s <= e)
        {
            tile = TileQueue[s];
            int k = tile.x % 2 == 1 ? 1 : 0;
            
            if (tile.gameObject.Equals(EP.gameObject))
            {
                if (minAct > ActQueue[s])
                {
                    minAct = ActQueue[s];
                    minActIndex = s;
                }
                //최소값 비교
            }
            

            temp = GameData.data.FindTile(tile.x, tile.y - 1);
            if (temp != null && ActQueue[s]+temp.cost<=Act && check[temp.x,temp.y] > ActQueue[s] + temp.cost)
            {
                check[temp.x, temp.y] = ActQueue[s] + temp.cost;
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x, tile.y + 1);
            if (temp != null && ActQueue[s] + temp.cost <= Act && check[temp.x, temp.y] > ActQueue[s] + temp.cost)
            {
                check[temp.x, temp.y] = ActQueue[s] + temp.cost;
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act && check[temp.x, temp.y] > ActQueue[s] + temp.cost)
            {
                check[temp.x, temp.y] = ActQueue[s] + temp.cost;
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y + 1 - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act && check[temp.x, temp.y] > ActQueue[s] + temp.cost)
            {
                check[temp.x, temp.y] = ActQueue[s] + temp.cost;
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act && check[temp.x, temp.y] > ActQueue[s] + temp.cost)
            {
                check[temp.x, temp.y] = ActQueue[s] + temp.cost;
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y + 1 - k);
            if (temp != null && ActQueue[s] + temp.cost <= Act && check[temp.x, temp.y] > ActQueue[s] + temp.cost)
            {
                check[temp.x, temp.y] = ActQueue[s] + temp.cost;
                e++;
                TileQueue.Add(temp);
                ActQueue.Add(ActQueue[s] + temp.cost);
                PathQueue.Add(s);
            }
            s++;
        }
        if (minActIndex != 0)
        {
            int index = minActIndex;
            List<TileInfo> path = new List<TileInfo>();
            path.Add(TileQueue[index]);
            while (index != 0)
            {
                index = PathQueue[index];
                path.Add(TileQueue[index]);
            }
            path.Reverse();
            return path;
        }
        return null;
    }

    //공격 가능 여부 계산, 가능 : true, 불가능 : false
    public int Range(TileInfo EP, TileInfo SP, int range)
    {
        int s = 0, e = 0;
        TileInfo tile, temp;
        List<TileInfo> TileQueue = new List<TileInfo>();
        List<int> RangeQueue = new List<int>();
        TileQueue.Add(SP);
        RangeQueue.Add(0);
        
        while (s <= e)
        {
            tile = TileQueue[s];
            int k = tile.x % 2 == 1 ? 1 : 0;
            if (tile.gameObject.Equals(EP.gameObject))
            {
                return RangeQueue[s];
            }
            temp = GameData.data.FindTile(tile.x, tile.y - 1);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x, tile.y + 1);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x + 1, tile.y + 1 - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            temp = GameData.data.FindTile(tile.x - 1, tile.y + 1 - k);
            if (temp != null && RangeQueue[s] + 1 <= range)
            {
                e++;
                TileQueue.Add(temp);
                RangeQueue.Add(RangeQueue[s] + 1);
            }
            s++;
        }
        return -1;
    }

}
