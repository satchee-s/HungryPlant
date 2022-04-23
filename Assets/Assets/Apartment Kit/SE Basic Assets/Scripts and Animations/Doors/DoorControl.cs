using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{

		public Animator openandclose;
		public bool open;
		public bool locked;

		void Start()
		{
			open = false;
		}

		public void Interact()
		{
			if (!locked)
			{				
				if (open == false)
				{
					StartCoroutine(opening());
				}
				else if (open == true)
				{
					StartCoroutine(closing());
				}
			}
		}

		public void LockDoor(bool state)
        {
			locked = state;
        }

		IEnumerator opening()
		{
			//print("you are opening the door");
			openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		public IEnumerator closing()
		{
			//print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}
}

