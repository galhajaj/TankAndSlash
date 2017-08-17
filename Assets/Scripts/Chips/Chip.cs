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
        SKILL,
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
            if (_isActive)
                this.activate();
            else
                this.deactivate();
        }
    }

    public int Cost = 0;
    public float CostPerSecond = 0.0F;

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
            case ChipType.SKILL:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            default:
                break;
        }
    }

    public virtual void Install()
    {
        Debug.Log("Install chip...");
    }
    public virtual void Uninstall()
    {
        Debug.Log("Uninstall chip...");
    }
    protected virtual void activate()
    {
        Debug.Log("Activate chip...");
        // destroy consumable after activation
        if (Type == ChipType.CONSUMABLE)
        {
            this.transform.SetParent(null);
            DestroyImmediate(this.gameObject);
            DataManager.Instance.SaveDataToFile();
        }
    }
    protected virtual void deactivate()
    {
        Debug.Log("Deactivate chip...");
    }

    public virtual void ExecuteStart()
    {
        Debug.Log("Execute start chip...");
    }
    public virtual void ExecuteContinues()
    {
        Debug.Log("Execute continues chip...");
    }
    public virtual void ExecuteEnd()
    {
        Debug.Log("Execute end chip...");
    }
}
