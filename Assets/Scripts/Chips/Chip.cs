using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChipData
{
    public int ChipID;
    public Chip.ChipType ChipType;
    public string ChipName;
    public string GridName;
    public string SocketName;
}

public class Chip : MonoBehaviour
{
    public enum ChipType
    {
        TURRET,
        CONST,
        STATE,
        CONSUMABLE
    }

    private ChipType _type;
    public ChipType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            changeColor();
        }
    }

    public int ChipID;
    public string ChipName;
    public string GridName; // in which grid contained - inventory/turrets/skills
    public string SocketName; // the socket name inside grid

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public ChipData GetChipData()
    {
        ChipData data = new ChipData();
        data.ChipID = this.ChipID;
        data.ChipType = this.Type;
        data.ChipName = this.ChipName;
        data.GridName = this.GridName;
        data.SocketName = this.SocketName;
        return data;
    }

    public void init()
    {
        // set random type
        int rand = UnityEngine.Random.Range(0, 4);
        if (rand == 0)
            Type = Chip.ChipType.TURRET;
        else if (rand == 1)
            Type = Chip.ChipType.CONST;
        else if (rand == 2)
            Type = Chip.ChipType.STATE;
        else if (rand == 3)
            Type = Chip.ChipType.CONSUMABLE;

        // set grid & socket names
    }

    public void setRandomSubType()
    {
        // from config files... TODO
    }

    private void changeColor()
    {
        switch (_type)
        {
            case ChipType.TURRET:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case ChipType.CONST:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case ChipType.STATE:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case ChipType.CONSUMABLE:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            default:
                break;
        }
    }
}
