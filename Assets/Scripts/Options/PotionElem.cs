using UnityEngine;

public class PotionElem : MonoBehaviour
{
    [SerializeField] private Animator[] anims;
    [SerializeField] private int value;

    private void Update()
    {
        switch(value)
        {
            case 1:
            anims[0].SetBool("show", true);
            anims[1].SetBool("show", false);
            anims[2].SetBool("show", false);
            break;

            case 2:
            anims[0].SetBool("show", true);
            anims[1].SetBool("show", true);
            anims[2].SetBool("show", false);
            break;

            case 3:
            anims[0].SetBool("show", true);
            anims[1].SetBool("show", true);
            anims[2].SetBool("show", true);
            break;

            case 0:
            anims[0].SetBool("show", false);
            anims[1].SetBool("show", false);
            anims[2].SetBool("show", false);
            break;
        }
    }

    public void PickUpPotion()
    {
        if (value + 1 <= 3) value++;
    }

    public void GiveAwayPotion()
    {
        if (value - 1 >= 0) value++;
    }
}