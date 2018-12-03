using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCharacterMgr
{
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
