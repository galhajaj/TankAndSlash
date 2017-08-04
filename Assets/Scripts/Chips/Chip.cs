using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public enum ChipType
    {
        TURRET,
        CONST,
        STATE,
        CONSUMABLE
    }

    public ChipType _type;
    public ChipType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            changeColor();
        }
    }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void initRandomType()
    {
        int rand = Random.Range(0, 4);
        if (rand == 0)
            Type = Chip.ChipType.TURRET;
        else if (rand == 1)
            Type = Chip.ChipType.CONST;
        else if (rand == 2)
            Type = Chip.ChipType.STATE;
        else if (rand == 3)
            Type = Chip.ChipType.CONSUMABLE;
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
