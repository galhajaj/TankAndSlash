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
    public bool IsActive;
}

public abstract class Chip : MonoBehaviour
{
    public enum ChipType
    {
        TURRET,
        CONST,
        STATE,
        CONSUMABLE,
        NONE
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

    private bool _isActive;
    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            _isActive = value;
            changeFrameVisibility();
        }
    }

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

    private void changeFrameVisibility()
    {
            this.transform.Find("Frame").GetComponent<SpriteRenderer>().enabled = _isActive;
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

    public virtual void Install()
    {
        Debug.Log("Install chip...");
        DataManager.Instance.SaveDataToFile();
    }
    public virtual void Uninstall()
    {
        Debug.Log("Uninstall chip...");
        DataManager.Instance.SaveDataToFile();
    }
    public virtual void Activate()
    {
        Debug.Log("Activate chip...");
        if (Type == ChipType.CONSUMABLE)
        {
            // destroy it
        }
        IsActive = true;
    }
    public virtual void Deactivate()
    {
        Debug.Log("Deactivate chip...");
        IsActive = false;
    }
}
