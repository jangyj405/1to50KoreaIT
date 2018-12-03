using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCharacterMgr
{
    // {POLAR, CAMEL, PUPPY, PENGUIN, SHEEP, KITTEN};
    private Dictionary<int, CHAR_TYPE> charIntToEnum = new Dictionary<int, CHAR_TYPE>()
    {
        {(int)CHAR_TYPE.POLAR, CHAR_TYPE.POLAR},
         {(int)CHAR_TYPE.CAMEL, CHAR_TYPE.CAMEL},
         {(int)CHAR_TYPE.PUPPY, CHAR_TYPE.PUPPY},
         {(int)CHAR_TYPE.PENGUIN, CHAR_TYPE.PENGUIN},
         {(int)CHAR_TYPE.SHEEP, CHAR_TYPE.SHEEP},
         {(int)CHAR_TYPE.KITTEN, CHAR_TYPE.KITTEN}

    };
	private static csCharacterMgr m_Instance;
	public static csCharacterMgr GetInstance()
	{
		if (m_Instance == null) {
			m_Instance = new csCharacterMgr ();
		}
		return m_Instance;
	}

	private CHAR_TYPE Char_Type;

	public void CharacterChoice(CHAR_TYPE chartype)
	{
		Char_Type = chartype;
	}

    public void CharacterChoice(int idx)
    {
        Char_Type = charIntToEnum[idx];
    }

	public void CharacterEffect()
	{
		switch (Char_Type) {
		case CHAR_TYPE.POLAR:
			
			break;

		case CHAR_TYPE.CAMEL:
			
			break;

		case CHAR_TYPE.KITTEN:
			
			break;

		case CHAR_TYPE.PENGUIN:
			
			break;

		case CHAR_TYPE.PUPPY:
			
			break;

		case CHAR_TYPE.SHEEP:
			
			break;
		}
	}


}
